namespace EC2SchedulerLib;

public enum ActionEnum
{
    UNDEFINED = 0,
    UpdateScheduledInstance = 10,
    ListScheduledInstances = 20,
    StartScheduledInstances = 30,
    FinishScheduledInstances = 40,
    ListImages = 50,
    CreateImage = 60,
    DeleteImage = 70,
    CreateInstanceFromImage = 80,
    TerminateInstance = 90,
    MonitorWebServices = 100,
    SyncronizeBacktestImage = 110,
    RemoveScheduledTag = 120,
    RunAllServices = 130
}