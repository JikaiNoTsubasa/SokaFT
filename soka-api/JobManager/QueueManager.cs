using System;
using Microsoft.EntityFrameworkCore;
using SBIDotnetUtils.Extensions;
using soka_api.Database;
using soka_api.Database.Models;

namespace soka_api.JobManager;

public class QueueManager(SoContext context): SoManager(context)
{
    public List<IndexQueueItem> FetchAllQueuedItems()
    {
        return [.. _context.IndexQueueItems.Include(i => i.Document).ThenInclude(d => d.Application)];
    }
}
