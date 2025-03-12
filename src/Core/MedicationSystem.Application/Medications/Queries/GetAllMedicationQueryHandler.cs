using System.Collections;
using MedicationSystem.Application.Abstractions;
using MedicationSystem.Application.Medications.Abstractions;
using MedicationSystem.Domain.Abstractions;

namespace MedicationSystem.Application.Medications.Queries;

public class GetAllMedicationQueryHandler: IQueryHandler<GetAllMedicationsQuery,IList<GetMedicationDto>>
{
    private readonly IMedicationReadRepository _repository;

    public GetAllMedicationQueryHandler(IMedicationReadRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IList<GetMedicationDto>> Handle(GetAllMedicationsQuery request, CancellationToken cancellationToken)
    {
        return  await _repository.GetAllMedications();
    }
}