

using Domain.Dtos.TicketDtos;
using Domain.Enums.TicketEnums;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.UseCases.TicketCases;

public class ChangeStatusCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<ChangeStatusDto> _validator;
    
    public ChangeStatusCase(ITicketRepository ticketRepository, IValidator<ChangeStatusDto> validator)
    {
        _ticketRepository = ticketRepository;
        _validator = validator;
    }
    
    public async Task<TicketModel> ExecuteAsync(int ticketId, ChangeStatusDto changeStatusDto)
    {
        var validationResult = await _validator.ValidateAsync(changeStatusDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var existingTicket = await _ticketRepository.GetByIdAsync(ticketId);
        if (existingTicket is null)
        {
            throw new KeyNotFoundException($"El ticket con ID {ticketId} no fue encontrado.");
        }
        
        if (!Enum.IsDefined(typeof(Status), changeStatusDto.NewStatusId))
        {
            throw new ArgumentException($"El ID de estado '{changeStatusDto.NewStatusId}' no es válido.");
        }
        var newStatus = (Status)changeStatusDto.NewStatusId;
        
        existingTicket.ChangeStatus(newStatus);
        
        var updatedTicket = await _ticketRepository.UpdateAsync(existingTicket);
        
        if (updatedTicket == null)
        {
            throw new Exception("Error al cambiar el estado del ticket.");
        }
        
        return updatedTicket;
    }
}