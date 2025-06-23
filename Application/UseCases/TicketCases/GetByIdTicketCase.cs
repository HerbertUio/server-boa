using Domain.IRepositories;
using Domain.Models;

namespace Application.UseCases.TicketCases;

public class GetByIdTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    public GetByIdTicketCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketModel> ExecuteAsync (int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        if (ticket is null)
        {
            throw new KeyNotFoundException($"El ticket con ID {id} no fue encontrado.");
        }
        return ticket;
    }
}