using AutoMapper;
using ControlGastos.Application.Services;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Interfaces.Services;
using ControlGastos.Infrastructure.Data;
using ControlGastos.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//repositories
// Capa de Repositorios (Infrastructure)
builder.Services.AddScoped<IGastoDetalleRepository, GastoDetalleRepository>();
builder.Services.AddScoped<IDepositoRepository, DepositoRepository>();
builder.Services.AddScoped<IGastoEncabezadoRepository, GastoEncabezadoRepository>();
builder.Services.AddScoped<IPresupuestoRepository, PresupuestoRepository>();

// pendiente implementar ITipoGastoRepository y IFondoMonetarioRepository 
builder.Services.AddScoped<ITipoGastoRepository, TipoGastoRepository>();
builder.Services.AddScoped<IFondoMonetarioRepository, FondoMonetarioRepository>();


// Capa de Servicios (Application)
builder.Services.AddScoped<IGastoDetalleService, GastoDetalleService>();
builder.Services.AddScoped<IDepositoService, DepositoService>();
builder.Services.AddScoped<IGastoEncabezadoService, GastoEncabezadoService>();
builder.Services.AddScoped<IPresupuestoService, PresupuestoService>();

builder.Services.AddScoped<ITipoGastoService, TipoGastoService>();
builder.Services.AddScoped<IFondoMonetarioService, FondoMonetarioService>();



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//swagger UI
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("politica", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(ControlGastos.API.MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseCors("politica");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DbInitializer.SeedAdminUser(app.Services);

app.Run();
