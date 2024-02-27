using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TrackMagic.Shared.Constants;
using TrackMagic.Shared.Exceptions;
using TrackMagic.Shared.Exceptions.Base;

namespace TrackMagic.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(IHostEnvironment environment) => _environment = environment;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (RequestException ex)
            {
                await HandleNonValidationExceptionAsync(context, ex);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            var errors = ex.Errors.GroupBy(x => x.PropertyName)
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

            await response.WriteAsJsonAsync(problemDetails);
        }

        private async Task HandleNonValidationExceptionAsync(HttpContext context, RequestException ex)
        {
            var errorResult = new ErrorResult
            {
                Source = ex.TargetSite?.DeclaringType?.FullName,
                Exception = _environment.IsDevelopment() ? ex.ToString() : ex.Message.Trim(),
                ErrorId = Guid.NewGuid().ToString(),
                SupportMessage = DefaultMessages.SupportMessage,
            };

            errorResult.Errors!.Add(ex.Message);
            if (ex.ErrorMessages != null && ex.ErrorMessages.Any())
            {
                errorResult.Errors!.AddRange(ex.ErrorMessages);
            }

            var response = context.Response;
            response.ContentType = "application/json";

            switch (ex)
            {
                case NotFoundException:
                    response.StatusCode = errorResult.StatusCode = StatusCodes.Status404NotFound;
                    break;

                case BadRequestException:
                    response.StatusCode = errorResult.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                default:
                    response.StatusCode = errorResult.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            await response.WriteAsJsonAsync(errorResult);
        }
    }
}
