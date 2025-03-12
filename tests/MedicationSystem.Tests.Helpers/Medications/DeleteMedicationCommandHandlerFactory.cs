using MedicationSystem.Application.Abstractions;
using MedicationSystem.Application.Medications.Abstractions;
using MedicationSystem.Application.Medications.Commands.Delete;
using MedicationSystem.Persistence.EF;
using MedicationSystem.Persistence.EF.Medications;

namespace MedicationSystem.Tests.Helpers.Medications;

public static class DeleteMedicationCommandHandlerFactory
{
    public static DeleteMedicationCommandHandler Create(EFWriteDbContext writeDbContext)
    {
        IMedicationWriteRepository repository = new EFMedicationWriteRepository(writeDbContext);
        IUnitOfWork unitOfWork = new EFUnitOfWork(writeDbContext);
        return new DeleteMedicationCommandHandler(repository, unitOfWork);
    }
}