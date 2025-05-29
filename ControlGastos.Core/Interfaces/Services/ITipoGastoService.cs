using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlGastos.Core.Entities;

namespace ControlGastos.Core.Interfaces.Services
{
    public interface ITipoGastoService
    {
        Task<TipoGasto> GetByIdAsync(int id);
        Task<IEnumerable<TipoGasto>> GetAllAsync();
        Task AddAsync(TipoGasto entity);
        Task UpdateAsync(TipoGasto entity);
        Task DeleteAsync(int id);
    }
}
