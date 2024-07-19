using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;
class CreateInstanceFromImageTrigger : ATrigger
{
    protected override string RunRequest()
    {
        var request = new RunInstancesRequest()
        {
            InstanceType = (InstanceType) Enum.Parse(typeof(InstanceType), Control.INSTANCE_TYPE),
            MinCount = 1,
            MaxCount = 1,
            ImageId = SchedulerRequest.ParamA,
            KeyName = Control.KEY_PAIR_NAME      
        };
        request.TagSpecifications.Add(new TagSpecification(){
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
        
        EC2Client.RunInstancesAsync(request);

        Thread.Sleep(1000);

        return "ok";
    }
}