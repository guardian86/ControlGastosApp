using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlGastos.Core.Entities;

namespace ControlGastos.Core.Interfaces.Repositories
{
    public interface IFondoMonetarioRepository
    {
        Task<FondoMonetario> GetByIdAsync(int id);
        Task<IEnumerable<FondoMonetario>> GetAllAsync();
        Task AddAsync(FondoMonetario entity);
        Task UpdateAsync(FondoMonetario entity);
        Task DeleteAsync(int id);
    }
}
