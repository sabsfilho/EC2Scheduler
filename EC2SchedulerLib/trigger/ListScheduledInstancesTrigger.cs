using System.Text.Json;
using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;

class ListScheduledInstancesTrigger : ATrigger
{

    protected virtual bool CheckInstance(Instance y)
    {
        return true;
    }
    
    protected List<InstanceDescription> BuildList()
    {
        var instanceDescriptions = new List<InstanceDescription>();

        var request = new DescribeInstancesRequest();
        request.Filters.Add(
            new Filter(
                "tag-key",
                new List<string>(){
                    Control.SCHEDULER_REQUESTED_BY_USER,
                    Control.SCHEDULER_KEY_START,
                    Control.SCHEDULER_KEY_FINISH
                }
            )
        );

        var t = EC2Client.DescribeInstancesAsync(request);
        t.Wait();
        var response = t.Result;

        var xs = response.Reservations;
        foreach(var x in xs)
        {
            foreach(var y in x.Instances)
            {
                if (!CheckInstance(y)) continue;
                
                var d = new InstanceDescription()
                {
                    InstanceId = y.InstanceId,
                    ImageId = y.ImageId,
                    InstanceType = y.InstanceType.Value,                                        
                    Lifecycle = y.InstanceLifecycle == null ? "on-demand" : y.InstanceLifecycle.Value,
                    Name = GetName(y),
                    PrivateIP = y.PrivateIpAddress,
                    PublicIP = y.PublicIpAddress,
                    Started = y.LaunchTime.ToString("dd/MM/yyyy HH:mm:ss"),
                    Status = y.State.Name
                };
                string started = y.LaunchTime.ToString("dd/MM/yyyy HH:mm:ss");
                int state = y.State.Code & 255;
                if (state == 48 || state == 80)
                {
                    d.Finished = y.UsageOperationUpdateTime.ToString("dd/MM/yyyy HH:mm:ss");                    
                }
                var user = y.Tags.FirstOrDefault(x=>x.Key==Control.SCHEDULER_REQUESTED_BY_USER);
                if (user != null){
                    d.User = user.Value;
                }
                var start = y.Tags.FirstOrDefault(x=>x.Key==Control.SCHEDULER_KEY_START);
                if (start != null){
                    d.Start = start.Value;
                }
                var finish = y.Tags.FirstOrDefault(x=>x.Key==Control.SCHEDULER_KEY_FINISH);
                if (finish != null){
                    d.Finish = finish.Value;
                }
                instanceDescriptions.Add(d);
            }
        }

        return 
            instanceDescriptions;
        
    }

    private string GetName(Instance y)
    {
        var t = y.Tags.FirstOrDefault(x => x.Key == "Name");
        if (t == null) return "-";
        return t.Value;
    }

    protected override string RunRequest()
    {
        return
            JsonSerializer.Serialize(BuildList());
    }
}