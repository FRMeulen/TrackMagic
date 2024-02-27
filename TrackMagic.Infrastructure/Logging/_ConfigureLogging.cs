using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Infrastructure.Logging
{
    public static class _ConfigureLogging
    {
        public static IServiceCollection AddLoggingServices(this IServiceCollection services, IConfiguration config)
        {
            var loggingSettings = config.GetSection("LoggingSettings").Get<LoggingSettings>();
            if (loggingSettings == null) throw new ConfigurationException(nameof(LoggingSettings));
            
            if (loggingSettings.UseConsole)
            {
                services.AddLogging(opt => opt.AddConsole());
            }

            return services;
        }
    }
}
