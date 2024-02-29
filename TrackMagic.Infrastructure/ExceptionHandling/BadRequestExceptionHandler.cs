using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TrackMagic.Shared.Constants;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Infrastructure.ExceptionHandling
{
    public class BadRequestExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BadRequestExceptionHandler> _logger;

        public BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception ex,
            CancellationToken cancellationToken)
        {
            if (ex is not BadRequestException badRequestEx) return false;

            _logger.LogError($"Exception handled by Bad Request Exception Handler.");
            var errorResult = new ErrorResult
            {
                Source = ex.TargetSite?.DeclaringType?.FullName,
                Exception = badRequestEx.GetType().Name,
                ErrorId = Guid.NewGuid().ToString(),
                SupportMessage = DefaultMessages.SupportMessage,
            };

            errorResult.Errors!.Add(ex.Message);
            if (badRequestEx.ErrorMessages != null && badRequestEx.ErrorMessages.Any())
            {
                errorResult.Errors!.AddRange(badRequestEx.ErrorMessages);
            }

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = StatusCodes.Status400BadRequest;

            _logger.LogError(badRequestEx, badRequestEx.Message);
            await response.WriteAsJsonAsync(errorResult, cancellationToken);

            return true;
        }
    }
}
