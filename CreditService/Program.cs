using CreditService;
using CreditService.Repository;
using CreditService.Services;
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

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connection));

builder.Services.AddScoped<IUserCreditService, UserCreditService>();
builder.Services.AddScoped<ICreditRepository, CreditRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICreditEmployeeRepository, CreditEmployeeRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddHostedService<MyHostedService>();
//var logger = new LoggerConfiguration()
//  .ReadFrom.Configuration(builder.Configuration)
//  .Enrich.FromLogContext()
//  .CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);

var app = builder.Build();

//using var serviceScope = app.Services.CreateScope();
//var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//dbContext?.Database.Migrate();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
