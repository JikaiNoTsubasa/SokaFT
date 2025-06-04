using System;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using soka_api.Database.Models;

namespace soka_api.Indexer;

public class LuceneEngine
{

    private const LuceneVersion VERSION = LuceneVersion.LUCENE_48;

    public static void IndexDocument(string identifier, string name, string content, Application app)
    {
        string folder = "index";
        System.IO.Directory.CreateDirectory(folder);
        string lucene_index_path = Path.Combine(folder, app.Name);
        var dir = FSDirectory.Open(lucene_index_path);
        var analyzer = new StandardAnalyzer(VERSION);
        var config = new IndexWriterConfig(VERSION, analyzer);
        using var writer = new IndexWriter(dir, config);
        var doc = new Lucene.Net.Documents.Document
        {
            new TextField("name", name, Field.Store.YES),
            new TextField("identifier", identifier, Field.Store.YES),
            new TextField("content", content, Field.Store.YES)
        };
        writer.AddDocument(doc);
        writer.Flush(triggerMerge: false, applyAllDeletes: false);
    }
    
    public static List<string> SearchDocumentsIdentifiers(string queryText, Application app, int maxResults = 10)
    {
        string folder = "index";
        string lucene_index_path = Path.Combine(folder, app.Name);
        var dir = FSDirectory.Open(lucene_index_path);
        var analyzer = new StandardAnalyzer(VERSION);
        using var reader = DirectoryReader.Open(dir);
        var searcher = new IndexSearcher(reader);
        var parser = new MultiFieldQueryParser(VERSION, ["name", "identifier", "content"], analyzer);
        var query = parser.Parse(queryText);

        var hits = searcher.Search(query, maxResults);

        return [.. hits.ScoreDocs.Select(h => searcher.Doc(h.Doc).Get("identifier"))];
    }
}
