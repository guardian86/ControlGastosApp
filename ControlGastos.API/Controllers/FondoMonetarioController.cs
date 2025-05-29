using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FondoMonetarioController : ControllerBase
    {
        private readonly IFondoMonetarioService _service;
        public FondoMonetarioController(IFondoMonetarioService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FondoMonetario>> Get(int id)
        {
            var fondo = await _service.GetByIdAsync(id);
            if (fondo == null) return NotFound();
            return Ok(fondo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FondoMonetario>>> GetAll()
        {
            var fondos = await _service.GetAllAsync();
            return Ok(fondos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FondoMonetario fondo)
        {
            await _service.AddAsync(fondo);
            return CreatedAtAction(nameof(Get), new { id = fondo.Id }, fondo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FondoMonetario fondo)
        {
            if (id != fondo.Id) return BadRequest();
            await _service.UpdateAsync(fondo);
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
