using ControlGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Core.Interfaces.Services
{
    public interface IFondoMonetarioService
    {
        Task<FondoMonetario> GetByIdAsync(int id);
        Task<IEnumerable<FondoMonetario>> GetAllAsync();
        Task AddAsync(FondoMonetario entity);
        Task UpdateAsync(FondoMonetario entity);
        Task DeleteAsync(int id);
    }
}
