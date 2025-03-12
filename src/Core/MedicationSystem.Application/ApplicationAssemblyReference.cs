using System.Reflection;

namespace MedicationSystem.Application;

public static class ApplicationAssemblyReference
{
    public static Assembly Assembly()
    {
        return typeof(ApplicationAssemblyReference).Assembly;
    }
}