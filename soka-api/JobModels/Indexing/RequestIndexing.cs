using System.ComponentModel.DataAnnotations;

namespace soka_api.JobModels.Indexing;

public record class RequestIndexing
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    [Required]
    public string Identifier { get; set; } = null!;

    [Required]
    public string Application { get; set; } = null!;
}
