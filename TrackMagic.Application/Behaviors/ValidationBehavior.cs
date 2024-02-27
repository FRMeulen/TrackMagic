using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger logger) => (_validators, _logger) = (validators, logger);

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation(DefaultMessages.LoggingPipelineMessage(nameof(request)));
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);

            var errors = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null);

            if (errors.Any()) throw new ValidationException(errors);

            return await next();
        }
    }
}
