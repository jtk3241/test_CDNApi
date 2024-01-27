using CompleteDevNet.API.Extensions;
using CompleteDevNet.Core.Interfaces;
using CompleteDevNet.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag;
using NSwag.Generation.Processors.Security;
using OpenApiSecurityScheme = NSwag.OpenApiSecurityScheme;


var builder = WebApplication.CreateBuilder(args);

//Add Logger
var loggerConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId();

var logger = loggerConfig.CreateLogger();
builder.Services.AddSingleton<Serilog.ILogger>(logger);

builder.Services.AddControllers();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IUserService, UserService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
