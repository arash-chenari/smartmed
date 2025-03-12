using MedicationSystem.Application.Abstractions;

namespace MedicationSystem.Application.Medications.Commands.Delete;

public record DeleteMedicationCommand(int Id) : ICommand
{
}