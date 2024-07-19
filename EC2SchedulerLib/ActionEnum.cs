namespace EC2SchedulerLib;

public enum ActionEnum
{
    UNDEFINED,
    UpdateScheduledInstance,
    ListScheduledInstances,
    StartScheduledInstances,
    FinishScheduledInstances,
    ListImages,
    CreateImage,
    DeleteImage,
    CreateInstanceFromImage,
    TerminateInstance,
    MonitorWebServices,
    SyncronizeBacktestImage
}