using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Application.Services
{
    public class DepositoService : IDepositoService
    {
        private readonly IDepositoRepository _repository;
        public DepositoService(IDepositoRepository repository)
        {
            _repository = repository;
        }

        public Task<Deposito> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<Deposito>> GetByFondoMonetarioIdAsync(int fondoId) => _repository.GetByFondoMonetarioIdAsync(fondoId);
        public Task AddAsync(Deposito entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(Deposito entity) => _repository.UpdateAsync(entity);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
