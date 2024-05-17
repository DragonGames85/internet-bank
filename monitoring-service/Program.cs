using monitoring_service;
using monitoring_service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var isProduction = Environment.GetEnvironmentVariable("IS_PRODUCTION");
var isValid = bool.TryParse(isProduction, out bool isProd);
var connection = isValid && isProd
    ? builder.Configuration.GetConnectionString("ProductionConnection")
    : builder.Configuration.GetConnectionString("LocalConnection");
builder.Services.AddSqlServer<ApplicationDbContext>(connection);


builder.Services.AddScoped<IMonitoringService, MonitoringService>();

var app = builder.Build();

// Auto migration
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext?.Database.Migrate();
}

app.UseCors();

// Swagger settings
var routeSwaggerJson = isValid && isProd
    ? "/monitoring/swagger/v1/swagger.json"
    : "/swagger/v1/swagger.json";
var routeSwaggerPrefix = isValid && isProd
    ? "monitoring/swagger"
    : "swagger";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
