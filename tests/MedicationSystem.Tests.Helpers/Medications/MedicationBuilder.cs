using MedicationSystem.Domain.Entities;
using MedicationSystem.Domain.Entities.Enums;

namespace MedicationSystem.Tests.Helpers.Medications;

public class MedicationBuilder
{
    private Medication _medication = new Medication
    {
        Code = "Dummy",
        Quantity = 1,
        Name = "dummy",
        Type = MedicationType.Capsules,
        CreationDate = DateTime.Now
    };

    public MedicationBuilder WithName(string name)
    {
        _medication.Name = name;
        return this;
    }

    public MedicationBuilder WithCode(string code)
    {
        _medication.Code = code;
        return this;
    }

    public MedicationBuilder WithCreationDate(DateTime dateTime)
    {
        _medication.CreationDate = dateTime;
        return this;
    }

    public MedicationBuilder WithMedicationType(MedicationType type)
    {
        _medication.Type = type;
        return this;
    }

    public Medication Build()
    {
        return _medication;
    }
}