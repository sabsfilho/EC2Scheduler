using AWSLib;
using HelperLib;
using System.Text.Json;
using EC2SchedulerLib.trigger.keepalive;

namespace EC2SchedulerLib.trigger;

class MonitorWebServicesTrigger : ATrigger
{
    protected override string RunRequest()
    {
        try
        {
            if (!DateTime.Today.IsWorkdayInBrazil())
            {
                SchedulerRequest.AddLog("not workday");
                return "nok";
            }
            if (
                DateTime.Now.TimeOfDay < new TimeSpan(7,5,0) ||
                DateTime.Now.TimeOfDay > new TimeSpan(17, 55, 0)
            )
            {
                SchedulerRequest.AddLog("not worktime");
                return "nok";
            }

            SchedulerRequest.AddLog("processing...");

            string txt = 
                S3Control.DownloadSingleFile(
                    Control.BUCKET_NAME_PCB_LIB, 
                    "KeepAliveCfg.json",
                    true
                )!;

            var cfg = JsonSerializer.Deserialize<KeepAliveCfg>(txt)!;
                        
            foreach(string z in cfg.TargetUrls)
            {
                try
                {
                    var t = new KeepAliveChecker()
                    {
                        Cfg = cfg,
                        Url = z,
                        SchedulerRequest = SchedulerRequest
                    };
                    t.Load();
                }
                catch(Exception ex)
                {
                    SchedulerRequest.AddLog(ex);
                }
            }
            SchedulerRequest.AddLog("finished");
            return "ok";
        }
        catch(Exception ex)
        {
            SchedulerRequest.AddLog(ex);
            return "nok";
        }
    }
}