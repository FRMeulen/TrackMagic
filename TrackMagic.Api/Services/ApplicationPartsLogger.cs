using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace TrackMagic.Api.Services
{
    public class ApplicationPartsLogger : IHostedService
    {
        private readonly ILogger<ApplicationPartsLogger> _logger;
        private readonly ApplicationPartManager _partsManager;

        public ApplicationPartsLogger(ILogger<ApplicationPartsLogger> logger, ApplicationPartManager partManager)
            => (_logger, _partsManager) = (logger, partManager);

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var applicationParts = _partsManager.ApplicationParts.Select(x => x.Name).Where(x => x.Contains("TrackMagic"));

            var controllerFeature = new ControllerFeature();
            _partsManager.PopulateFeature(controllerFeature);

            var controllers = controllerFeature.Controllers.Select(x => x.Name);

            _logger.LogInformation($"Found application parts: {string.Join(", ", applicationParts)}");
            _logger.LogInformation($"Containing controllers: {string.Join(", ", controllers)}");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
