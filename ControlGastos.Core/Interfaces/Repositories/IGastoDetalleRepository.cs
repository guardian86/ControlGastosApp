using ControlGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Core.Interfaces.Repositories
{
    public interface IGastoDetalleRepository
    {
        Task<GastoDetalle> GetByIdAsync(int id);
        Task<IEnumerable<GastoDetalle>> GetByGastoEncabezadoIdAsync(int encabezadoId);
        Task AddAsync(GastoDetalle entity);
        Task UpdateAsync(GastoDetalle entity);
        Task DeleteAsync(int id);
    }
}
