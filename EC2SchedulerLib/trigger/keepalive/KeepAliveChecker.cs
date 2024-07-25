namespace EC2SchedulerLib.trigger.keepalive;

class KeepAliveChecker
{
    const string NOK = "NOK";
    public KeepAliveCfg? Cfg { get; set; }
    public required string Url { get; set; }
    public required SchedulerRequest SchedulerRequest { get; set; }

    KeepAliveSmtp smtp;
    public KeepAliveChecker()
    {
        smtp = 
            new KeepAliveSmtp()
            {
                Cfg = Cfg                
            };
    }
    public void Load()
    {
        using (HttpClient client = new HttpClient())
        {
            var t = client.GetStringAsync(Url);
            t.ContinueWith((x) =>
            {
                string z = 
                    (t.IsFaulted || t.IsCanceled) ? 
                    NOK : 
                    string.Concat(t.Result.Substring(0, Math.Min(10, t.Result.Length)), " ...");
                SchedulerRequest.AddLog(string.Concat(Url, ": ", z));
                if (z == NOK)
                {
                    smtp.SendMessage(Url);
                }
            }).Wait();
            Thread.Sleep(1000);
        }

    }
}