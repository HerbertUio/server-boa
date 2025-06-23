using Domain.Enums.TicketEnums;
namespace Domain.Models;

public class TicketModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public string? Tags { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime? ResolutionDate { get; set; }

    public int? RequesterId { get; set; }
    public int? AssignedAgentId { get; set; }
    public int? AssignedGroupId { get; set; }
    public int? OfficeId { get; set; }
    public int? AreaId { get; set; }
    public int? SubjectId { get; set; }

    public int? PrimaryTicketId { get; set; }
    public int? ParentTicketId { get; set; }
    public ICollection<TicketModel>? ChildTickets { get; set; } = new List<TicketModel>();

    public ICollection<CommentModel>? Comments { get; set; } = new List<CommentModel>();

    public ICollection<AttachmentModel>? Attachments { get; set; } = new List<AttachmentModel>();

    public static TicketModel Create(
        int? requesterId,
        string title,
        string description,
        int? subjectId,
        int? officeId,
        int? typeTicketId)
    {
        return new TicketModel()
        {
            Title = title,
            Description = description,
            Priority = Priority.Bajo,
            Status = Status.Abierto,
            CreatedDate = DateTime.Now,
            ClosedDate = null,
            ResolutionDate = null,
            RequesterId = requesterId,
            AssignedAgentId = null,
            AssignedGroupId = null,
            OfficeId = officeId,
            AreaId = null,
            SubjectId = subjectId,
            PrimaryTicketId = null,
            ParentTicketId = null
        };
    }
    
    public void Update(
        string title,
        string description,
        int? priorityId,
        int? statusId,
        int? primaryTicketId,
        int? parentTicketId,
        int? assignedAgentId,
        int? assignedGroupId,
        int? typeTicketId,
        int? officeId,
        int? areaId,
        int? subjectId)
    {
        Title = title;
        Description = description;
        
        if (priorityId.HasValue)
            Priority = (Priority)priorityId.Value;

        if (statusId.HasValue)
            Status = (Status)statusId.Value;

        PrimaryTicketId = primaryTicketId;
        ParentTicketId = parentTicketId;
        AssignedAgentId = assignedAgentId;
        AssignedGroupId = assignedGroupId;
        OfficeId = officeId;
        AreaId = areaId;
        SubjectId = subjectId;
    }
    
    public void ChangePriority(Priority newPriority)
    {
        if (Status == Status.Cerrado) return;
        if (Priority == newPriority) return;
        Priority = newPriority;
    }
    
    public void ChangeStatus(Status newStatus)
    {
        if (Status == Status.Cerrado && newStatus != Status.Reabierto) return;
        if (newStatus == Status) return;
        Status = newStatus;
    }
}