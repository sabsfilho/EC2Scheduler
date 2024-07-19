using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;

class UpdateScheduledInstanceTrigger : ATrigger
{
    protected override string RunRequest()
    {
        string? ids = SchedulerRequest.ParamA;
        if (ids == null){
            throw new Exception("InstanceId undefined in ParamA");
        }
        string? tagName = SchedulerRequest.ParamB;
        if (tagName == null){
            throw new Exception("TagName undefined in ParamB");
        }
        string? tagValue = SchedulerRequest.ParamC;
        if (tagValue == null){
            throw new Exception("TagValue undefined in ParamC");
        }

        var request = new CreateTagsRequest(){
            Resources = ids.Split(',').ToList(),
            Tags= new List<Tag>(){
                new Tag(){
                    Key = tagName,
                    Value = tagValue
                }
            }
        };
        var t = EC2Client.CreateTagsAsync(request);
        t.Wait();
        
        return "ok";
    }
}