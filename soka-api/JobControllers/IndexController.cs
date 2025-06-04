using System;
using Microsoft.AspNetCore.Mvc;
using soka_api.Global;
using soka_api.JobManager;
using soka_api.JobModels;
using soka_api.JobModels.Indexing;

namespace soka_api.JobControllers;

public class IndexController(IndexManager idxManager) : SoController
{

    protected IndexManager _indexManager = idxManager;

    [HttpPost]
    [Route("api/index/queue")]
    public IActionResult AddToIndexQueue([FromForm] RequestIndexing model)
    {
        var item = _indexManager.AddToIndexQueue(model.Name, model.Content, model.Identifier, model.Application);
        return StatusCode(200, item.Document.ToDTO());
    }

    [HttpGet]
    [Route("api/documents")]
    public IActionResult FetchAllDocuments()
    {
        var res = new ApiResult() { HttpCode = 200, Content = _indexManager.FetchDocuments().Select(d => d.ToDTO()).ToList() };
        return Return(res);
    }
}
