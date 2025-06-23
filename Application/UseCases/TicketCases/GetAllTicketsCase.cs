using Domain.IRepositories;
using Domain.Models;

namespace Application.UseCases.TicketCases;

public class GetAllTicketsCase
{
    private readonly ITicketRepository _ticketRepository;
    
    public GetAllTicketsCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    
    public async Task<List<TicketModel>> ExecuteAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();
        if (tickets is null || !tickets.Any())
        {
            throw new Exception("No se encontraron tickets.");
        }
        return tickets;
    }
    
}