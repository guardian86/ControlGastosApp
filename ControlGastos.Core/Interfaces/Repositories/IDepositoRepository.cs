using ControlGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Core.Interfaces.Repositories
{
    public interface IDepositoRepository
    {
        Task<Deposito> GetByIdAsync(int id);
        Task<IEnumerable<Deposito>> GetByFondoMonetarioIdAsync(int fondoId);
        Task AddAsync(Deposito entity);
        Task UpdateAsync(Deposito entity);
        Task DeleteAsync(int id);
    }
}
