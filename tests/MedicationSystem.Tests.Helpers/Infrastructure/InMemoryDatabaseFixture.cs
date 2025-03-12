using MedicationSystem.Application.Abstractions;
using MedicationSystem.Persistence.EF;

namespace MedicationSystem.Tests.Helpers.Infrastructure;

public class InMemoryDatabaseFixture
{
    protected readonly EFWriteDbContext writeDbContext;
    protected readonly EFReadDbContext ReadDbContext;

    public InMemoryDatabaseFixture()
    {
        writeDbContext = new EFInMemoryDatabase().CreateDataContext<EFWriteDbContext>();
        ReadDbContext = new EFInMemoryDatabase().CreateDataContext<EFReadDbContext>();
    }
}