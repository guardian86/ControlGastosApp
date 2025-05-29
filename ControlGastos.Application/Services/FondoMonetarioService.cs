using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Application.Services
{
    public class FondoMonetarioService : IFondoMonetarioService
    {
        private readonly IFondoMonetarioRepository _repository;
        public FondoMonetarioService(IFondoMonetarioRepository repository)
        {
            _repository = repository;
        }
        public Task<FondoMonetario> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<FondoMonetario>> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(FondoMonetario entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(FondoMonetario entity) => _repository.UpdateAsync(entity);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
