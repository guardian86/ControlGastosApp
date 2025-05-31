using AutoMapper;
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
    public class FondoMonetarioController : ControllerBase
    {
        private readonly IFondoMonetarioService _service;
        private readonly IMapper _mapper;
        public FondoMonetarioController(IFondoMonetarioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FondoMonetarioDto>> Get(int id)
        {
            var fondo = await _service.GetByIdAsync(id);
            if (fondo == null) return NotFound();
            return Ok(_mapper.Map<FondoMonetarioDto>(fondo));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FondoMonetarioDto>>> GetAll()
        {
            var fondos = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<FondoMonetarioDto>>(fondos));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FondoMonetarioDto fondoDto)
        {
            var fondo = _mapper.Map<FondoMonetario>(fondoDto);
            await _service.AddAsync(fondo);
            fondoDto.Id = fondo.Id;
            return CreatedAtAction(nameof(Get), new { id = fondo.Id }, fondoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FondoMonetarioDto fondoDto)
        {
            if (id != fondoDto.Id) return BadRequest();
            var fondo = _mapper.Map<FondoMonetario>(fondoDto);
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
