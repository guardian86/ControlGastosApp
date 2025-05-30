using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Infrastructure.Repositories
{
    public class GastoEncabezadoRepository : IGastoEncabezadoRepository
    {
        // Implementación pendiente: usar DbContext para acceso a datos
        public Task<GastoEncabezado> GetByIdAsync(int id) => throw new System.NotImplementedException();
        public Task<IEnumerable<GastoEncabezado>> GetByUsuarioIdAsync(int usuarioId) => throw new System.NotImplementedException();
        public Task AddAsync(GastoEncabezado entity) => throw new System.NotImplementedException();
        public Task UpdateAsync(GastoEncabezado entity) => throw new System.NotImplementedException();
        public Task DeleteAsync(int id) => throw new System.NotImplementedException();
    }
}
