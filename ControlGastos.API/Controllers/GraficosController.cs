using ControlGastos.Core.DTOs;
using ControlGastos.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraficosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GraficosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GraficoDto>>> Get([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            var tiposGasto = await _context.TiposGasto.ToListAsync();
            var presupuestos = await _context.Presupuestos.ToListAsync();
            var detalles = await _context.GastoDetalles
                .Include(d => d.TipoGasto)
                .Include(d => d.GastoEncabezado)
                .Where(d => d.GastoEncabezado.Fecha >= fechaInicio && d.GastoEncabezado.Fecha <= fechaFin)
                .ToListAsync();

            var result = tiposGasto.Select(tipo => new GraficoDto
            {
                TipoGastoNombre = tipo.Nombre,
                Presupuestado = presupuestos.Where(p => p.TipoGastoId == tipo.Id &&
                    new DateTime(p.Anio, p.Mes, 1) >= new DateTime(fechaInicio.Year, fechaInicio.Month, 1) &&
                    new DateTime(p.Anio, p.Mes, 1) <= new DateTime(fechaFin.Year, fechaFin.Month, 1))
                    .Sum(p => p.MontoPresupuestado),
                Ejecutado = detalles.Where(d => d.TipoGastoId == tipo.Id).Sum(d => d.Monto)
            }).ToList();
            return Ok(result);
        }
    }
}
