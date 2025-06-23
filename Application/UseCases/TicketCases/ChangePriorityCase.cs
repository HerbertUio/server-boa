using Domain.Dtos.TicketDtos;
using Domain.Enums.TicketEnums;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.UseCases.TicketCases;

public class ChangePriorityCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<ChangePriorityDto> _validator;
    
    public ChangePriorityCase(ITicketRepository ticketRepository, IValidator<ChangePriorityDto> validator)
    {
        _ticketRepository = ticketRepository;
        _validator = validator;
    }


    public async Task<TicketModel> ExecuteAsync(int ticketId, ChangePriorityDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var ticketToChange = await _ticketRepository.GetByIdAsync(ticketId);

        if (ticketToChange is null)
        {
            throw new KeyNotFoundException($"El ticket con ID {ticketId} no fue encontrado.");
        }
        if (!Enum.IsDefined(typeof(Priority), dto.NewPriorityId))
        {
            throw new ArgumentException($"El ID de prioridad '{dto.NewPriorityId}' no es válido.");
        }
        var newPriority = (Priority)dto.NewPriorityId;
        ticketToChange.ChangePriority(newPriority);
        var updatedTicket = await _ticketRepository.UpdateAsync(ticketToChange);
        if (updatedTicket is null)
        {
            throw new Exception("Error al cambiar la prioridad del ticket.");
        }
        return updatedTicket;
    }
}