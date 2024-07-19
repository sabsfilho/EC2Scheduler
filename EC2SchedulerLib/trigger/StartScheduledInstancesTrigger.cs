using Amazon.EC2.Model;
using HelperLib;

namespace EC2SchedulerLib.trigger;

class StartScheduledInstancesTrigger : ListScheduledInstancesTrigger
{
    protected override bool CheckInstance(Instance y)
    {
        return (y.State.Code & 255 ) == 80;
    }
    protected override string RunRequest()
    {
        if (!DateTime.Now.IsWorkdayInBrazil()) return "nok";

        var ids = 
            BuildList()
            .Select(x => x.InstanceId)
            .ToList();

        if (ids.Count == 0) return "nok";

        var request = new StartInstancesRequest()
        {
            InstanceIds = ids
        };

        var t = EC2Client.StartInstancesAsync(request);
        t.Wait();
        
        return "ok";
    }
}