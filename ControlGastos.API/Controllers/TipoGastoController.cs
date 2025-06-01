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
    public class TipoGastoController : ControllerBase
    {
        private readonly ITipoGastoService _service;
        private readonly IMapper _mapper;
        public TipoGastoController(ITipoGastoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoGastoDto>> Get(int id)
        {
            var tipo = await _service.GetByIdAsync(id);
            if (tipo == null) return NotFound();
            return Ok(_mapper.Map<TipoGastoDto>(tipo));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoGastoDto>>> GetAll()
        {
            var tipos = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TipoGastoDto>>(tipos));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoGastoDto tipoDto)
        {
            // Calcular el siguiente código correlativo automáticamente
            var tipos = await _service.GetAllAsync();
            int max = 0;
            foreach (var t in tipos)
            {
                if (int.TryParse(t.Codigo, out int cod) && cod > max)
                    max = cod;
            }
            var nextCodigo = (max + 1).ToString("D3");
            var tipo = _mapper.Map<TipoGasto>(tipoDto);
            tipo.Codigo = nextCodigo; // Sobrescribe el código
            await _service.AddAsync(tipo);
            tipoDto.Id = tipo.Id;
            tipoDto.Codigo = tipo.Codigo;
            return CreatedAtAction(nameof(Get), new { id = tipo.Id }, tipoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoGastoDto tipoDto)
        {
            if (id != tipoDto.Id) return BadRequest();
            var tipo = _mapper.Map<TipoGasto>(tipoDto);
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
