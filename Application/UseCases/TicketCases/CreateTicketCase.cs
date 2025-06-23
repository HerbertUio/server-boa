using Domain.Dtos.TicketDtos;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.UseCases.TicketCases;

public class CreateTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<CreateTicketDto> _validator;

    public CreateTicketCase(ITicketRepository ticketRepository, IValidator<CreateTicketDto> validator)
    {
        _validator = validator;
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketModel> ExecuteAsync(CreateTicketDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        
        if (!validationResult.IsValid)
        {
            throw new ArgumentException(
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                nameof(dto)
            );
        }

        var ticket = TicketModel.Create(
            dto.RequesterId,
            dto.Title,
            dto.Description,
            dto.SubjectId,
            dto.OfficeId,
            dto.TypeTicketId
        );
        
        var ticketCreated = await _ticketRepository.CreateAsync(ticket);
        
        if (ticketCreated is null)
        {
            throw new Exception("Capa de application: Error al crear ticket.");
        }
        
        return ticketCreated;
    }
} 