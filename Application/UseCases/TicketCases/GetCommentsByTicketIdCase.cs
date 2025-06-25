using Domain.IRepositories;
using Domain.Models;

namespace Application.UseCases.TicketCases;

public class GetCommentsByTicketIdCase
{
    private readonly ITicketRepository _ticketRepository;
    public GetCommentsByTicketIdCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    
    public async Task<IEnumerable<CommentModel>> ExecuteAsync(int ticketId)
    {
        // Verificamos que el ticket principal exista para no devolver comentarios de un ticket borrado.
        var ticketExists = await _ticketRepository.GetByIdAsync(ticketId);
        if (ticketExists == null)
        {
            throw new KeyNotFoundException($"El ticket con ID {ticketId} no fue encontrado.");
        }
        return await _ticketRepository.GetCommentsByTicketIdAsync(ticketId);
    }
}