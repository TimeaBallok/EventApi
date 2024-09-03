using FluentValidation;
using MediatR;

namespace EventAPI.Features.Common.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        // Variables.
        private readonly IEnumerable<IValidator<TRequest>> validators;

        // Constructors.
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => this.validators = validators;

        // Methods.
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validators.Any()) return await next();

            var errorsDictionary = validators
                .Select(validator => validator.Validate(new ValidationContext<TRequest>(request)))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .GroupBy(
                    validationFailure => validationFailure.PropertyName,
                    validationFailure => validationFailure.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(error => error.Key, error => error.Values);

            if (errorsDictionary.Any())
            {
                var exceptions = errorsDictionary.Select(error => new Exception(string.Join(',', error.Value.Select(value => value.ToString()))));
                throw new AggregateException(exceptions);
            }

            return await next();
        }
    }

}
