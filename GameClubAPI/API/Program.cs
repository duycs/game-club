using Application.Services;
using CrossCutting;
using Infrastructure.Persistences;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration;

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory());

// system env or run on local has default is development
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS");

builder.Environment.EnvironmentName = env;

configurationBuilder.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

configuration = configurationBuilder.Build();

// use connection string in appsettings
connectionString = configuration.GetConnectionString("GameClub");

// use serilog to console
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

logger.Information("ENVIRONMENT");
logger.Information(env);
logger.Information(connectionString);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

// add services to the container
builder.Services.AddLayersInjector(configuration);

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// author if need
/*
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.Authority = configuration.GetValue<string>("AppSettings:ssoUrl");
    o.Audience = "api";
    o.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("apiPolicy", policy => policy.RequireClaim("scope", "api.read"));
});
*/

// Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

// migrate database
using (var serviceScope = app.Services.CreateScope())
{
    var gameClubContext = serviceScope.ServiceProvider.GetRequiredService<GameClubContext>();
    gameClubContext.Database.Migrate();
}

// swagger in development
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
