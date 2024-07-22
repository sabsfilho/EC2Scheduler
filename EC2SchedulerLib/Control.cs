using System.Text.Json;

namespace EC2SchedulerLib;

public static class Control
{
    internal const string KEY_PAIR_NAME = "PutCallSP";
    internal const string SUBNET_ID = "subnet-22814f47";
    internal const string SECURITY_GROUP_ID = "sg-63587e05";
    internal const string INSTANCE_TYPE = "T3Medium";
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
        MonitorWebServices
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