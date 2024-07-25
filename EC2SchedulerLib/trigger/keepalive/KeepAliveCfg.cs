namespace EC2SchedulerLib.trigger.keepalive;
public class KeepAliveCfg
{
    public required string EmailAtendimento;
    public required string EmailAtendimentoNome;
    public required string EmailHost;
    public int EmailPort;
    public required string EmailAdmin;
    public required string EmailEncryptedPassword;
    public required string AlertEmails;
    public required string[] TargetUrls;
}