using System;
using System.Net;
using System.Net.Mail;
using MillionaireGame.BusinessLogic.Abstract;

namespace MillionaireGame.BusinessLogic.Concrete
{
    public class EmailClient : IMessageService
    {
        private EmailConfigurationElement _emailSettings;
        private readonly SmtpClient _smtp;

        public EmailClient()
        {
            _smtp = new SmtpClient
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 20000,
                UseDefaultCredentials = false,
            };
        }

        public void SendMessage(string text, string recipientEmail)
        {
            // it would be better to initialize this field in ctor
            // but it was a trouble that EmailConfigurationSection
            // cannot be initializes during app startup because of
            // Ninject binging
            _emailSettings = EmailConfigurationSection.Instance.Email;

            var fromAddress = new MailAddress(_emailSettings.MailFromAddress, _emailSettings.MailFromName);
            var toAddress = new MailAddress(recipientEmail);
            var credential = new NetworkCredential(fromAddress.Address, _emailSettings.Password);

            _smtp.Host = _emailSettings.Host;
            _smtp.Port = _emailSettings.Port;
            _smtp.Credentials = credential;

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = _emailSettings.MailFromName,
                Body = text
            })
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