using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;

class CreateImageTrigger : ATrigger
{
    protected override string RunRequest()
    {
        string? id = SchedulerRequest.ParamA;
        if (id == null){
            throw new Exception("InstanceId undefined in ParamA");
        }
        string? key = SchedulerRequest.ParamB;
        if (key == null){
            throw new Exception("Key undefined in ParamB");
        }

        string name = $"{key}_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}";

        SchedulerRequest.AddLog($"create image {name}");

        var request = new CreateImageRequest(){
            InstanceId = id,
            Name = name,
            TagSpecifications = new List<TagSpecification>(){
                new TagSpecification(){
                    ResourceType = new Amazon.EC2.ResourceType("image"),
                    Tags = new List<Tag>(){
                        new Tag(){
                            Key = Control.SCHEDULER_KEY_IMAGE,
                            Value = string.Empty
                        }
                    }
                }
            }
        };
        var t = EC2Client.CreateImageAsync(request);
        t.Wait();
        return t.Result.ImageId;
    }
}