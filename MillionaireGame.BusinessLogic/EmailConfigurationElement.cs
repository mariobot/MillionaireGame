using System.Configuration;

namespace MillionaireGame.BusinessLogic
{
    public class EmailConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty(nameof(MailFromAddress), IsRequired = true)]
        public string MailFromAddress
        {
            get => (string)base[nameof(MailFromAddress)];
            set => base[nameof(MailFromAddress)] = value;
        }

        [ConfigurationProperty(nameof(MailFromName))]
        public string MailFromName
        {
            get => (string)base[nameof(MailFromName)];
            set => base[nameof(MailFromName)] = value;
        }

        [ConfigurationProperty(nameof(Host), IsRequired = true)]
        public string Host
        {
            get => (string)base[nameof(Host)];
            set => base[nameof(Host)] = value;
        }

        [ConfigurationProperty(nameof(Port), IsRequired = true)]
        [IntegerValidator(MinValue = 0, MaxValue = 65535)]
        public int Port
        {
            get => (int)base[nameof(Port)];
            set => base[nameof(Port)] = value;
        }

        [ConfigurationProperty(nameof(Password), IsRequired = true)]
        public string Password
        {
            get => (string)base[nameof(Password)];
            set => base[nameof(Password)] = value;
        }
    }
}