using System;
using log4net;
using Microsoft.EntityFrameworkCore;
using soka_api.Database;
using soka_api.Database.Models;
using soka_api.Indexer;

namespace soka_api.JobManager;

public class IndexManager(SoContext ctx) : SoManager(ctx)
{
    private static ILog log = LogManager.GetLogger(typeof(IndexManager));

    public IndexQueueItem AddToIndexQueue(string name, string content, string identifier, string applicationName)
    {
        log.Info($"Adding document {name} to index queue...");
        // Check if application exists
        var app = _context.Applications.FirstOrDefault(a => a.Name.Equals(applicationName));
        if (app == null)
        {
            // Create new application
            app = new Application()
            {
                Name = applicationName
            };
            _context.Applications.Add(app);
            _context.SaveChanges();
        }

        // Check if document already exists, if yes reindex it
        var doc = _context.Documents.FirstOrDefault(d => d.Identifier.Equals(identifier) && d.Application.Name.Equals(applicationName));
        if (doc == null)
        {
            // Create new document
            doc = new Document()
            {
                Name = name,
                Identifier = identifier,
                Application = app,
                Content = content
            };
            doc.MarkAsCreated();
            doc.MarkAsUpdated();
            _context.Documents.Add(doc);
            _context.SaveChanges();
        }
        else
        {
            // Update document
            doc.Name = name;
            doc.Content = content;
            doc.MarkAsUpdated();
            _context.SaveChanges();
        }

        // Add to index queue
        var item = new IndexQueueItem()
        {
            Status = IndexQueueStatus.PENDING,
            Document = doc,
            IndexStartDate = DateTime.UtcNow
        };
        _context.IndexQueueItems.Add(item);
        _context.SaveChanges();

        log.Info($"Added document {name} to index queue");

        return item;
    }

    public List<Document> FetchDocuments()
    {
        return [.. _context.Documents.Include(d => d.Application)];
    }

    public List<Document> SearchFullText(string queryText, string applicationName)
    {
        Application app = _context.Applications.FirstOrDefault(a => a.Name.Equals(applicationName)) ?? throw new Exception("Application not found");
        var identifiers = LuceneEngine.SearchDocumentsIdentifiers(queryText, app);
        return [.. _context.Documents.Include(d => d.Application).Where(d => d.Application.Name.Equals(app.Name) && identifiers.Contains(d.Identifier))];
    }
}
