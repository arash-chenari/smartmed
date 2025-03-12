using System;
using MedicationSystem.Application.Abstractions;

namespace MedicationSystem.Infrastructure.Services;

public class AppDateTimeService : IDateTimeService
{
    public DateTime Now()
    {
        return DateTime.UtcNow;
    }
}