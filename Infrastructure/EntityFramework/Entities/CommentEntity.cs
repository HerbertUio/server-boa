using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.EntityFramework.Entities;

[Table("TicketComments")]
public class CommentEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required (ErrorMessage = "El contenido del comentario es obligatorio.")]
    public string Content { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int AuthorId { get; set; }
    public bool IsPrivate { get; set; }
    
    public int TicketId { get; set; }
    public virtual TicketEntity Ticket { get; set; }
}