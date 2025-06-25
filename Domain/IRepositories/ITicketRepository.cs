using Domain.Models;

namespace Domain.IRepositories;

public interface ITicketRepository: IGenericRepository<TicketModel>
{
    Task<TicketModel> ChangeStatusAsync(int ticketId, int statusId);
    Task<TicketModel> ChangePriorityAsync(int ticketId, int priorityId);
    Task<TicketModel> MergeTicketsAsync(int ticketId, int ticketIdToMerge);
    Task<TicketModel> UnmergeTicketsAsync(int ticketId, int ticketIdToUnmerge);
    Task<bool> CloseTicketAsync(int ticketId);
    Task<bool> ReopenTicketAsync(int ticketId);
    Task AddCommentAsync(int ticketId, CommentModel comment);
    Task<TicketModel> AssignTicketAsync(int ticketId, int? agentId, int? groupId);
    Task<IEnumerable<CommentModel>> GetCommentsByTicketIdAsync(int ticketId);
    Task<IEnumerable<TicketModel>> GetChildTicketsAsync(int parentTicketId);
    Task<IEnumerable<TicketModel>> GetMergedTicketsAsync(int primaryTicketId);
}