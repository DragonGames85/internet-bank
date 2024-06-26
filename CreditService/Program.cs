using CreditService;
using CreditService.Logger;
using CreditService.Middlewares;
using CreditService.Repository;
using CreditService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connection));
builder.Services.AddHealthChecks();
builder.Services.AddScoped<IUserCreditService, UserCreditService>();
builder.Services.AddScoped<ICreditRepository, CreditRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICreditEmployeeRepository, CreditEmployeeRepository>();
builder.Services.AddHostedService<MyHostedService>();
builder.Services.AddScoped<IMonitoring, Monitoring>();
builder.Services.AddScoped<IExceptionService, ExceptionService>();
builder.Services.AddHttpClient();


var app = builder.Build();

// Auto migration
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext?.Database.Migrate();
}

app.UseCors();

// Swagger settings
var isProduction = Environment.GetEnvironmentVariable("IS_PRODUCTION");
var isValid = bool.TryParse(isProduction, out bool isProd);
var routeSwaggerJson = isValid && isProd
    ? "/credit/swagger/v1/swagger.json"
    : "/swagger/v1/swagger.json";
var routeSwaggerPrefix = isValid && isProd
    ? "credit/swagger"
    : "swagger";

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(routeSwaggerJson, "My API V1");
        c.RoutePrefix = routeSwaggerPrefix;
    });
}

app.UseAuthorization();
app.MapHealthChecks("/health");

app.UseMiddleware<CircuitBreakerMiddleware>();
app.UseMiddleware<HalfErrorMiddleware>();

app.MapControllers();


app.Run();