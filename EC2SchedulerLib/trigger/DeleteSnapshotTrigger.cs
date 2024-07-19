using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;

class DeleteSnapshotTrigger : ATrigger
{
    protected override string RunRequest()
    {
        string? id = SchedulerRequest.ParamA;
        if (id == null){
            throw new Exception("InstanceIds undefined in ParamA");
        }
        var requestSnapshot = new DeleteSnapshotRequest(){
            SnapshotId = id
        };
        var t2 = EC2Client.DeleteSnapshotAsync(requestSnapshot);
        t2.Wait();

        return "ok";
    }
}