using Domain.IRepositories;

namespace Application.UseCases.TicketCases;

public class DeleteTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    
    public DeleteTicketCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    
    public async Task<bool> ExecuteAsync(int ticketId)
    {
        var ticketToDelete = await _ticketRepository.GetByIdAsync(ticketId);
        if (ticketToDelete is null)
        {
            throw new KeyNotFoundException($"El ticket con ID {ticketId} no fue encontrado.");
        }
        
        var result = await _ticketRepository.DeleteAsync(ticketId);
        if (!result)
        {
            throw new Exception("Error al eliminar el ticket.");
        }
        return true;
    }
}