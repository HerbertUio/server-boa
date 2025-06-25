using Domain.Enums.TicketEnums;
using Domain.IRepositories;
using Domain.Models;
using Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class TicketRepository: ITicketRepository
{
    private readonly ApplicationContext _context;
    public TicketRepository(ApplicationContext context)
    {
        _context = context;
    }

    private static TicketEntity ToEntity(TicketModel model)
    {
        return new TicketEntity
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Priority = (int)model.Priority,
            Status = (int)model.Status,
            Tags = model.Tags,
            CreatedDate = model.CreatedDate,
            ClosedDate = model.ClosedDate,
            ResolutionDate = model.ResolutionDate,
            RequesterId = model.RequesterId,
            AssignedAgentId = model.AssignedAgentId,
            AssignedGroupId = model.AssignedGroupId,
            OfficeId = model.OfficeId,
            AreaId = model.AreaId,
            SubjectId = model.SubjectId,
            PrimaryTicketId = model.PrimaryTicketId,
            ParentTicketId = model.ParentTicketId
        };
    }
    private static TicketModel ToModel(TicketEntity entity)
    {
        return new TicketModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Priority = (Priority)entity.Priority,
            Status = (Status)entity.Status,
            Tags = entity.Tags,
            CreatedDate = entity.CreatedDate,
            ClosedDate = entity.ClosedDate,
            ResolutionDate = entity.ResolutionDate,
            RequesterId = entity.RequesterId,
            AssignedAgentId = entity.AssignedAgentId,
            AssignedGroupId = entity.AssignedGroupId,
            OfficeId = entity.OfficeId,
            AreaId = entity.AreaId,
            SubjectId = entity.SubjectId,
            PrimaryTicketId = entity.PrimaryTicketId,
            ParentTicketId = entity.ParentTicketId,
            Comments = new List<CommentModel>(),
            Attachments = new List<AttachmentModel>(),
            ChildTickets = new List<TicketModel>()
            // Las colecciones se pueden cargar aquí si es necesario (ej. entity.Comments.Select(ToModel)...)
        };
    }
    private static CommentEntity ToEntity(CommentModel model)
    {
        return new CommentEntity
        {
            Id = model.Id,
            TicketId = model.TicketId,
            Content = model.Content,
            AuthorId = model.AuthorId,
            IsPrivate = model.IsPrivate,
            CreatedAt = model.CreatedAt
        };
    }

    public async Task<TicketModel> CreateAsync(TicketModel model)
    {
        var entity = ToEntity(model);
        await _context.Tickets.AddAsync(entity);
        await _context.SaveChangesAsync();
        return ToModel(entity);
    }

    public async Task<List<TicketModel>> GetAllAsync()
    {
        var entities = await _context.Tickets.AsNoTracking().ToListAsync();
        return entities.Select(ToModel).ToList();
    }

    public async Task<TicketModel> GetByIdAsync(int id)
    {
        var entity = await _context.Tickets
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        return entity != null ? ToModel(entity) : null;
    }

    public async Task<TicketModel> UpdateAsync(TicketModel model)
    {
        var entity = ToEntity(model);
        _context.Tickets.Update(entity);
        await _context.SaveChangesAsync();
        return ToModel(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Tickets.FindAsync(id);
        if (entity is null)
        {
            return false;
        }
        _context.Tickets.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TicketModel> ChangeStatusAsync(int ticketId, int statusId)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);
        if (ticket is null) return null;

        ticket.Status = statusId;
        await _context.SaveChangesAsync();
        return ToModel(ticket);
    }

    public async Task<TicketModel> ChangePriorityAsync(int ticketId, int priorityId)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);
        if (ticket is null) return null;

        ticket.Priority = priorityId;
        await _context.SaveChangesAsync();
        return ToModel(ticket);
    }

    public async Task<TicketModel> MergeTicketsAsync(int ticketId, int ticketIdToMerge)
    {
        var primaryTicket = await _context.Tickets.FindAsync(ticketId);
        var ticketToMerge = await _context.Tickets.FindAsync(ticketIdToMerge);
        

        ticketToMerge.PrimaryTicketId = primaryTicket.Id;
        ticketToMerge.Status = (int)Status.Cerrado; 
        await _context.SaveChangesAsync();

        return ToModel(primaryTicket);
    }

    public async Task<TicketModel> UnmergeTicketsAsync(int ticketId, int ticketIdToUnmerge)
    {
        var primaryTicket = await _context.Tickets.FindAsync(ticketId);
        var ticketToUnmerge = await _context.Tickets.FindAsync(ticketIdToUnmerge);
        
        ticketToUnmerge.PrimaryTicketId = null;
        ticketToUnmerge.Status = (int)Status.Reabierto; 
        await _context.SaveChangesAsync();

        return ToModel(primaryTicket);
    }

    public async Task<bool> CloseTicketAsync(int ticketId)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);
        if (ticket is null) return false;

        ticket.Status = (int)Status.Cerrado;
        ticket.ClosedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReopenTicketAsync(int ticketId)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);
        if (ticket is null) return false;

        ticket.Status = (int)Status.Reabierto;
        ticket.ClosedDate = null;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task AddCommentAsync(int ticketId, CommentModel comment)
    {
        var commentEntity = ToEntity(comment);
        await _context.Comments.AddAsync(commentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<TicketModel> AssignTicketAsync(int ticketId, int? agentId, int? groupId)
    {
        var ticketEntity = await _context.Tickets.FindAsync(ticketId);
        if (ticketEntity == null)
        {
            throw new KeyNotFoundException($"El ticket con ID {ticketId} no fue encontrado.");
        }

        
        ticketEntity.AssignedAgentId = agentId;
        ticketEntity.AssignedGroupId = groupId;

        await _context.SaveChangesAsync();

        return ToModel(ticketEntity);
    }

    public async Task<IEnumerable<CommentModel>> GetCommentsByTicketIdAsync(int ticketId)
    {
        var commentEntities = await _context.Comments
            .Where(c => c.TicketId == ticketId)
            .AsNoTracking()
            .ToListAsync();
        return commentEntities.Select(c => new CommentModel
        {
            Id = c.Id,
            TicketId = c.TicketId,
            Content = c.Content,
            AuthorId = c.AuthorId,
            IsPrivate = c.IsPrivate,
            CreatedAt = c.CreatedAt
        });
    }
    public async Task<IEnumerable<TicketModel>> GetChildTicketsAsync(int parentTicketId)
    {
        var childEntities = await _context.Tickets
            .Where(t => t.ParentTicketId == parentTicketId)
            .AsNoTracking()
            .ToListAsync();
        return childEntities.Select(ToModel);
    }

    public async Task<IEnumerable<TicketModel>> GetMergedTicketsAsync(int primaryTicketId)
    {
        var mergedEntities = await _context.Tickets
            .Where(t => t.PrimaryTicketId == primaryTicketId)
            .AsNoTracking()
            .ToListAsync();
    
        return mergedEntities.Select(ToModel);
    }
}