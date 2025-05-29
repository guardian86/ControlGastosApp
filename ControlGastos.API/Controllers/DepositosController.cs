using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositosController : ControllerBase
    {
        private readonly IDepositoService _service;
        public DepositosController(IDepositoService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Deposito>> Get(int id)
        {
            var deposito = await _service.GetByIdAsync(id);
            if (deposito == null) return NotFound();
            return Ok(deposito);
        }

        [HttpGet("fondo/{fondoId}")]
        public async Task<ActionResult<IEnumerable<Deposito>>> GetByFondo(int fondoId)
        {
            var depositos = await _service.GetByFondoMonetarioIdAsync(fondoId);
            return Ok(depositos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Deposito deposito)
        {
            await _service.AddAsync(deposito);
            return CreatedAtAction(nameof(Get), new { id = deposito.Id }, deposito);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Deposito deposito)
        {
            if (id != deposito.Id) return BadRequest();
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
