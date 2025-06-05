using System;
using Microsoft.AspNetCore.Mvc;
using soka_api.Global;
using soka_api.JobManager;
using soka_api.JobModels;

namespace soka_api.JobControllers;

public class QueueController(QueueManager queueManager) : SoController
{

    protected QueueManager _queueManager = queueManager;

    [HttpGet]
    [Route("api/index/queues")]
    public IActionResult FetchQueueForApplication([FromQuery] string applicationName)
    {
        var res = new ApiResult() { HttpCode = 200, Content = _queueManager.FetchAllQueuedItems().Select(d => d.ToDTO()).ToList() };
        return Return(res);
    }
}
