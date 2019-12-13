using app2md.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace app2md.Services
{
    public static class EmailService
    {
        public static void SendMail(ContactFormViewModel model, int contactFormId, IConfiguration configuration)
        {
            try
            {
                var smtpServer = configuration.GetSection("MailClient:SmtpServer").Value;
                var port = int.Parse(configuration.GetSection("MailClient:Port").Value);
                var username = configuration.GetSection("MailClient:Username").Value;
                var password = configuration.GetSection("MailClient:Password").Value;
                var recipient = configuration.GetSection("MailClient:RecipientEmail").Value;

                var sender = model.EmailAddress;

                string body = JsonConvert.SerializeObject(model);

                using (var client = new SmtpClient(smtpServer, port))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(username, password);
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(sender);
                    mailMessage.To.Add(recipient);
                    mailMessage.Body = body;
                    mailMessage.Subject = $"Record #{contactFormId}";
                    client.Send(mailMessage);
                }
            }
            catch
            {
                throw; // here should be logger
            }
        }
    }
}
