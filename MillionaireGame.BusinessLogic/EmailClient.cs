using System;
using System.Net;
using System.Net.Mail;

namespace MillionaireGame.BusinessLogic
{
    public class EmailClient
    {
        private readonly EmailConfigurationElement _emailSettings = EmailConfigurationSection.Instance.Email;
        private readonly SmtpClient _smtp;
        private readonly MailAddress _fromAddress;

        public EmailClient()
        {
            _fromAddress = new MailAddress(_emailSettings.MailFromAddress, _emailSettings.MailFromName);
            var credential = new NetworkCredential(_fromAddress.Address, _emailSettings.Password);

            _smtp = new SmtpClient
            {
                Host = _emailSettings.Host,
                Port = _emailSettings.Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 30000,
                UseDefaultCredentials = false,
                Credentials = credential
            };
        }

        public void Send(string recipientEmail, string title, string text)
        {
            var toAddress = new MailAddress(recipientEmail);

            using (var message = new MailMessage(_fromAddress, toAddress) { Subject = title, Body = text })
            {
                try
                {
                    _smtp.Send(message);
                }
                catch (Exception)
                {
                    // TODO: some exception handling
                }
            }
        }
    }
}