using MedicationSystem.Application.Abstractions;
using MedicationSystem.Application.Medications.Abstractions;
using MedicationSystem.Application.Medications.Commands.Create;
using MedicationSystem.Persistence.EF;
using MedicationSystem.Persistence.EF.Medications;

namespace MedicationSystem.Tests.Helpers.Medications;

public static class CreateMedicationCommandHandlerFactory
{
    public static CreateMedicationCommandHandler Create(EFWriteDbContext writeDbContext, IDateTimeService dateTimeService)
    {
        IMedicationWriteRepository repository = new EFMedicationWriteRepository(writeDbContext);
        IUnitOfWork unitOfWork = new EFUnitOfWork(writeDbContext);
        return new CreateMedicationCommandHandler(repository, unitOfWork,dateTimeService);
    }
}