using Amazon.EC2.Model;
using HelperLib;

namespace EC2SchedulerLib.trigger;

abstract class AScheduledInstancesTrigger : ListScheduledInstancesTrigger
{
    protected abstract bool HasState(int code);
    protected abstract string GetKeyTimer();
    protected abstract bool RunCommand(List<InstanceDescription> descriptions);
    protected override bool CheckInstance(Instance y)
    {
        if (HasState(y.State.Code & 255 )) {
            var tag = 
                y.Tags
                .Where(x=>x.Key == GetKeyTimer())
                .FirstOrDefault();
            if (tag != null){
                string v = tag.Value;
                if (!string.IsNullOrEmpty(v)){
                    TimeSpan tm;
                    if (TimeSpan.TryParseExact(
                        v,
                        "hh\\:mm",
                        System.Globalization.CultureInfo.InvariantCulture, 
                        out tm
                    )){
                        var now = DateTime.Now.TimeOfDay;
                        if (
                            now > tm &&
                            now < tm.Add(new TimeSpan(0,5,0))
                        ){
                            return true;
                        }

                    }
                }
            }
        }

        return false;
    }
    protected override string RunRequest()
    {
        if (!DateTime.Now.IsWorkdayInBrazil()) {
            SchedulerRequest.AddLog("NOT WorkdayInBrazil");
            return "nok";
        }

        var lst = BuildList();

        SchedulerRequest.AddLog($"list count: {lst.Count}");

        bool b = lst.Count > 0;

        if (b) 
        {
            b = RunCommand(lst);
        }
        return b ? "ok" : "nok";
    }
}