namespace EC2SchedulerLib.trigger;

class RunAllServicesTrigger : ATrigger
{
    protected override string RunRequest()
    {
        var triggers = new ATrigger[]
        {
            new StartScheduledInstancesTrigger(),
            new FinishScheduledInstancesTrigger(),
            new SyncronizeBacktestImageTrigger(),
            new MonitorWebServicesTrigger()
        };

        foreach(var t in triggers)
        {
            t.Run(SchedulerRequest);
            Thread.Sleep(1000);
        }
        
        return "ok";
    }
}