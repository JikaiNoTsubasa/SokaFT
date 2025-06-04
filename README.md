# SokaFT

A dotnet fulltext indexing server

# Usage

## Add to index queue

```
POST /api/index/queue
{
  "name": "document2.docx",
  "application": "soka",
  "identifier": "00-03",
  "content": "The content BS2026 gors here"
}
```

## Search

```
POST /api/index/search
{
  "search":"BS2026",
  "application": "soka"
}
```

