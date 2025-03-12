using FluentAssertions;
using MedicationSystem.Application.Medications.Commands.Delete;
using MedicationSystem.Application.Medications.Exceptions;
using MedicationSystem.Tests.Helpers.Infrastructure;
using MedicationSystem.Tests.Helpers.Medications;

namespace MedicationSystem.Application.Tests.Unit.Medications.Commands.Delete;

public class DeleteMedicationTests : InMemoryDatabaseFixture
{
    private readonly DeleteMedicationCommandHandler _sut;

    public DeleteMedicationTests()
    {
        _sut = DeleteMedicationCommandHandlerFactory.Create(writeDbContext);
    }

    [Fact]
    public async Task DeleteMedicationCommandHandler_Deletes_Mediation()
    {
        var medication = new MedicationBuilder().Build();
        writeDbContext.Manipulate(_ => _.Medications.Add(medication));
        var deleteMedicationCommand = new DeleteMedicationCommand(medication.Id);

       await _sut.Handle(deleteMedicationCommand, CancellationToken.None);

       var actual = writeDbContext.Medications
           .FirstOrDefault(_ => _.Id == medication.Id);

       actual.Should().BeNull();
    }

    [Fact]
    public async Task DeleteMedicationHandler_Throws_MedicationNotFoundException_When_Medication_With_Given_Id_Doesnt_Exist()
    {
        var dummyMedicationId = 100;
        var deleteMedicationCommand = new DeleteMedicationCommand(dummyMedicationId);

        Func<Task> expected = ()=>  _sut.Handle(deleteMedicationCommand, CancellationToken.None);

        await expected.Should().ThrowExactlyAsync<MedicationNotFoundException>();
    }
}