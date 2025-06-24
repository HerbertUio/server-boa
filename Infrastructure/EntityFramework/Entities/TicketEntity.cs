namespace Infrastructure.EntityFramework.Entities;

public class TicketEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public int Status { get; set; }
    public string? Tags { get; set; }
    
    public DateTime CreatedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime? ResolutionDate { get; set; }
    
    public int? PrimaryTicketId { get; set; }
    public virtual TicketEntity? PrimaryTicket { get; set; }
    
    public int? ParentTicketId { get; set; }
    public virtual TicketEntity? ParentTicket { get; set; }
    
    public int? RequesterId { get; set; }
    public int? AssignedAgentId { get; set; }
    public int? AssignedGroupId { get; set; }
    public int? OfficeId { get; set; }
    public int? AreaId { get; set; }
    public int? SubjectId { get; set; }
    
    public virtual ICollection<CommentEntity> Comments { get; set; }
    public virtual ICollection<AttachmentEntity> Attachments { get; set; }
    
    public TicketEntity()
    {
        Comments = new HashSet<CommentEntity>();
        Attachments = new HashSet<AttachmentEntity>();
    }
    
}