using System.Configuration;
using System.Net.Mime;
using System.Reflection.Metadata;
using FluentValidation;
using MediatR;
using MedicationSystem.Application;
using MedicationSystem.Application.Abstractions;
using MedicationSystem.Application.Behaviors;
using MedicationSystem.Application.Medications.Abstractions;
using MedicationSystem.Application.Medications.Commands;
using MedicationSystem.Application.Medications.Commands.Create;
using MedicationSystem.Infrastructure.Services;
using MedicationSystem.Persistence.EF;
using MedicationSystem.Persistence.EF.Medications;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
var connectionString = configuration.GetValue<string>("ConnectionString");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<EFWriteDbContext>(_ =>
    _.UseSqlServer(connectionString));
builder.Services.AddDbContext<EFReadDbContext>(_ =>
        _.UseSqlServer(connectionString)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly());
builder.Services.AddMediatR(_ =>
{
    _.RegisterServicesFromAssemblies(ApplicationAssemblyReference.Assembly());
    _.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});
    
builder.Services.AddSingleton<IDateTimeService,AppDateTimeService>();
builder.Services.AddScoped<IMedicationWriteRepository, EFMedicationWriteRepository>();
builder.Services.AddScoped<IMedicationReadRepository, EFMedicationReadRepository>();
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

ConfigGlobalExceptionHandler(app);

app.MapControllers();
app.Run();

void ConfigGlobalExceptionHandler(WebApplication webApplication)
{
    webApplication.UseExceptionHandler(_ => _.Run( async context => 
    {
        var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
        var errorType = exception?.GetType().Name.Replace("Exception", String.Empty);
        var errorDescription = exception.Message;
        var result = new
        {
            Error = errorType,
            Description = errorDescription
        };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsJsonAsync(result);
    }));
}
