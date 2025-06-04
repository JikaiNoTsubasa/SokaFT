using Microsoft.EntityFrameworkCore;
using soka_api.Database.Models;

namespace soka_api.Database;


public class SoContext(DbContextOptions<SoContext> options) : DbContext(options)
{
    public DbSet<Document> Documents { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<IndexQueueItem> IndexQueueItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Application>().HasIndex(a => a.Name).IsUnique();
        builder.Entity<Application>().HasMany(a => a.Documents).WithOne(d => d.Application);

        builder.Entity<Document>().HasIndex(d => d.Identifier).IsUnique();
    }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=soka.sqlite");
    }
    
}