using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Application.Services
{
    public class TipoGastoService : ITipoGastoService
    {
        private readonly ITipoGastoRepository _repository;
        public TipoGastoService(ITipoGastoRepository repository)
        {
            _repository = repository;
        }
        public Task<TipoGasto> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<TipoGasto>> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(TipoGasto entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(TipoGasto entity) => _repository.UpdateAsync(entity);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
