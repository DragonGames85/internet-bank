using CreditService.Logger;
using InternetBank.Auth.Api;
using InternetBank.Auth.Application.DTOs.RoleDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Extensions;
using InternetBank.Auth.Application.Interfaces.Services.RoleServices;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using InternetBank.Auth.Infrastructure.Extensions;
using InternetBank.Auth.Infrastructure.Services.RoleServices;
using InternetBank.Auth.Infrastructure.Services.UserServices;
using InternetBank.Auth.Persistence.Contexts.EfCore;
using InternetBank.Auth.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

// Layers
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);

// Configure JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddControllers();
builder.Services.AddScoped<IMonitoring, Monitoring>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IExceptionService, ExceptionService>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });
});


var app = builder.Build();

// Auto migration
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext?.Database?.Migrate();

    if (dbContext != null)
    {
        var roleGetService = scope.ServiceProvider.GetRequiredService<IRoleGetService>();
        if (!roleGetService.GetAllRoles().Result.Any())
        {
            var roleHandleService = scope.ServiceProvider.GetRequiredService<IRoleHandleService>();

            var roleEmployee = "Employee";
            var roleCustomer = "Customer";
            await roleHandleService.CreateRole(roleEmployee);
            await roleHandleService.CreateRole(roleCustomer);

            dbContext.SaveChanges();
        }

        var userGetService = scope.ServiceProvider.GetRequiredService<IUserGetService>();
        if (!userGetService.GetAllUsers().Result.Any())
        {
            var userHandleService = scope.ServiceProvider.GetRequiredService<IUserHandleService>();
            
            var userEmployee = new CreateUserDto("coop", "coop", "coop", "Employee");
            var userCustomer = new CreateUserDto("add123", "add123", "add123", "Customer");
            await userHandleService.CreateUser(userEmployee);
            await userHandleService.CreateUser(userCustomer);

            dbContext.SaveChanges();
        }
    }
}

app.UseCors();

// Swagger settings
var isProduction = Environment.GetEnvironmentVariable("IS_PRODUCTION");
var isValid = bool.TryParse(isProduction, out bool isProd);
var routeSwaggerJson = isValid && isProd
    ? "/auth/swagger/v1/swagger.json" 
    : "/swagger/v1/swagger.json";
var routeSwaggerPrefix = isValid && isProd
    ? "auth/swagger"
    : "swagger";

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(routeSwaggerJson, "Auth API V1");
        c.RoutePrefix = routeSwaggerPrefix;
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
