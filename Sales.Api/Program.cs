using Core.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sales.Api.Model.Models;
using Sales.Api.Services;
using Sales.Api.Services.Interfaces;
using Sales.Domain.Services;
using Sales.Domain.Services.Interfaces;
using Sales.Infraestructure;
using Sales.Infraestructure.Management;
using Sales.Repositories;
using Sales.Repositories.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Sales API",
        Version = "v1"
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssemblyContaining<SalesCreatableDto>(ServiceLifetime.Singleton);
builder.Services.AddValidatorsFromAssemblyContaining<SalesDetailsCreatableDto>(ServiceLifetime.Singleton);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddSingleton<IModelValidatorResolver, ModelValidatorResolver>();

builder.Services.AddScoped<ISalesServices, SalesServices>();
builder.Services.AddScoped<ISalesDomainServices, SalesDomainServices>();
builder.Services.AddScoped<ISalesRepositories, SalesRepositories>();
builder.Services.AddScoped<IDomainApplicationUnitOfWork, DomainApplicationUnitOfWork>();

var jwtConfig = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]))
        };
    });

builder.Services.AddHttpClient<IMovementHttpService, MovementHttpService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:47688/"); // URL de Movement.Api
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
