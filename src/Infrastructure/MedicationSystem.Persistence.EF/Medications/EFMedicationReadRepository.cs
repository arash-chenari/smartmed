using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicationSystem.Application.Medications.Abstractions;
using MedicationSystem.Application.Medications.Queries;
using Microsoft.EntityFrameworkCore;

namespace MedicationSystem.Persistence.EF.Medications;
public class EFMedicationReadRepository : IMedicationReadRepository
{
    private readonly EFReadDbContext _dbContext;

    public EFMedicationReadRepository(EFReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<GetMedicationDto>> GetAllMedications()
    {
        return await _dbContext.Medications.Select(_ => new GetMedicationDto
        {
            Id = _.Id,
            Code = _.Code,
            Name = _.Name,
            Quantity = _.Quantity,
            Type = _.Type,
            CreationDate = _.CreationDate
        }).ToListAsync();
    }
}