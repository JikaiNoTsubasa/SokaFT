using System;
using soka_api.Database.Models;
using soka_api.JobModels.DocumentModels;
using soka_api.JobModels.QueueModels;

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

    public static ResponseIndexQueueItem ToDTO(this IndexQueueItem item)
    {
        return new()
        {
            Id = item.Id,
            Document = item.Document.ToDTO(),
            Status = item.Status,
            IndexStartDate = item.IndexStartDate,
            IndexEndDate = item.IndexEndDate,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt
        };
    }
}
