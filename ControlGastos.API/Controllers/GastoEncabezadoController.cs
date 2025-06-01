using ControlGastos.Core.DTOs;
using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControlGastos.Infrastructure.Data;

namespace ControlGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastoEncabezadoController : ControllerBase
    {
        private readonly IGastoEncabezadoService _service;
        private readonly ApplicationDbContext _context;
        public GastoEncabezadoController(IGastoEncabezadoService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GastoEncabezadoDto>>> GetAllGastos()
        {
            var gastos = await _context.GastoEncabezados
                .Include(g => g.FondoMonetario)
                .Include(g => g.Detalles)
                    .ThenInclude(d => d.TipoGasto)
                .ToListAsync();
            var dtos = gastos.Select(g => new GastoEncabezadoDto
            {
                Id = g.Id,
                Fecha = g.Fecha,
                FondoMonetarioNombre = g.FondoMonetario != null ? g.FondoMonetario.Nombre : string.Empty,
                Observaciones = g.Observaciones,
                NombreComercio = g.NombreComercio,
                TipoDocumento = g.TipoDocumento,
                Detalles = g.Detalles.Select(d => new GastoDetalleDto
                {
                    TipoGastoNombre = d.TipoGasto != null ? d.TipoGasto.Nombre : string.Empty,
                    Monto = d.Monto
                }).ToList()
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GastoEncabezadoDto>> Get(int id)
        {
            var g = await _context.GastoEncabezados
                .Include(x => x.FondoMonetario)
                .Include(x => x.Detalles).ThenInclude(d => d.TipoGasto)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (g == null) return NotFound();
            var dto = new GastoEncabezadoDto
            {
                Id = g.Id,
                Fecha = g.Fecha,
                FondoMonetarioNombre = g.FondoMonetario != null ? g.FondoMonetario.Nombre : string.Empty,
                Observaciones = g.Observaciones,
                NombreComercio = g.NombreComercio,
                TipoDocumento = g.TipoDocumento,
                Detalles = g.Detalles.Select(d => new GastoDetalleDto
                {
                    TipoGastoNombre = d.TipoGasto != null ? d.TipoGasto.Nombre : string.Empty,
                    Monto = d.Monto
                }).ToList()
            };
            return Ok(dto);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<GastoEncabezadoDto>>> GetByUsuario(int usuarioId)
        {
            var gastos = await _context.GastoEncabezados
                .Include(g => g.FondoMonetario)
                .Include(g => g.Detalles).ThenInclude(d => d.TipoGasto)
                .Where(g => g.FondoMonetario != null && g.FondoMonetario.Id == usuarioId)
                .ToListAsync();
            var dtos = gastos.Select(g => new GastoEncabezadoDto
            {
                Id = g.Id,
                Fecha = g.Fecha,
                FondoMonetarioNombre = g.FondoMonetario != null ? g.FondoMonetario.Nombre : string.Empty,
                Observaciones = g.Observaciones,
                NombreComercio = g.NombreComercio,
                TipoDocumento = g.TipoDocumento,
                Detalles = g.Detalles.Select(d => new GastoDetalleDto
                {
                    TipoGastoNombre = d.TipoGasto != null ? d.TipoGasto.Nombre : string.Empty,
                    Monto = d.Monto
                }).ToList()
            }).ToList();
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GastoEncabezadoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (dto.Detalles == null || !dto.Detalles.Any())
                return BadRequest("Debe ingresar al menos un detalle de gasto.");

            var fondo = await _context.FondosMonetarios.FirstOrDefaultAsync(f => f.Nombre == dto.FondoMonetarioNombre);
            if (fondo == null) return BadRequest("Fondo monetario no válido");

            // Validación de sobregiro de presupuesto
            var alertas = new List<string>();
            foreach (var d in dto.Detalles)
            {
                var tipoGasto = await _context.TiposGasto.FirstOrDefaultAsync(t => t.Nombre == d.TipoGastoNombre);
                if (tipoGasto == null) continue;
                var mes = dto.Fecha.Month;
                var anio = dto.Fecha.Year;
                var presupuesto = await _context.Presupuestos.FirstOrDefaultAsync(p => p.TipoGastoId == tipoGasto.Id && p.Mes == mes && p.Anio == anio);
                var ejecutado = _context.GastoDetalles
                    .Include(gd => gd.GastoEncabezado)
                    .Where(gd => gd.TipoGastoId == tipoGasto.Id && gd.GastoEncabezado.Fecha.Month == mes && gd.GastoEncabezado.Fecha.Year == anio)
                    .Sum(gd => gd.Monto);
                var total = ejecutado + d.Monto;
                if (presupuesto != null && total > presupuesto.MontoPresupuestado)
                {
                    alertas.Add($"Tipo de Gasto: {tipoGasto.Nombre} - Presupuestado: {presupuesto.MontoPresupuestado:C}, Ejecutado: {ejecutado:C}, Nuevo Gasto: {d.Monto:C}, Sobregiro: {(total - presupuesto.MontoPresupuestado):C}");
                }
            }
            if (alertas.Any())
            {
                return BadRequest(new { message = "Presupuesto sobregirado en los siguientes tipos de gasto:", detalles = alertas });
            }

            var detalles = new List<GastoDetalle>();
            foreach (var d in dto.Detalles)
            {
                var tipoGasto = await _context.TiposGasto.FirstOrDefaultAsync(t => t.Nombre == d.TipoGastoNombre);
                detalles.Add(new GastoDetalle
                {
                    TipoGastoId = tipoGasto?.Id ?? 0,
                    Monto = d.Monto
                });
            }
            var encabezado = new GastoEncabezado
            {
                Fecha = dto.Fecha,
                FondoMonetarioId = fondo.Id,
                Observaciones = dto.Observaciones,
                NombreComercio = dto.NombreComercio,
                TipoDocumento = dto.TipoDocumento,
                Detalles = detalles
            };
            await _service.AddAsync(encabezado);
            dto.Id = encabezado.Id;
            return CreatedAtAction(nameof(Get), new { id = encabezado.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GastoEncabezadoDto dto)
        {
            if (id != dto.Id) return BadRequest();
            if (dto.Detalles == null || !dto.Detalles.Any())
                return BadRequest("Debe ingresar al menos un detalle de gasto.");

            var encabezado = await _context.GastoEncabezados.Include(x => x.Detalles).FirstOrDefaultAsync(x => x.Id == id);
            if (encabezado == null) return NotFound();
            var fondo = await _context.FondosMonetarios.FirstOrDefaultAsync(f => f.Nombre == dto.FondoMonetarioNombre);
            if (fondo == null) return BadRequest("Fondo monetario no válido");

            // Validación de sobregiro de presupuesto
            var alertas = new List<string>();
            foreach (var d in dto.Detalles)
            {
                var tipoGasto = await _context.TiposGasto.FirstOrDefaultAsync(t => t.Nombre == d.TipoGastoNombre);
                if (tipoGasto == null) continue;
                var mes = dto.Fecha.Month;
                var anio = dto.Fecha.Year;
                var presupuesto = await _context.Presupuestos.FirstOrDefaultAsync(p => p.TipoGastoId == tipoGasto.Id && p.Mes == mes && p.Anio == anio);
                var ejecutado = _context.GastoDetalles
                    .Include(gd => gd.GastoEncabezado)
                    .Where(gd => gd.TipoGastoId == tipoGasto.Id && gd.GastoEncabezado.Fecha.Month == mes && gd.GastoEncabezado.Fecha.Year == anio && gd.GastoEncabezado.Id != id)
                    .Sum(gd => gd.Monto);
                var total = ejecutado + d.Monto;
                if (presupuesto != null && total > presupuesto.MontoPresupuestado)
                {
                    alertas.Add($"Tipo de Gasto: {tipoGasto.Nombre} - Presupuestado: {presupuesto.MontoPresupuestado:C}, Ejecutado: {ejecutado:C}, Nuevo Gasto: {d.Monto:C}, Sobregiro: {(total - presupuesto.MontoPresupuestado):C}");
                }
            }
            if (alertas.Any())
            {
                return BadRequest(new { message = "Presupuesto sobregirado en los siguientes tipos de gasto:", detalles = alertas });
            }

            encabezado.Fecha = dto.Fecha;
            encabezado.FondoMonetarioId = fondo.Id;
            encabezado.Observaciones = dto.Observaciones;
            encabezado.NombreComercio = dto.NombreComercio;
            encabezado.TipoDocumento = dto.TipoDocumento;
            encabezado.Detalles.Clear();
            foreach (var d in dto.Detalles)
            {
                var tipoGasto = await _context.TiposGasto.FirstOrDefaultAsync(t => t.Nombre == d.TipoGastoNombre);
                var detalle = new GastoDetalle
                {
                    TipoGastoId = tipoGasto?.Id ?? 0,
                    Monto = d.Monto
                };
                encabezado.Detalles.Add(detalle);
            }
            await _service.UpdateAsync(encabezado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
