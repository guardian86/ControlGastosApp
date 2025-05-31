using ControlGastos.Core.DTOs;
using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastoEncabezadoController : ControllerBase
    {
        private readonly IGastoEncabezadoService _service;
        public GastoEncabezadoController(IGastoEncabezadoService service)
        {
            _service = service;
        }


        [HttpGet] 
        public async Task<ActionResult<IEnumerable<GastoEncabezadoDto>>> GetAllGastos() 
        {
            var gastos = await _service.GetAllAsync(); 
            return Ok(gastos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GastoEncabezado>> Get(int id)
        {
            var encabezado = await _service.GetByIdAsync(id);
            if (encabezado == null) return NotFound();
            return Ok(encabezado);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<GastoEncabezado>>> GetByUsuario(int usuarioId)
        {
            var encabezados = await _service.GetByUsuarioIdAsync(usuarioId);
            return Ok(encabezados);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GastoEncabezado encabezado)
        {
            await _service.AddAsync(encabezado);
            return CreatedAtAction(nameof(Get), new { id = encabezado.Id }, encabezado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GastoEncabezado encabezado)
        {
            if (id != encabezado.Id) return BadRequest();
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
