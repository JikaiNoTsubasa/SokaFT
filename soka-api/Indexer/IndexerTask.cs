using System;
using log4net;
using Microsoft.EntityFrameworkCore;
using soka_api.Database;
using soka_api.Database.Models;

namespace soka_api.Indexer;

public class IndexerTask(IServiceProvider provider) : BackgroundService
{
    protected IServiceProvider _provider = provider;
    private static ILog log = LogManager.GetLogger(typeof(IndexerTask));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        log.Info("Indexer task started");

        // Create DB connection
        using var scope = _provider.CreateScope();
        var _context = scope.ServiceProvider.GetRequiredService<SoContext>();

        // Parameters
        int indexingCount = 5;
        int sleepTime = 30;

        // Get queued index requests not yet indexed
        List<IndexQueueItem> items = [.. _context.IndexQueueItems.Include(i => i.Document).ThenInclude(d => d.Application).Where(i => i.Status.Equals(IndexQueueStatus.PENDING)).Take(indexingCount)];

        if (items.Count == 0)
        {
            log.Debug("No items to index");
        }
        else
        {
            foreach (var item in items)
            {
                log.Debug($"Indexing {item.Document.Name} ({item.Document.Identifier})...");
                // Update status to processing
                item.Status = IndexQueueStatus.PROCESSING;
                item.IndexStartDate = DateTime.UtcNow;
                _context.SaveChanges();

                // Index document
                LuceneEngine.IndexDocument(item.Document.Identifier, item.Document.Name, item.Document.Content ?? "", item.Document.Application);
                item.Document.Content = "indexed";
                item.Document.MarkAsUpdated();

                // Update status to processed
                item.Status = IndexQueueStatus.PROCESSED;
                item.IndexEndDate = DateTime.UtcNow;
                item.MarkAsUpdated();
                _context.SaveChanges();
                log.Debug($"Indexed {item.Document.Name} ({item.Document.Identifier})");
            }
            
        }


        log.Info("Indexer task finished");
        await Task.Delay(TimeSpan.FromSeconds(sleepTime), stoppingToken);
    }
}
