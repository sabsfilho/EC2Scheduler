using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;

class CreateImageTrigger : ATrigger
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
        EC2Client.TerminateInstancesAsync(request);
        return "ok";
    }
}