using FluentValidation;
using MedicationSystem.Application.Medications.Exceptions;

namespace MedicationSystem.Application.Medications.Commands.Create;

public class CreateMedicationCommandValidator : AbstractValidator<CreateMedicationCommand>
{
    public CreateMedicationCommandValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty().WithMessage(nameof(MedicationNameIsRequiredException))
            .Length(1, 50).WithMessage(nameof(MedicationNameShouldBeBetWeenOneAndFiftyCharacterException));

        RuleFor(m => m.Quantity)
            .GreaterThan(0).WithMessage(nameof(QuantityShouldBeGreaterThanZeroException));
    }
}