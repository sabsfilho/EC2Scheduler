using System.Text.Json;
using EC2SchedulerLib.trigger;
using HelperLib;

namespace EC2SchedulerLib;

public class SchedulerRequest
{
    public required ActionEnum Action { get; set; }
    public required string User { get; set; }
    public string? ParamA { get; set; }
    public string? ParamB { get; set; }
    public string? ParamC { get; set; }
    private List<ILoggerListener>? logListeners;
    public void AddLogListener(ILoggerListener listener)
    {
        if (logListeners == null)
        {
            logListeners = new List<ILoggerListener>();
        }
        logListeners.Add(listener);
    }

    internal void AddLog(Exception ex)
    {
        if (logListeners == null) return;
        var ms = new List<string>();
        Exception? exi = ex;
        while(exi != null){
            ms.Add(exi.Message);
            ms.Add(exi.StackTrace ?? "-" );
            ms.Add("#####");            
            exi = exi.InnerException;
        }
        string m = string.Join(Environment.NewLine, ms);
        logListeners.ForEach(x=>x.LogException(m));
    }
    internal void AddLog(string m)
    {
        if (logListeners == null) return;
        logListeners.ForEach(x=>x.LogInformation(m));
    }

    public string Run()
    {
        try
        {
            AddLog($"requested {JsonSerializer.Serialize(this)}");

            ATrigger trigger;

            switch(Action)
            {
                case ActionEnum.UNDEFINED:
                    return $"Action: UNDEFINED, User: {User}, Date: {DateTime.Now.ToStringHelperDDMMYYYYHHMMSS()}";
                case ActionEnum.UpdateScheduledInstance:
                    trigger = new UpdateScheduledInstanceTrigger();
                    break;
                case ActionEnum.ListScheduledInstances:
                    trigger = new ListScheduledInstancesTrigger();
                    break;
                case ActionEnum.StartScheduledInstances:
                    trigger = new StartScheduledInstancesTrigger();
                    break;
                case ActionEnum.FinishScheduledInstances:
                    trigger = new FinishScheduledInstancesTrigger();
                    break;
                case ActionEnum.ListImages:
                    trigger = new ListImagesTrigger();
                    break;
                case ActionEnum.CreateImage:
                    trigger = new CreateImageTrigger();
                    break;
                case ActionEnum.DeleteImage:
                    trigger = new DeleteImageTrigger();
                    break;
                case ActionEnum.CreateInstanceFromImage:
                    trigger = new CreateInstanceFromImageTrigger();
                    break;
                case ActionEnum.TerminateInstance:
                    trigger = new TerminateInstanceTrigger();
                    break;
                case ActionEnum.MonitorWebServices:
                    trigger = new MonitorWebServicesTrigger();
                    break;
                case ActionEnum.SyncronizeBacktestImage:
                    trigger = new SyncronizeBacktestImageTrigger();
                    break;
                case ActionEnum.RemoveScheduledTag:
                    trigger = new RemoveScheduledTagTrigger();
                    break;
                case ActionEnum.RunAllServices:
                    trigger = new RunAllServicesTrigger();
                    break;
                default:
                    throw new Exception($"not implemented {Action}");
            }

            string rtn = trigger.Run(this);
            AddLog($"finished rtn={rtn} ");
            return rtn;
        }
        catch(Exception ex)
        {
            AddLog(ex);
            return "nok";
        }
    }
}
