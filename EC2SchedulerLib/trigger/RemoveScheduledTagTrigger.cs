using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;

class RemoveScheduledTagTrigger : ATrigger
{
    protected override string RunRequest()
    {
        string? ids = SchedulerRequest.ParamA;
        if (ids == null){
            throw new Exception("InstanceId undefined in ParamA");
        }
        var request = new DeleteTagsRequest(){
            Resources = ids.Split(',').ToList(),
            Tags= new List<Tag>(){
                new Tag(){
                    Key = Control.SCHEDULER_KEY_START
                },
                new Tag(){
                    Key = Control.SCHEDULER_KEY_FINISH
                }
            }
        };
        var t = EC2Client.DeleteTagsAsync(request);
        t.Wait();
        
        return "ok";
    }
}