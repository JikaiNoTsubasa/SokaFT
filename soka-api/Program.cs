using log4net;
using Microsoft.EntityFrameworkCore;
using soka_api.Database;
using soka_api.Global;
using soka_api.Indexer;
using soka_api.JobManager;

var builder = WebApplication.CreateBuilder(args);

// Add log4net
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net("log4net.config");

ILog log = LogManager.GetLogger(typeof(Program));

builder.Services.AddDbContext<SoContext>();

builder.Services.AddDbContext<SoContext>(options => options.UseSqlite("Data Source=soka.db"));
builder.Services.AddScoped<IndexManager>();
builder.Services.AddScoped<QueueManager>();

builder.Services.AddHostedService<IndexerTask>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations
// EFMigration.ApplyMigrations(app);

// Disable CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

log.Info("Application started");
app.Run();