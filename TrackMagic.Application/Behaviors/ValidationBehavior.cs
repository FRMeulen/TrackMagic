﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TrackMagic.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILoggerFactory loggerFactory)
        {
            _validators = validators;
            _logger = loggerFactory.CreateLogger<ValidationBehavior<TRequest, TResponse>>();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request of type {request.GetType().Name} passed through validation.");
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationTasks = _validators.Select(x => x.ValidateAsync(request, cancellationToken)).ToList();
            var errors = (await Task.WhenAll(validationTasks)).SelectMany(x => x.Errors).ToList();

            if (errors.Any()) throw new ValidationException(errors);

            return await next();
        }
    }
}
