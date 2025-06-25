using Domain.Dtos.TicketDtos;
using Domain.IRepositories;
using Domain.Models;

namespace Application.UseCases.TicketCases;

public class AssignTicketCase
{
    private readonly ITicketRepository _ticketRepository;

    public AssignTicketCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    
    public async Task<TicketModel> ExecuteAsync(int ticketId, AssignTicketDto dto)
    {
        // 1. Validar que el ticket existe
        var ticket = await _ticketRepository.GetByIdAsync(ticketId);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"El ticket con ID {ticketId} no fue encontrado.");
        }

        // 2. Asignar el ticket a través del repositorio
        // La lógica de la asignación real se delega al repositorio
        return await _ticketRepository.AssignTicketAsync(ticketId, dto.AgentId, dto.GroupId);
    }

}