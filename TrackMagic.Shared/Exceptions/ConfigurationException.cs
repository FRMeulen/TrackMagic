namespace TrackMagic.Shared.Exceptions
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string configName) : base($"Config section {configName} was not configured.") { }
    }
}
