using System.ComponentModel.DataAnnotations.Schema;

namespace soka_api.Database.Models;

public class Document : Entity
{
    public string Identifier { get; set; } = null!;
    public string Name { get; set; } = null!;

    [ForeignKey(nameof(Application))]
    public long ApplicationId { get; set; }
    public Application Application { get; set; } = null!;
    public string? Content { get; set; }
    public DateTime? IndexedDate { get; set; }
    public bool IsIndexed { get; set; } = false;

    public void MarkAsIndexed()
    {
        Content = "indexed";
        IsIndexed = true;
        IndexedDate = DateTime.UtcNow;
        MarkAsUpdated();
    }
}