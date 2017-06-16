using System.Configuration;

namespace MillionaireGame.BusinessLogic
{
    public sealed class EmailConfigurationSection : ConfigurationSection
    {
        private static EmailConfigurationSection _instance;

        [ConfigurationProperty(nameof(Email), IsRequired = true)]
        public EmailConfigurationElement Email
        {
            get => (EmailConfigurationElement)base[nameof(Email)];
            set => base[nameof(Email)] = value;
        }

        public static EmailConfigurationSection Instance => _instance ?? (_instance =
                                                                (EmailConfigurationSection)ConfigurationManager.GetSection(
                                                                    nameof(EmailConfigurationSection)));

        private EmailConfigurationSection() { }
    }
}