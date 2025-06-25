using Domain.Dtos.TicketDtos;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.UseCases.TicketCases;

public class UpdateTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    private IValidator<UpdateTicketDto> _validator;
    
    public UpdateTicketCase(ITicketRepository ticketRepository, IValidator<UpdateTicketDto> validator)
    {
        _ticketRepository = ticketRepository;
        _validator = validator;
    }
    
    public async Task<TicketModel> ExecuteAsync(int id, UpdateTicketDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(
                $"Capa de application: Error de validación al actualizar el ticket: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}"
            );
        }
        var existingTicket = await _ticketRepository.GetByIdAsync(id);
        if (existingTicket is null)
        {
            throw new KeyNotFoundException($"Capa de application: Ticket con ID {id} no encontrado.");
        }
        
        existingTicket.Update(
            dto.Title,
            dto.Description,
            dto.PriorityId,
            dto.StatusId,
            dto.PrimaryTicketId,
            dto.ParentTicketId,
            dto.AssignedAgentId,
            dto.AssignedGroupId,
            dto.TypeTicketId,
            dto.OfficeId,
            dto.AreaId,
            dto.SubjectId
        );
        
        await _ticketRepository.UpdateAsync(existingTicket);
        if (existingTicket is null) 
        {
            throw new Exception("Capa de application: Error al actualizar el ticket.");
        }
        return existingTicket;
    }
}