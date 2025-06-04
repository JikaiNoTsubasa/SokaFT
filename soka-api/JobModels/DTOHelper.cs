using System;
using soka_api.Database.Models;
using soka_api.JobModels.DocumentModels;

namespace soka_api.JobModels;

public static class DTOHelper
{
    public static ResponseDocument ToDTO(this Document document)
    {
        return new()
        {
            Id = document.Id,
            Name = document.Name,
            Identifier = document.Identifier,
            ApplicationName = document.Application.Name,
            IndexedDate = document.IndexedDate,
            IsIndexed = document.IsIndexed,
            CreatedAt = document.CreatedAt,
            UpdatedAt = document.UpdatedAt
        };
    }
}
