using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Advance
{
    public class SmtpClientAdvance
    {
        private readonly string host;
        private readonly int port;
        private readonly string address;
        private readonly string password;
        public SmtpClientAdvance(string host, int port, string address, string password)
        {
            this.host = host;
            this.port = port;
            this.address = address;
            this.password = password;
        }
        public void SendMail(string disPlayName, string mailAddress, string subject, string body)
        {
            using (var mailMessage = new MailMessage())
            {
                var fromMailAddress = new MailAddress(this.address, disPlayName);
                mailMessage.From = fromMailAddress;
                var toMailAddress = new MailAddress(mailAddress);
                mailMessage.To.Add(toMailAddress);

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                using (var smtpClient = new SmtpClient(this.host, this.port))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(this.address, this.password);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
