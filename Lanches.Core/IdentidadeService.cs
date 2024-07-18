using System.Net.Mail;
using System.Net;
using Lanches.Core.Entities;

namespace Lanches.Application.Services
{
    public class IdentidadeService
    {
        public async Task SendEmailAsync(User user,string subject, string body)
        {
            if (user.Email != null)
            {
                try
                {
                    string smtpServer = "smtp.office365.com";
                    int smtpPort = 587;
                    string userName = "LanchesAPI@outlook.com";
                    string password = "02252713Fc!";

                    SmtpClient client = new SmtpClient(smtpServer, smtpPort);
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(userName, password);

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("LanchesAPI@outlook.com");
                    message.To.Add(user.Email);
                    message.Subject = subject;
                    message.Body = string.Format(body,user.UserName);
                    message.IsBodyHtml = true;
                    client.Send(message);
                }
                catch (Exception ex)
                {
                }
            }
            else
                throw new Exception("Usuário não possui e-mail cadastrado.");
        }
    }
}
