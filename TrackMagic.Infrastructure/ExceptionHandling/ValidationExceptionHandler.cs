using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Infrastructure.ExceptionHandling
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ValidationExceptionHandler> _logger;

        public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception ex,
            CancellationToken cancellationToken)
        {
            if (ex is not ValidationException validationEx) return false;

            _logger.LogError($"Exception handled by Validation Exception Handler.");
            var errors = validationEx.Errors.GroupBy(x => x.PropertyName)
                .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage).ToArray());

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Type = $"{ex.GetType().Name}",
                Title = DefaultMessages.ValidationExceptionTitle,
                Detail = DefaultMessages.ValidationExceptionMessage,
                Instance = context.Request.Path,
                Status = StatusCodes.Status400BadRequest,
            };

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = StatusCodes.Status400BadRequest;

            await response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
