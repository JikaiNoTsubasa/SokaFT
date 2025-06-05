using soka_api.Database.Models;
using soka_api.JobModels.DocumentModels;

namespace soka_api.JobModels.QueueModels;

public record class ResponseIndexQueueItem : ResponseEntity
{
    public IndexQueueStatus Status { get; set; }

    public ResponseDocument Document { get; set; } = null!;

    public DateTime? IndexStartDate { get; set; }
    public DateTime? IndexEndDate { get; set; }
}
