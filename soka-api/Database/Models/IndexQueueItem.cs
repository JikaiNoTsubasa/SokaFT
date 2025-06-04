using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace soka_api.Database.Models;

public class IndexQueueItem : Entity
{
    public IndexQueueStatus Status { get; set; } = IndexQueueStatus.PENDING;

    [ForeignKey(nameof(Document))]
    public long DocumentId { get; set; }
    public Document Document { get; set; } = null!;

    public DateTime? IndexStartDate { get; set; }
    public DateTime? IndexEndDate { get; set; }
}
