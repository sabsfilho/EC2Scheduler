using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;
/*
ParamA = ImageID,
ParamB = INSTANCE_TYPE or Control.INSTANCE_TYPE if null
*/
class CreateInstanceFromImageTrigger : ATrigger
{
    protected override string RunRequest()
    {
        string? imageID = SchedulerRequest.ParamA;
        if (string.IsNullOrEmpty(imageID)) {
            throw new Exception("ImageID must be defined in ParamA.");
        }

        var request = new RunInstancesRequest()
        {
            InstanceType = 
                new InstanceType(
                    SchedulerRequest.ParamB ?? Control.INSTANCE_TYPE
                ),
            MinCount = 1,
            MaxCount = 1,
            ImageId = imageID,
            KeyName = Control.KEY_PAIR_NAME      
        };
        request.TagSpecifications.Add(new TagSpecification(){
            ResourceType = ResourceType.Instance,
            Tags = new List<Tag>(){
                new Tag(Control.SCHEDULER_REQUESTED_BY_USER, SchedulerRequest.User),
                new Tag(
                    Control.SCHEDULER_KEY_FINISH,
                    DateTime.Now
                        .AddHours(Control.SCHEDULER_DEFAULT_DURATION_HOURS)
                        .ToString("HH:mm")
                )
            }
        });

        request.NetworkInterfaces =
            new List<InstanceNetworkInterfaceSpecification>() 
            {
                new InstanceNetworkInterfaceSpecification() {
                    DeviceIndex = 0,
                    SubnetId = Control.SUBNET_ID,
                    Groups = new List<string>(){ Control.SECURITY_GROUP_ID },
                    AssociatePublicIpAddress = true
                }
            };
        
        var t = EC2Client.RunInstancesAsync(request);
        t.Wait();

        Thread.Sleep(1000);

        return t.Result.Reservation.Instances.First().InstanceId;
    }
}