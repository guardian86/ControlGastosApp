using AutoMapper;
using ControlGastos.Core.DTOs;
using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlGastos.Infrastructure.Data;

namespace ControlGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositosController : ControllerBase
    {
        private readonly IDepositoService _service;
        private readonly ApplicationDbContext _context;
        public DepositosController(IDepositoService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepositoDto>>> GetAll()
        {
            var depositos = await _context.Depositos.Include(d => d.FondoMonetario).ToListAsync();
            var dtos = depositos.Select(d => new DepositoDto
            {
                Id = d.Id,
                Fecha = d.Fecha,
                FondoMonetarioNombre = d.FondoMonetario != null ? d.FondoMonetario.Nombre : string.Empty,
                Monto = d.Monto
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepositoDto>> Get(int id)
        {
            var deposito = await _context.Depositos.Include(d => d.FondoMonetario).FirstOrDefaultAsync(d => d.Id == id);
            if (deposito == null) return NotFound();
            var dto = new DepositoDto
            {
                Id = deposito.Id,
                Fecha = deposito.Fecha,
                FondoMonetarioNombre = deposito.FondoMonetario != null ? deposito.FondoMonetario.Nombre : string.Empty,
                Monto = deposito.Monto
            };
            return Ok(dto);
        }

        [HttpGet("fondo/{fondoId}")]
        public async Task<ActionResult<IEnumerable<DepositoDto>>> GetByFondo(int fondoId)
        {
            var depositos = await _context.Depositos.Include(d => d.FondoMonetario).Where(d => d.FondoMonetarioId == fondoId).ToListAsync();
            var dtos = depositos.Select(d => new DepositoDto
            {
                Id = d.Id,
                Fecha = d.Fecha,
                FondoMonetarioNombre = d.FondoMonetario != null ? d.FondoMonetario.Nombre : string.Empty,
                Monto = d.Monto
            }).ToList();
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DepositoDto depositoDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var fondo = await _context.FondosMonetarios.FirstOrDefaultAsync(f => f.Nombre == depositoDto.FondoMonetarioNombre);
            if (fondo == null) return BadRequest("Fondo monetario no válido");
            var deposito = new Deposito
            {
                Fecha = depositoDto.Fecha,
                FondoMonetarioId = fondo.Id,
                Monto = depositoDto.Monto
            };
            await _service.AddAsync(deposito);
            depositoDto.Id = deposito.Id;
            return CreatedAtAction(nameof(Get), new { id = deposito.Id }, depositoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DepositoDto depositoDto)
        {
            if (id != depositoDto.Id) return BadRequest();
            var fondo = await _context.FondosMonetarios.FirstOrDefaultAsync(f => f.Nombre == depositoDto.FondoMonetarioNombre);
            if (fondo == null) return BadRequest("Fondo monetario no válido");
            var deposito = await _context.Depositos.FindAsync(id);
            if (deposito == null) return NotFound();
            deposito.Fecha = depositoDto.Fecha;
            deposito.FondoMonetarioId = fondo.Id;
            deposito.Monto = depositoDto.Monto;
            await _service.UpdateAsync(deposito);
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
