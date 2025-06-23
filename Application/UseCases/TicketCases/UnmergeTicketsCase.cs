using Domain.Dtos.TicketDtos;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.UseCases.TicketCases;

public class UnmergeTicketsCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<UnmergeTicketsDto> _ticketValidator;
    public UnmergeTicketsCase(ITicketRepository ticketRepository, IValidator<UnmergeTicketsDto> ticketValidator)
    {
        _ticketRepository = ticketRepository;
        _ticketValidator = ticketValidator;
    }
    
    public async Task<TicketModel> ExecuteAsync(UnmergeTicketsDto dto)
    {
        var validationResult = await _ticketValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(
                $"Capa de application: Error de validación al desfusionar tickets: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}"
            );
        }

        var primaryTicket = await _ticketRepository.GetByIdAsync(dto.TicketId);
        if (primaryTicket is null)
        {
            throw new KeyNotFoundException($"Capa de application: Ticket con ID {dto.TicketId} no encontrado.");
        }
        
        var secondaryTicket = await _ticketRepository.GetByIdAsync(dto.TicketToUnmergeId);
        if (secondaryTicket is null)
        {
            throw new KeyNotFoundException($"Capa de application: Ticket a desfusionar con ID {dto.TicketToUnmergeId} no encontrado.");
        }
        
        var resultTicket = await _ticketRepository.UnmergeTicketsAsync(dto.TicketId, dto.TicketToUnmergeId);
        if (resultTicket is null)
        {
            throw new Exception("Capa de application: No se pudo completar la operación de desfusión.");
        }
        return resultTicket;
    }
}