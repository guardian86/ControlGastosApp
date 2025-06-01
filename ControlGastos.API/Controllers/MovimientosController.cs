using ControlGastos.Core.DTOs;
using ControlGastos.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class MovimientosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MovimientosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoDto>>> Get([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            var depositos = await _context.Depositos
                .Include(d => d.FondoMonetario)
                .Where(d => d.Fecha >= fechaInicio && d.Fecha <= fechaFin)
                .Select(d => new MovimientoDto
                {
                    Tipo = "Depósito",
                    Fecha = d.Fecha,
                    FondoMonetarioNombre = d.FondoMonetario != null ? d.FondoMonetario.Nombre : string.Empty,
                    Monto = d.Monto,
                    Descripcion = $"Depósito en {d.FondoMonetario.Nombre}"
                })
                .ToListAsync();

            var gastos = await _context.GastoEncabezados
                .Include(g => g.FondoMonetario)
                .Where(g => g.Fecha >= fechaInicio && g.Fecha <= fechaFin)
                .Select(g => new MovimientoDto
                {
                    Tipo = "Gasto",
                    Fecha = g.Fecha,
                    FondoMonetarioNombre = g.FondoMonetario != null ? g.FondoMonetario.Nombre : string.Empty,
                    Monto = g.Detalles.Sum(d => d.Monto),
                    Descripcion = g.Observaciones
                })
                .ToListAsync();

            var movimientos = depositos.Concat(gastos)
                .OrderByDescending(m => m.Fecha)
                .ToList();

            return Ok(movimientos);
        }
    }
}
