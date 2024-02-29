using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TrackMagic.Shared.Constants;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Infrastructure.ExceptionHandling
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<NotFoundExceptionHandler> _logger;

        public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception ex,
            CancellationToken cancellationToken)
        {
            if (ex is not NotFoundException notFoundEx) return false;

            _logger.LogError($"Exception handled by Not Found Exception Handler.");
            var errorResult = new ErrorResult
            {
                Source = ex.TargetSite?.DeclaringType?.FullName,
                Exception = notFoundEx.GetType().Name,
                ErrorId = Guid.NewGuid().ToString(),
                SupportMessage = DefaultMessages.SupportMessage,
                Errors = new List<string>(),
            };

            errorResult.Errors!.Add(ex.Message);
            if (notFoundEx.ErrorMessages != null && notFoundEx.ErrorMessages.Any())
            {
                errorResult.Errors!.AddRange(notFoundEx.ErrorMessages);
            }

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = StatusCodes.Status404NotFound;

            _logger.LogError(notFoundEx, notFoundEx.Message);
            await response.WriteAsJsonAsync(errorResult, cancellationToken);

            return true;
        }
    }
}
