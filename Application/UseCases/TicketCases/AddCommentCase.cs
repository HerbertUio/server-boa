using Domain.Dtos.TicketDtos;
using Domain.IRepositories;
using Domain.Models;

namespace Application.UseCases.TicketCases;

public class AddCommentCase
{
    private readonly ITicketRepository _ticketRepository;
    public AddCommentCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    public async Task<TicketModel> ExecuteAsync(int ticketId, AddCommentDto dto)
    {
        var ticket = await _ticketRepository.GetByIdAsync(ticketId);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"El ticket con ID {ticketId} no fue encontrado.");
        }

        var comment = new CommentModel
        {
            TicketId = ticketId,
            Content = dto.Content,
            AuthorId = dto.AuthorId,
            IsPrivate = dto.IsPrivate,
            CreatedAt = DateTime.UtcNow
        };
        
        await _ticketRepository.AddCommentAsync(ticketId, comment);
        
        return await _ticketRepository.GetByIdAsync(ticketId);
    }
    
}