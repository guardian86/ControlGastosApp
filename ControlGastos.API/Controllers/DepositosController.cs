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
                FondoMonetarioId = d.FondoMonetarioId, 
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
        public async Task<ActionResult<DepositoDto>> Post([FromBody] DepositoDto depositoDto) 
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fondoMonetarioExiste = await _context.FondosMonetarios.AnyAsync(f => f.Id == depositoDto.FondoMonetarioId);
            if (!fondoMonetarioExiste)
            {
                // Puedes añadir un error específico al ModelState para el campo FondoMonetarioId
                ModelState.AddModelError(nameof(depositoDto.FondoMonetarioId), "El fondo monetario seleccionado no existe.");
                return BadRequest(ModelState);
            }

            
            var deposito = new Deposito 
            {
                Fecha = depositoDto.Fecha,
                FondoMonetarioId = depositoDto.FondoMonetarioId,
                Monto = depositoDto.Monto
                
            };

            
            await _service.AddAsync(deposito);


            depositoDto.Id = deposito.Id; 
            depositoDto.FondoMonetarioId = deposito.FondoMonetarioId; 

            var fondoInfo = await _context.FondosMonetarios.FindAsync(deposito.FondoMonetarioId);
            depositoDto.FondoMonetarioNombre = fondoInfo?.Nombre; 

            return CreatedAtAction(nameof(Get), new { id = deposito.Id }, depositoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DepositoDto depositoDto)
        {
            if (id != depositoDto.Id) return BadRequest("El ID de la ruta no coincide con el ID del cuerpo."); 

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           // Verificar si el FondoMonetarioId del DTO existe
            var fondoMonetarioExiste = await _context.FondosMonetarios.AnyAsync(f => f.Id == depositoDto.FondoMonetarioId);
            if (!fondoMonetarioExiste)
            {
                ModelState.AddModelError(nameof(depositoDto.FondoMonetarioId), "El fondo monetario seleccionado no existe.");
                return BadRequest(ModelState);
            }

            var deposito = await _context.Depositos.FindAsync(id); 
            if (deposito == null)
            {
                return NotFound("El depósito que intentas actualizar no fue encontrado.");
            }

            deposito.Fecha = depositoDto.Fecha;
            deposito.FondoMonetarioId = depositoDto.FondoMonetarioId; 
            deposito.Monto = depositoDto.Monto;
            
            try
            {
                await _service.UpdateAsync(deposito);
            }
            catch (DbUpdateConcurrencyException)
            {
               
                if (!await DepositoExists(id)) 
                {
                    return NotFound();
                }
                else
                {
                    throw; 
                }
            }

            return NoContent(); 
        }

    
        private async Task<bool> DepositoExists(int id)
        {
            return await _context.Depositos.AnyAsync(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }


    }
}
