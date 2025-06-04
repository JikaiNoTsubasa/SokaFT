using System;
using log4net;
using Microsoft.EntityFrameworkCore;
using soka_api.Database;

namespace soka_api.Global;

public static class EFMigration
{
    private static readonly ILog log = LogManager.GetLogger(typeof(EFMigration));

    public static void ApplyMigrations(WebApplication app)
    {
        log.Info("Applying database migrations");
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SoContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
            log.Info("Database migrations applied");
        }
        else
        {
            log.Info("Database migrations already applied");
        }
    }
}
