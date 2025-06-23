using Application.Validators.TicketValidators;
using Domain.Dtos.TicketDtos;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.UseCases.TicketCases;

public class MergeTicketsCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<MergeTicketsDto> _ticketValidator;
    
    public MergeTicketsCase(ITicketRepository ticketRepository, IValidator<MergeTicketsDto> ticketValidator)
    {
        _ticketRepository = ticketRepository;
        _ticketValidator = ticketValidator;
    }
    
    public async Task<TicketModel> ExecuteAsync (MergeTicketsDto dto)
    {
        var validationResult = await _ticketValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(
                $"Capa de application: Error de validación al fusionar tickets: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}"
            );
        }
        var primaryTicket = await _ticketRepository.GetByIdAsync(dto.PrimaryTicketId);
        var ticketToMerge = await _ticketRepository.GetByIdAsync(dto.TicketToMergeId);
        if (primaryTicket is null)
        {
            throw new KeyNotFoundException($"Capa de application: Ticket principal con ID {dto.PrimaryTicketId} no encontrado.");
        }
        if (ticketToMerge is null)
        {
            throw new KeyNotFoundException($"Capa de application: Ticket a fusionar con ID {dto.TicketToMergeId} no encontrado.");
        }
        
        var resultTicket = await _ticketRepository.MergeTicketsAsync(dto.PrimaryTicketId, dto.TicketToMergeId);
        if (resultTicket is null)
        {
            throw new Exception("Capa de application: No se pudo completar la operación de fusión.");
        }
        return resultTicket;
    }
}