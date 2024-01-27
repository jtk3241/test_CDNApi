using CompleteDevNet.API;
using CompleteDevNet.API.Extensions;
using CompleteDevNet.API.Middleware;
using CompleteDevNet.Core.Interfaces;
using CompleteDevNet.Core.SystemRelated;
using CompleteDevNet.Infrastructure;
using CompleteDevNet.Infrastructure.DataOracle;
using CompleteDevNet.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);

//Add Logger
var loggerConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId();

var logger = loggerConfig.CreateLogger();
builder.Services.AddSingleton<Serilog.ILogger>(logger);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
var databaseSettings = new DatabaseSettings();
builder.Configuration.GetSection("DatabaseSettings").Bind(databaseSettings);

if (databaseSettings.DatabaseProvider.ToLower() == "oracle")
{
    builder.Services.AddTransient<IDatabaseService, OracleDatabaseService>();
    builder.Services.AddDbContext<CDNContext>(c =>
        c.UseOracle(builder.Configuration.GetConnectionString("OracleConnectionString"))
    );
    builder.Services.AddTransient<IDataAccess, CompleteDevNet.Infrastructure.DataOracle.DataAccess>();
}
else if (databaseSettings.DatabaseProvider.ToLower() == "sql")
{

}

builder.Services.AddControllers();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IDeveloperService, DeveloperService>();
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddJWTTokenServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocument(document =>
{

    document.Title = $"Complete Developer Network";

    document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });

    document.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
