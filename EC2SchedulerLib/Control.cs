using System.Text.Json;

namespace EC2SchedulerLib;

public static class Control
{
    internal const string BUCKET_NAME_PCB_LIB = "SET_YOUR_BUCKET_NAME_PCB_LIB";
    internal const string KEY_PAIR_NAME = "SET_YOUR_KEY_PAIR_NAME";
    internal const string SUBNET_ID = "SET_YOUR_SUBNET_ID";
    internal const string SECURITY_GROUP_ID = "SET_YOUR_SECURITY_GROUP_ID";
    internal const string INSTANCE_TYPE = "t3.medium";
    internal const string SCHEDULER_REQUESTED_BY_USER = "SchedulerRequestedByUser";
    internal const string SCHEDULER_KEY_START = "SchedulerStart";
    internal const string SCHEDULER_KEY_FINISH = "SchedulerFinish";
    internal const string SCHEDULER_KEY_IMAGE = "SchedulerImage";
    internal const int SCHEDULER_DEFAULT_DURATION_HOURS = 10;
    
    /*
    input = SchedulerRequest
    Action = SchedulerEnum (required)
        ListScheduledInstances,
        UpdateScheduledInstances,
        DeleteScheduledInstances,
        StartScheduledInstances,
        FinishScheduledInstances,
        ListImages,
        CreateImage,
        DeleteImage,
        CreateInstanceFromImage,
        TerminateInstance,
        MonitorWebServices,
        RunAllServices
    ParamA = auxiliary parameter A
    ParamB = auxiliary parameter B
    ParamC = auxiliary parameter C

{"Action":0,"User":"test","ParamA":null,"ParamB":null,"ParamC":null}
    */
    public static string Run(string input)
    {
        if (string.IsNullOrEmpty(input)){
            throw new NullReferenceException();
        }

        var request = JsonSerializer.Deserialize<SchedulerRequest>(input)!;
        return request.Run();
    }

}
