using ControlGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Core.Interfaces.Services
{
    public interface IGastoDetalleService
    {
        Task<GastoDetalle> GetByIdAsync(int id);
        Task<IEnumerable<GastoDetalle>> GetByGastoEncabezadoIdAsync(int encabezadoId);
        Task AddAsync(GastoDetalle entity);
        Task UpdateAsync(GastoDetalle entity);
        Task DeleteAsync(int id);
    }
}
