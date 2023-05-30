using FluentValidation;
using MediatR;

namespace TestPoint.Application.Pipeline;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var failures = _validators
            .Select(validator => validator.Validate(request))
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .Distinct()
            .ToArray();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return next();
    }
}
