using System;

namespace soka_api.Global;

public class ApiResult
{
    public int HttpCode { get; set; }
    public object? Content { get; set; }
}
