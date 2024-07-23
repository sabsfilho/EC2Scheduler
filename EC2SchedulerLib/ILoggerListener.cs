namespace EC2SchedulerLib;
public interface ILoggerListener
{
    void LogInformation(string m);
    void LogException(string m);
}