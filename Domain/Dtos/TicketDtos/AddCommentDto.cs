using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TicketDtos;

public class AddCommentDto
{
    [Required]
    public string Content { get; set; }
    
    [Required]
    public int AuthorId { get; set; }
    
    public bool IsPrivate { get; set; } = false;
}