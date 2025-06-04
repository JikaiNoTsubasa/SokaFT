using System;

namespace soka_api.Database.Models;

public class Application : Entity
{
    public string Name { get; set; } = null!;
    public List<Document>? Documents { get; set; }
}
