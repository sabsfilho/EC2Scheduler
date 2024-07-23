using Amazon.Lambda.Core;
using EC2SchedulerLib;

class LoggerListener : ILoggerListener
{
    private ILambdaLogger logger;

    public LoggerListener(ILambdaLogger logger)
    {
        this.logger = logger;
    }

    public void LogException(string m)
    {
        logger.LogError(m);
    }

    public void LogInformation(string m)
    {
        logger.LogInformation(m);
    }
}