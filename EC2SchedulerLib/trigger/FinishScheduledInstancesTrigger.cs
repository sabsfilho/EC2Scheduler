using Amazon.EC2.Model;
using HelperLib;

namespace EC2SchedulerLib.trigger;

class FinishScheduledInstancesTrigger : ListScheduledInstancesTrigger
{
    protected override bool CheckInstance(Instance y)
    {
        return (y.State.Code & 255 ) == 16;
    }
    protected override string RunRequest()
    {
        if (!DateTime.Now.IsWorkdayInBrazil()) return "nok";

        var xs = 
            BuildList();

        if (xs.Count == 0) return "nok";

        var stops = new List<string>();
        var terms = new List<string>();

        foreach(var x in xs)
        {
            if (string.IsNullOrEmpty(x.User)){
                stops.Add(x.InstanceId);
            }
            else {
                terms.Add(x.InstanceId);
            }
        }

        if (stops.Count > 0) {

            var request = new StopInstancesRequest()
            {
                InstanceIds = stops
            };

            var t = EC2Client.StopInstancesAsync(request);
            t.Wait();
        }

        if (terms.Count > 0) {

            var request = new TerminateInstancesRequest()
            {
                InstanceIds = stops
            };

            var t = EC2Client.TerminateInstancesAsync(request);
            t.Wait();
        }
        
        return "ok";
    }
}