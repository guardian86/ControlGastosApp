using ControlGastos.Core.DTOs;
using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace ControlGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PresupuestosController : ControllerBase
    {
        private readonly IPresupuestoService _service;
        private readonly IMapper _mapper;
        public PresupuestosController(IPresupuestoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresupuestoDto>>> GetAll()
        {
            var presupuestos = await _service.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<PresupuestoDto>>(presupuestos);
            return Ok(dtos);
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
        public async Task<ActionResult> Post([FromBody] PresupuestoDto dto)
        {
            var entity = _mapper.Map<Presupuesto>(dto);
            await _service.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PresupuestoDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var entity = _mapper.Map<Presupuesto>(dto);
            await _service.UpdateAsync(entity);
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
