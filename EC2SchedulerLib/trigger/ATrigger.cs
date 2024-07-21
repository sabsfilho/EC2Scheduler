using Amazon.EC2;

namespace EC2SchedulerLib.trigger;

abstract class ATrigger
{
    abstract protected string RunRequest();
    protected SchedulerRequest SchedulerRequest { get; private set; }
    AmazonEC2Client? eC2Client = null;
    public ATrigger(){
        SchedulerRequest = new SchedulerRequest(){
            Action = ActionEnum.UNDEFINED,
            User = "undefined"
        };
    }
    protected AmazonEC2Client EC2Client
    {
        get
        {
            if (eC2Client == null)
            {
                eC2Client = new AmazonEC2Client();        
            }
            return eC2Client;
        }
    }
    public string Run(SchedulerRequest r)
    {
        SchedulerRequest = r;
        return
            RunRequest();
    }
}
