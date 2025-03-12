using FluentAssertions;
using MedicationSystem.Application.Abstractions;
using MedicationSystem.Application.Medications.Abstractions;
using MedicationSystem.Application.Medications.Commands.Create;
using MedicationSystem.Application.Medications.Exceptions;
using MedicationSystem.Domain.Entities;
using MedicationSystem.Domain.Entities.Enums;
using MedicationSystem.Persistence.EF.Medications;
using MedicationSystem.Tests.Helpers.Infrastructure;
using MedicationSystem.Tests.Helpers.Medications;
using Moq;

namespace MedicationSystem.Application.Tests.Unit.Medications.Commands.Create;

public class CreateMedicationTests : InMemoryDatabaseFixture
{
    private readonly ICommandHandler<CreateMedicationCommand> _sut;
    private readonly Mock<IDateTimeService> _dateTimeService;
    public CreateMedicationTests()
    {
        _dateTimeService = new Mock<IDateTimeService>();
        _sut = CreateMedicationCommandHandlerFactory.Create(writeDbContext,_dateTimeService.Object);
    }

    [Fact]
    public async void CreateMedicationCommandHandler_Creates_New_Medication()
    {
        var dummyDateTime = new DateTime(2024, 1, 1, 10, 10, 00);
        _dateTimeService.Setup(_ => _.Now()).Returns(dummyDateTime);
        var command = CreateMedicationCommandFactory.Create();

        await _sut.Handle(command,CancellationToken.None);

        var expected = writeDbContext.Medications
            .SingleOrDefault(_ => _.Code == command.Code);
        expected.Should().NotBeNull();
        expected.Code.Should().Be(command.Code);
        expected.Name.Should().Be(command.Name);
        expected.Type.Should().Be(command.Type);
        expected.Quantity.Should().Be(command.Quantity);
        expected.CreationDate.Should().Be(dummyDateTime);
    }

    [Fact]
    public async void CreateMedicationHandler_Throws_MedicationCodeShouldNotBeDuplicatedException_When_Medication_With_Given_Code_Is_Exist()
    {
        string medicationCode = "abc";
        var medication = new MedicationBuilder().WithCode(medicationCode).Build();
        writeDbContext.Manipulate(_ => _.Medications.Add(medication));
        var command = CreateMedicationCommandFactory.Create(code: medicationCode);

       Func<Task> expected = () => _sut.Handle(command, CancellationToken.None);

       await expected.Should()
           .ThrowExactlyAsync<MedicationCodeShouldNotBeDuplicatedException>();
    }
}