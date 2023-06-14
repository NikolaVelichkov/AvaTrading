using System.Net.Mail;
using System.Net;

namespace AvaTrading.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _senderEmail;
        private readonly string _senderName;

        public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, string senderEmail, string senderName)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
            _senderEmail = senderEmail;
            _senderName = senderName;
        }

        public async Task<bool> SendEmail(string email, string subject, string body)
        {
            try
            {
                // Create a new MailMessage
                var message = new MailMessage();
                message.From = new MailAddress(_senderEmail, _senderName);
                message.To.Add(email);
                message.Subject = subject;
                message.Body = subject;

                // Configure the SMTP client
                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    smtpClient.EnableSsl = true;
                    await smtpClient.SendMailAsync(message);
                }
                //var smtpClient = new SmtpClient(_smtpServer, _smtpPort);
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                //smtpClient.EnableSsl = true;

                // Send the email

                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions or logging
                return false;
            }
        }
    }
}
