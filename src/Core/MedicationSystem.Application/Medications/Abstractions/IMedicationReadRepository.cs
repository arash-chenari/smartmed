using MedicationSystem.Application.Medications.Queries;

namespace MedicationSystem.Application.Medications.Abstractions
{
    public interface IMedicationReadRepository
    {
        Task<IList<GetMedicationDto>> GetAllMedications();
    }
}