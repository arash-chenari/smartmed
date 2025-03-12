using System.Windows.Input;
using FluentValidation;
using MediatR;

namespace MedicationSystem.Application.Behaviors;

public class ValidationBehavior<TCommand,TResponse> : IPipelineBehavior<TCommand, TResponse >
where TCommand : IRequest
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TCommand>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TCommand request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken= default)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TCommand>(request);
            
            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));
            
            var failures = validationResults
                .Where(r => r.Errors.Count != 0)
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        return await next();
    }
}