using MedicationSystem.Application.Medications.Queries;
using MedicationSystem.Persistence.EF;
using MedicationSystem.Persistence.EF.Medications;

namespace MedicationSystem.Tests.Helpers.Medications;

public static class GetAllMedicationQueryHandlerFactory
{
    public static GetAllMedicationQueryHandler Create(EFReadDbContext readDbContext)
    {
        var repository = new EFMedicationReadRepository(readDbContext);
        return new GetAllMedicationQueryHandler(repository);
    }
}