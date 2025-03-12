using FluentAssertions;
using MedicationSystem.Application.Medications.Commands.Create;
using MedicationSystem.Domain.Entities.Enums;
using FluentValidation.TestHelper;
using MedicationSystem.Application.Medications.Exceptions;
using MedicationSystem.Tests.Helpers.Medications;

namespace MedicationSystem.Application.Tests.Unit.Medications.Commands.Create;

public class CreateMedicationCommandValidatorTests
{
    private readonly CreateMedicationCommandValidator _sut = new();

    [Fact]
    public void Should_Pass_When_Command_Is_Valid()
    {
        var command = new CreateMedicationCommand
        {
            Code = "123",
            Quantity = 10,
            Type = MedicationType.Capsules,
            Name = "Dummy"
        };

        var expected = _sut.TestValidate(command);

        expected.ShouldNotHaveAnyValidationErrors();
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Should_Fail_When_Quantity_Is_Zero_Or_Negetive(int quantity)
    {
        var createMedicationCommand = CreateMedicationCommandFactory.Create(quantity: quantity);

        var expected = _sut.TestValidate(createMedicationCommand);

        expected.ShouldHaveValidationErrorFor(_ => _.Quantity)
            .WithErrorMessage(nameof(QuantityShouldBeGreaterThanZeroException));
    }

    [Fact]
    public void Should_Fail_When_Command_Name_Is_Empty()
    {
        var createMedicationCommand = CreateMedicationCommandFactory.Create(name: string.Empty);

        var expected = _sut.TestValidate(createMedicationCommand);
        expected.ShouldHaveValidationErrorFor(_ => _.Name)
            .WithErrorMessage(nameof(MedicationNameIsRequiredException));
    }
    
    [Fact]
    public void Should_Fail_When_Command_Name_Characters_Is_More_Than_Fifty()
    {
        var createMedicationCommand = CreateMedicationCommandFactory.Create(name: new string('m',100));

        var expected = _sut.TestValidate(createMedicationCommand);
        expected.ShouldHaveValidationErrorFor(_ => _.Name)
            .WithErrorMessage(nameof(MedicationNameShouldBeBetWeenOneAndFiftyCharacterException));
    }
}