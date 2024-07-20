using Amazon.EC2.Model;
using HelperLib;

namespace EC2SchedulerLib.trigger;

class StartScheduledInstancesTrigger : AScheduledInstancesTrigger
{
    protected override string GetKeyTimer()
    {
        return Control.SCHEDULER_KEY_START;
    }
    protected override bool HasState(int code)
    {
        return code == 80 /* stopped */; 
    }
    protected override bool RunCommand(List<InstanceDescription> descriptions)
    {
        var request = new StartInstancesRequest()
        {
            InstanceIds = 
                descriptions
                .Select(x => x.InstanceId)
                .ToList()
        };

        var t = EC2Client.StartInstancesAsync(request);
        t.Wait();

        return true;
    }
}