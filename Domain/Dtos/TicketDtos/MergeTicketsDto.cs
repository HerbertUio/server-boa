namespace Domain.Dtos.TicketDtos;

public class MergeTicketsDto
{
    public int PrimaryTicketId { get; set; }
    public int TicketToMergeId { get; set; }
}