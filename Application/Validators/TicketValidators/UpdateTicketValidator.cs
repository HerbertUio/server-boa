using Domain.Dtos.TicketDtos;
using FluentValidation;

namespace Application.Validators.TicketValidators;

public class UpdateTicketValidator : AbstractValidator<UpdateTicketDto>
{
    public UpdateTicketValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El asunto del ticket no puede estar vacío.")
            .MinimumLength(10).WithMessage("El asunto debe tener al menos 10 caracteres.")
            .MaximumLength(50).WithMessage("El asunto debe tener menos de 50 caracteres.");
            
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripcion del ticket no puede estar vacío.")
            .MinimumLength(10).WithMessage("La descripcion debe tener al menos 10 caracteres.")
            .MaximumLength(1000).WithMessage("La descripcion debe tener menos de 1000 caracteres.");
        
        RuleFor(x => x.PriorityId)
            .GreaterThan(0).WithMessage("El ID de prioridad debe ser mayor a 0.")
            .When(x => x.PriorityId.HasValue);
            
        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("El ID de estado debe ser mayor a 0.")
            .When(x => x.StatusId.HasValue);
            
        RuleFor(x => x.PrimaryTicketId)
            .GreaterThan(0).WithMessage("El ID del ticket primario debe ser mayor a 0.")
            .When(x => x.PrimaryTicketId.HasValue);
            
        RuleFor(x => x.ParentTicketId)
            .GreaterThan(0).WithMessage("El ID del ticket padre debe ser mayor a 0.")
            .When(x => x.ParentTicketId.HasValue);
            
        RuleFor(x => x.AssignedAgentId)
            .GreaterThan(0).WithMessage("El ID del agente asignado debe ser mayor a 0.")
            .When(x => x.AssignedAgentId.HasValue);
            
        RuleFor(x => x.AssignedGroupId)
            .GreaterThan(0).WithMessage("El ID del grupo asignado debe ser mayor a 0.")
            .When(x => x.AssignedGroupId.HasValue);
            
        RuleFor(x => x.TypeTicketId)
            .GreaterThan(0).WithMessage("El ID del tipo de ticket debe ser mayor a 0.")
            .When(x => x.TypeTicketId.HasValue);
            
        RuleFor(x => x.OfficeId)
            .GreaterThan(0).WithMessage("El ID de la oficina debe ser mayor a 0.")
            .When(x => x.OfficeId.HasValue);
            
        RuleFor(x => x.AreaId)
            .GreaterThan(0).WithMessage("El ID del área debe ser mayor a 0.")
            .When(x => x.AreaId.HasValue);
            
        RuleFor(x => x.SubjectId)
            .GreaterThan(0).WithMessage("El ID del asunto debe ser mayor a 0.")
            .When(x => x.SubjectId.HasValue);
            
        // Validación de lógica de negocio: no puede ser ticket padre e hijo a la vez
        RuleFor(x => x)
            .Must(x => !(x.PrimaryTicketId.HasValue && x.ParentTicketId.HasValue))
            .WithMessage("Un ticket no puede tener tanto un ticket primario como un ticket padre asignado.")
            .WithName("TicketHierarchy");
    }
}