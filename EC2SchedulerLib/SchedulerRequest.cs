using EC2SchedulerLib.trigger;

namespace EC2SchedulerLib;

public class SchedulerRequest
{
    public required ActionEnum Action { get; set; }
    public required string User { get; set; }
    public string? ParamA { get; set; }
    public string? ParamB { get; set; }

    public string Run()
    {
        ATrigger trigger;

        switch(Action)
        {
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
            case ActionEnum.CreateInstanceFromImage:
                trigger = new CreateInstanceFromImageTrigger();
                break;
            default:
                throw new Exception($"not implemented {Action}");
        }

        return trigger.Run(this);
    }
}
