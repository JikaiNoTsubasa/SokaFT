using System;
using soka_api.Database;

namespace soka_api.JobManager;

public class SoManager(SoContext context)
{
    protected SoContext _context = context;
}
