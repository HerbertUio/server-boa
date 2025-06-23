using Domain.Dtos.TicketDtos;
using FluentValidation;

namespace Application.Validators.TicketValidators;

public class UnmergeTicketValidator: AbstractValidator<UnmergeTicketsDto>
{
    public UnmergeTicketValidator()
    {
        RuleFor(x => x.TicketId)
            .GreaterThan(0).WithMessage("El ID del ticket principal debe ser mayor a 0.");

        RuleFor(x => x.TicketToUnmergeId)
            .GreaterThan(0).WithMessage("El ID del ticket a desfusionar debe ser mayor a 0.");

        RuleFor(x => x)
            .Must(x => x.TicketId != x.TicketToUnmergeId)
            .WithMessage("Un ticket no puede desfusionarse de sí mismo.")
            .WithName("SelfUnmerge");
    }
}