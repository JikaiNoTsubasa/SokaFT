using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using soka_api.Database;
using soka_api.Global;

namespace soka_api.JobControllers;

[ApiController]
public class SoController : Controller
{
    [NonAction]
    public virtual ObjectResult Return(ApiResult result){
        return StatusCode(result.HttpCode, result.Content);
    }
}
