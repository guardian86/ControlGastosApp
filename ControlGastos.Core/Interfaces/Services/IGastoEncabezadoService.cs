using ControlGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Core.Interfaces.Services
{
    public interface IGastoEncabezadoService
    {
        Task<GastoEncabezado> GetByIdAsync(int id);
        Task<IEnumerable<GastoEncabezado>> GetByUsuarioIdAsync(int usuarioId);
        Task AddAsync(GastoEncabezado entity);
        Task UpdateAsync(GastoEncabezado entity);
        Task DeleteAsync(int id);
    }
}
