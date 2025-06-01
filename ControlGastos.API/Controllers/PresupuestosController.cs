using ControlGastos.Core.DTOs;
using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PresupuestosController : ControllerBase
    {
        private readonly IPresupuestoService _service;
        public PresupuestosController(IPresupuestoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresupuestoDto>>> GetAll()
        {
            var presupuestos = await _service.GetAllAsync();
            return Ok(presupuestos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Presupuesto>> Get(int id)
        {
            var presupuesto = await _service.GetByIdAsync(id);
            if (presupuesto == null) return NotFound();
            return Ok(presupuesto);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Presupuesto>>> GetByUsuario(int usuarioId)
        {
            var presupuestos = await _service.GetByUsuarioIdAsync(usuarioId);
            return Ok(presupuestos);
        }

        [HttpGet("usuario/{usuarioId}/tipo/{tipoGastoId}/mes/{mes}/anio/{anio}")]
        public async Task<ActionResult<Presupuesto>> GetByUsuarioTipoGastoMesAnio(int usuarioId, int tipoGastoId, int mes, int anio)
        {
            var presupuesto = await _service.GetByUsuarioTipoGastoMesAnioAsync(usuarioId, tipoGastoId, mes, anio);
            if (presupuesto == null) return NotFound();
            return Ok(presupuesto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Presupuesto presupuesto)
        {
            await _service.AddAsync(presupuesto);
            return CreatedAtAction(nameof(Get), new { id = presupuesto.Id }, presupuesto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Presupuesto presupuesto)
        {
            if (id != presupuesto.Id) return BadRequest();
            await _service.UpdateAsync(presupuesto);
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
