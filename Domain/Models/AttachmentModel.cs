namespace Domain.Models;

public class AttachmentModel
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; } // Ruta donde se guarda el archivo en el servidor
    public string ContentType { get; set; } // ej. "image/png" o "application/pdf"
}