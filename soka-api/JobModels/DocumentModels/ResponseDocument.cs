namespace soka_api.JobModels.DocumentModels;

public record class ResponseDocument : ResponseEntity
{
    public string? Identifier { get; set; }
    public string? Name { get; set; }
    public string? ApplicationName { get; set; }
    public DateTime? IndexedDate { get; set; }
    public bool IsIndexed { get; set; } = false;
}
