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
    public class GastoDetalleController : ControllerBase
    {
        private readonly IGastoDetalleService _service;
        public GastoDetalleController(IGastoDetalleService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GastoDetalle>> Get(int id)
        {
            var detalle = await _service.GetByIdAsync(id);
            if (detalle == null) return NotFound();
            return Ok(detalle);
        }

        [HttpGet("encabezado/{encabezadoId}")]
        public async Task<ActionResult<IEnumerable<GastoDetalle>>> GetByEncabezado(int encabezadoId)
        {
            var detalles = await _service.GetByGastoEncabezadoIdAsync(encabezadoId);
            return Ok(detalles);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GastoDetalle detalle)
        {
            await _service.AddAsync(detalle);
            return CreatedAtAction(nameof(Get), new { id = detalle.Id }, detalle);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GastoDetalle detalle)
        {
            if (id != detalle.Id) return BadRequest();
            await _service.UpdateAsync(detalle);
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
