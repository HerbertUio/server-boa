using System.Reflection;
using Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public class ApplicationContext: DbContext
{
    public DbSet<TicketEntity> Tickets { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<AttachmentEntity> Attachments { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}