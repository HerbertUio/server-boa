namespace Domain.Dtos.TicketDtos;

public class CreateTicketDto
{
    public int  RequesterId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int? SubjectId { get; set; } 
    public int? OfficeId { get; set; }
    public int? AreaId { get; set; }
    public int? TypeTicketId { get; set; }
}