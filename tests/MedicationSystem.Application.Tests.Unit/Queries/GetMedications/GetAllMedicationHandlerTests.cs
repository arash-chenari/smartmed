using FluentAssertions;
using MedicationSystem.Application.Medications.Abstractions;
using MedicationSystem.Application.Medications.Queries;
using MedicationSystem.Domain.Entities;
using MedicationSystem.Persistence.EF.Medications;
using MedicationSystem.Tests.Helpers.Infrastructure;
using MedicationSystem.Tests.Helpers.Medications;
using SQLitePCL;

namespace MedicationSystem.Application.Tests.Unit.Medications.Queries.GetMedications;

public class GetAllMedicationQueryHandlerTests : InMemoryDatabaseFixture
{
    private readonly  GetAllMedicationQueryHandler _sut;
    
    public GetAllMedicationQueryHandlerTests()
    {
        _sut = GetAllMedicationQueryHandlerFactory.Create(ReadDbContext);
    }
    
    [Fact]
    public async Task GetAllMedicationQueryHandler_Returns_All_Medications()
    {
        var medications = new List<Medication>()
        {
            new MedicationBuilder().WithCode("1").Build(),
            new MedicationBuilder().WithCode("2").Build()
        };
        writeDbContext.Manipulate(_=> _.Medications.AddRange(medications));
        var query = new GetAllMedicationsQuery();
        var expected = await _sut.Handle(query, CancellationToken.None);

        expected.Count().Should().Be(2);
    }

    [Fact]
    public async Task GetAllMedicationQueryHandler_Returns_All_Medication_Properties_Properly()
    {
        var medication = new MedicationBuilder().Build();
        writeDbContext.Manipulate(_ => _.Medications.Add(medication));
        var query = new GetAllMedicationsQuery();

        var expected = await _sut.Handle(query, CancellationToken.None);

        var singleMedication = expected.Single();
        singleMedication.Should().NotBeNull();
        singleMedication.CreationDate.Should().Be(medication.CreationDate);
        singleMedication.Code.Should().Be(medication.Code);
        singleMedication.Id.Should().Be(medication.Id);
        singleMedication.Name.Should().Be(medication.Name);
        singleMedication.Quantity.Should().Be(medication.Quantity);
        singleMedication.Type.Should().Be(medication.Type);
    }
}