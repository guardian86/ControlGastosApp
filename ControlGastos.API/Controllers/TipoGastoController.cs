using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoGastoController : ControllerBase
    {
        private readonly ITipoGastoService _service;
        public TipoGastoController(ITipoGastoService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoGasto>> Get(int id)
        {
            var tipo = await _service.GetByIdAsync(id);
            if (tipo == null) return NotFound();
            return Ok(tipo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoGasto>>> GetAll()
        {
            var tipos = await _service.GetAllAsync();
            return Ok(tipos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoGasto tipo)
        {
            await _service.AddAsync(tipo);
            return CreatedAtAction(nameof(Get), new { id = tipo.Id }, tipo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoGasto tipo)
        {
            if (id != tipo.Id) return BadRequest();
            await _service.UpdateAsync(tipo);
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
