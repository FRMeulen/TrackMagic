using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TrackMagic.Shared.Constants;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Infrastructure.ExceptionHandling
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        private readonly List<Type> SeparatelyHandledExceptionTypes = new List<Type>
        {
            typeof(BadRequestException),
            typeof(NotFoundException),
            typeof(ValidationException),
        };

        private readonly ILogger<DefaultExceptionHandler> _logger;

        public DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception ex,
            CancellationToken cancellationToken)
        {
            _logger.LogError($"Exception of type {ex.GetType().Name} handled by Default Exception Handler.");
            var errorResult = new ErrorResult
            {
                Source = ex.TargetSite?.DeclaringType?.FullName,
                Exception = ex.GetType().Name,
                ErrorId = Guid.NewGuid().ToString(),
                SupportMessage = DefaultMessages.SupportMessage,
            };

            errorResult.Errors!.Add(ex.Message);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = StatusCodes.Status500InternalServerError;

            _logger.LogError(ex, ex.Message);
            await response.WriteAsJsonAsync(errorResult, cancellationToken);

            return true;
        }
    }
}
