using Core.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Purchase.Api.Model.Models;
using Purchase.Api.Services;
using Purchase.Api.Services.Interfaces;
using Purchase.Domain.Services;
using Purchase.Domain.Services.Interfaces;
using Purchase.Infraestructure;
using Purchase.Infraestructure.Management;
using Purchase.Repositories;
using Purchase.Repositories.Interfaces;
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
        Title = "Purchase API",
        Version = "v1"
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddValidatorsFromAssemblyContaining<PurchaseCreatableDto>(ServiceLifetime.Singleton);
builder.Services.AddValidatorsFromAssemblyContaining<PurchaseDetailCreatableDto>(ServiceLifetime.Singleton);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddSingleton<IModelValidatorResolver, ModelValidatorResolver>();

builder.Services.AddScoped<IPurchaseServices, PurchaseServices>();
builder.Services.AddScoped<IPurchaseDomainServices, PurchaseDomainServices>();
builder.Services.AddScoped<IPurchaseRepositories, PurchaseRepositories>();
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

builder.Services.AddHttpClient<IProductHttpService, ProductHttpService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:42001/"); // URL de Product.Api
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
