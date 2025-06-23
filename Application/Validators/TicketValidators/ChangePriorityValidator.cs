using Domain.Dtos.TicketDtos;
using Domain.Enums.TicketEnums;
using FluentValidation;

namespace Application.Validators.TicketValidators;

public class ChangePriorityValidator: AbstractValidator<ChangePriorityDto>
{
    public ChangePriorityValidator()
    {
        RuleFor(x => x.NewPriorityId)
            .NotEmpty().WithMessage("La nueva prioridad es obligatoria.")
            .Must(BeAValidPriority).WithMessage("El ID de prioridad no es un valor válido.");
    }
    private bool BeAValidPriority(int priorityId)
    {
        return Enum.IsDefined(typeof(Priority), priorityId);
    }
}