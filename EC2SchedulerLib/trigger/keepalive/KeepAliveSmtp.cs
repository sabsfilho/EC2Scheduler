using System.Net;
using System.Net.Mail;

namespace EC2SchedulerLib.trigger.keepalive;
class KeepAliveSmtp
{
    public KeepAliveCfg? Cfg { get; set; }
    object locker = new object();
    DateTime last;
    public void SendMessage(string url)
    {
        lock (locker)
        {
            if (
                Cfg == null ||
                string.IsNullOrEmpty(Cfg.AlertEmails) ||
                (DateTime.Now - last).TotalMinutes < 15
            ) return;
            last = DateTime.Now;

            string id = DateTime.Now.Ticks.ToString();
            var smtp = new SmtpClient(Cfg.EmailHost, Cfg.EmailPort);
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(Cfg.EmailAdmin, EncryptFromStringBytes(Cfg.EmailEncryptedPassword));
            MailMessage mm = new MailMessage()
            {
                From = new MailAddress(Cfg.EmailAtendimento, Cfg.EmailAtendimentoNome),
                Subject = $"{url} => SEM SINAL !",
                Body = $"Monitoramento não conseguiu acessar serviço em {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}.",
                IsBodyHtml = false
            };

            var emails = Cfg.AlertEmails.Split(new char[] { ',', ';' });
            foreach (string a in emails)
                mm.To.Add(a);

            smtp.Send(mm);

            Thread.Sleep(1000);
        }
    }

    public static string EncryptFromStringBytes(string val)
    {
        return ModelLib.Security.encryptFromStringBytes(val);
    }

}