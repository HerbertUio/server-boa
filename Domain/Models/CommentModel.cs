namespace Domain.Models;

public class CommentModel
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int AuthorId { get; set; }
    public bool IsPrivate { get; set; }
}