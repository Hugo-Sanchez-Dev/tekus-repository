#region Usings
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tekus.Providers.Application.Interfaces.Catalog;
using Tekus.Providers.Application.Interfaces.Country;
using Tekus.Providers.Application.Interfaces.Provider;
using Tekus.Providers.Application.Interfaces.ProviderCatalog;
using Tekus.Providers.Application.Services;
using Tekus.Providers.Domain.Repositories;
using Tekus.Providers.Infrastructure.Data;
using Tekus.Providers.Infrastructure.Repositories;
#endregion

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<TekusProvidersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TekusAPIConnection")));

// Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IProviderCatalogService, ProviderCatalogService>();
builder.Services.AddScoped<ICountryService, CountryService>();

// HttpClient for country API
builder.Services.AddHttpClient<ICountryService, CountryService>(client =>
{
    client.BaseAddress = new Uri("https://restcountries.com/v3.1/");
});

// JWT Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
