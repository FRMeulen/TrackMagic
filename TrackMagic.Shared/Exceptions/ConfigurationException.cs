using TrackMagic.Shared.Exceptions.Base;

namespace TrackMagic.Shared.Exceptions
{
    public class ConfigurationException : ApplicationSetupException
    {
        public ConfigurationException(string configName) : base("Configuration Error", $"Config section {configName} was not configured.") { }
    }
}
