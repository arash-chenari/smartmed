using MedicationSystem.Application.Abstractions;
using MedicationSystem.Application.Medications.Abstractions;
using MedicationSystem.Application.Medications.Exceptions;
using MedicationSystem.Domain.Entities;

namespace MedicationSystem.Application.Medications.Commands.Create;

public class CreateMedicationCommandHandler : ICommandHandler<CreateMedicationCommand>
{
    private readonly IMedicationWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;
    public CreateMedicationCommandHandler(
                IMedicationWriteRepository writeRepository,
                IUnitOfWork unitOfWork,
                IDateTimeService dateTimeService)
    {
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }
    
    public async Task Handle(CreateMedicationCommand command,
                            CancellationToken cancellationToken)
    {
        await PreventToCreateMedicationWithDuplicateCode(command.Code);

        var medication = new Medication()
        {
            Code = command.Code,
            Quantity = command.Quantity,
            Name = command.Name,
            Type = command.Type,
            CreationDate = _dateTimeService.Now()
        };
        
        _writeRepository.Add(medication);
        await _unitOfWork.CompleteAsync();
    }

    private async Task PreventToCreateMedicationWithDuplicateCode(string code)
    {
        var isMedicationWithSameCodeExist =
            await _writeRepository.IsMedicationWithSameCodeExist(code);
       
        if (isMedicationWithSameCodeExist)
        {
            throw new MedicationCodeShouldNotBeDuplicatedException();
        }
    }
}