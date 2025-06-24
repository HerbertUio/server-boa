using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.EntityFramework.Entities;

[Table("TicketAttachments")]
public class AttachmentEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string FileName { get; set; }
    
    [Required]
    public string FilePath { get; set; }

    public string ContentType { get; set; }
    
    public DateTime UploadedAt { get; set; } = DateTime.Now;
    
    public int UploaderId { get; set; }
    public int TicketId { get; set; }
    public virtual TicketEntity Ticket { get; set; }
}