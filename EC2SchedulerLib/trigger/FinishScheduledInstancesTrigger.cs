using Amazon.EC2.Model;
using HelperLib;

namespace EC2SchedulerLib.trigger;

class FinishScheduledInstancesTrigger : AScheduledInstancesTrigger
{
    protected override string GetKeyTimer()
    {
        return Control.SCHEDULER_KEY_FINISH;
    }
    protected override bool HasState(int code)
    {
        return code == 16 /* running */; 
    }
    protected override bool RunCommand(List<InstanceDescription> descriptions)
    {
        var stops = new List<string>();
        var terms = new List<string>();

        foreach(var x in descriptions)
        {
            if (string.IsNullOrEmpty(x.User)){
                stops.Add(x.InstanceId);
            }
            else {
                terms.Add(x.InstanceId);
            }
        }

        if (stops.Count > 0) {

            var request = new StopInstancesRequest()
            {
                InstanceIds = stops
            };

            var t = EC2Client.StopInstancesAsync(request);
            t.Wait();
        }

        if (terms.Count > 0) {

            var request = new TerminateInstancesRequest()
            {
                InstanceIds = stops
            };

            var t = EC2Client.TerminateInstancesAsync(request);
            t.Wait();
        }

        return true;
    }
}