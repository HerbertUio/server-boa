using Domain.IRepositories;
using Domain.Models;

namespace Application.UseCases.TicketCases;

public class GetChildTicketsCase
{
    private readonly ITicketRepository _ticketRepository;
    public GetChildTicketsCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    
    public async Task<IEnumerable<TicketModel>> ExecuteAsync(int parentTicketId)
    {
        var parentTicket = await _ticketRepository.GetByIdAsync(parentTicketId);
        if (parentTicket == null)
        {
            throw new KeyNotFoundException($"El ticket padre con ID {parentTicketId} no fue encontrado.");
        }

        return await _ticketRepository.GetChildTicketsAsync(parentTicketId);
    }
}