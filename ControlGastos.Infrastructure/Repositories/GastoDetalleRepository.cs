using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Infrastructure.Repositories
{
    public class GastoDetalleRepository : IGastoDetalleRepository
    {
        // Implementación pendiente: usar DbContext para acceso a datos
        public Task<GastoDetalle> GetByIdAsync(int id) => throw new System.NotImplementedException();
        public Task<IEnumerable<GastoDetalle>> GetByGastoEncabezadoIdAsync(int encabezadoId) => throw new System.NotImplementedException();
        public Task AddAsync(GastoDetalle entity) => throw new System.NotImplementedException();
        public Task UpdateAsync(GastoDetalle entity) => throw new System.NotImplementedException();
        public Task DeleteAsync(int id) => throw new System.NotImplementedException();
    }
}
