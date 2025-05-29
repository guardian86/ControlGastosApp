using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Infrastructure.Repositories
{
    public class DepositoRepository : IDepositoRepository
    {
        // Implementación pendiente: usar DbContext para acceso a datos
        public Task<Deposito> GetByIdAsync(int id) => throw new System.NotImplementedException();
        public Task<IEnumerable<Deposito>> GetByFondoMonetarioIdAsync(int fondoId) => throw new System.NotImplementedException();
        public Task AddAsync(Deposito entity) => throw new System.NotImplementedException();
        public Task UpdateAsync(Deposito entity) => throw new System.NotImplementedException();
        public Task DeleteAsync(int id) => throw new System.NotImplementedException();
    }
}
