using Domain.Dtos.TicketDtos;
using FluentValidation;

namespace Application.Validators.TicketValidators;

public class MergeTicketsValidator: AbstractValidator<MergeTicketsDto>
{
    public MergeTicketsValidator()
    {
        RuleFor(x => x.PrimaryTicketId)
            .GreaterThan(0).WithMessage("El ID del ticket primario debe ser mayor a 0.");

        RuleFor(x => x.TicketToMergeId)
            .GreaterThan(0).WithMessage("El ID del ticket a fusionar debe ser mayor a 0.");

        RuleFor(x => x)
            .Must(x => x.PrimaryTicketId != x.TicketToMergeId)
            .WithMessage("Un ticket no puede fusionarse consigo mismo.")
            .WithName("SelfMerge");
    }
}