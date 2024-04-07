using InternetBank.Core.Application.Extensions;
using InternetBank.Core.Infrastructure.Extensions;
using InternetBank.Core.Infrastructure.Hubs.OperationHubs;
using InternetBank.Core.Persistence.Contexts.EfCore;
using InternetBank.Core.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

builder.Services.AddEndpointsApiExplorer();


// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core API", Version = "v1" });

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
}

app.UseCors();

// Swagger settings
var isProduction = Environment.GetEnvironmentVariable("IS_PRODUCTION");
var isValid = bool.TryParse(isProduction, out bool isProd);
var routeSwaggerJson = isValid && isProd
    ? "/core/swagger/v1/swagger.json"
    : "/swagger/v1/swagger.json";
var routeSwaggerPrefix = isValid && isProd
    ? "core/swagger"
    : "swagger";

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(routeSwaggerJson, "Core API V1");
        c.RoutePrefix = routeSwaggerPrefix;
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// WebSocket
app.MapHub<OperationHub>("/core/operationHub");

app.MapControllers();

app.Run();
