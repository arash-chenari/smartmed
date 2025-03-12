using System;
using MedicationSystem.Domain.Abstractions;
using MedicationSystem.Domain.Entities.Enums;

namespace MedicationSystem.Domain.Entities
{
    public class Medication : Entity
    {
        public Medication()
        {
        }
        
        public string Code { get;  set; }
        public string Name { get;  set; }
        public MedicationType Type { get;  set; }
        public DateTime CreationDate { get; set; }
        public int Quantity { get;  set; }
    }
}