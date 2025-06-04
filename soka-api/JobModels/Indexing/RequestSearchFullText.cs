using System.ComponentModel.DataAnnotations;

namespace soka_api.JobModels.Indexing;

public record class RequestSearchFullText
{
    [Required]
    public string Search { get; set; } = null!;
    [Required]
    public string Application { get; set; } = null!;
}
