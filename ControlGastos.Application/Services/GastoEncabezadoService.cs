using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Application.Services
{
    public class GastoEncabezadoService : IGastoEncabezadoService
    {
        private readonly IGastoEncabezadoRepository _repository;
        public GastoEncabezadoService(IGastoEncabezadoRepository repository)
        {
            _repository = repository;
        }
        public Task<GastoEncabezado> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<GastoEncabezado>> GetByUsuarioIdAsync(int usuarioId) => _repository.GetByUsuarioIdAsync(usuarioId);
        public Task AddAsync(GastoEncabezado entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(GastoEncabezado entity) => _repository.UpdateAsync(entity);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
