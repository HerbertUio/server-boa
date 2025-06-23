namespace Domain.Dtos.TicketDtos;

public class UpdateTicketDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int? PriorityId { get; set; }
    public int? StatusId { get; set; }
    public int? PrimaryTicketId { get; set; }
    public int? ParentTicketId { get; set; }
    public int? AssignedAgentId { get; set; }
    public int? AssignedGroupId { get; set; }
    public int? TypeTicketId { get; set; }
    public int? OfficeId { get; set; }
    public int? AreaId { get; set; }
    public int? SubjectId { get; set; }
}