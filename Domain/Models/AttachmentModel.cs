namespace Domain.Models;

public class AttachmentModel
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string ContentType { get; set; }
    public DateTime UploadedAt { get; set; }
    public int UploaderId { get; set; }
}