using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;

class TerminateInstanceTrigger : ATrigger
{
    protected override string RunRequest()
    {
        string? ids = SchedulerRequest.ParamA;
        if (ids == null){
            throw new Exception("InstanceIds undefined in ParamA");
        }
        var request = new TerminateInstancesRequest(){
            InstanceIds = ids.Split(',').ToList()
        };
        var t = EC2Client.TerminateInstancesAsync(request);
        t.Wait();
        return "ok";
    }
}