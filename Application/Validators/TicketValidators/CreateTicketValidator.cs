using Domain.Dtos.TicketDtos;
using FluentValidation;

namespace Application.Validators.TicketValidators;

public class CreateTicketValidator: AbstractValidator<CreateTicketDto>
{
    public CreateTicketValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El asunto del ticket no puede estar vacío.")
            .MinimumLength(10).WithMessage("El asunto debe tener al menos 10 caracteres.")
            .MaximumLength(50).WithMessage("El asunto debe tener menos de 50 caracteres.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripcion del ticket no puede estar vacío.")
            .MinimumLength(10).WithMessage("La descripcion debe tener al menos 10 caracteres.")
            .MaximumLength(1000).WithMessage("La descripcion debe tener menos de 1000 caracteres.");
        RuleFor(x => x.TypeTicketId)
            .NotEmpty().WithMessage("El tipo de ticket es requerido");
    }
}