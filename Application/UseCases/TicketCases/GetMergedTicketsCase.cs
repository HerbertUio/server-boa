using Domain.IRepositories;
using Domain.Models;

namespace Application.UseCases.TicketCases;

public class GetMergedTicketsCase
{
    private readonly ITicketRepository _ticketRepository;

    public GetMergedTicketsCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<IEnumerable<TicketModel>> ExecuteAsync(int primaryTicketId)
    {
        var primaryTicket = await _ticketRepository.GetByIdAsync(primaryTicketId);
        if (primaryTicket == null)
        {
            throw new KeyNotFoundException($"El ticket principal con ID {primaryTicketId} no fue encontrado.");
        }

        return await _ticketRepository.GetMergedTicketsAsync(primaryTicketId);
    }
}