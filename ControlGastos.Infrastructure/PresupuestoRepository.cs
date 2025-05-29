using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Infrastructure.Repositories
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        // Implementación pendiente: usar DbContext para acceso a datos
        public Task<Presupuesto> GetByIdAsync(int id) => throw new System.NotImplementedException();
        public Task<IEnumerable<Presupuesto>> GetByUsuarioIdAsync(int usuarioId) => throw new System.NotImplementedException();
        public Task<Presupuesto> GetByUsuarioTipoGastoMesAnioAsync(int usuarioId, int tipoGastoId, int mes, int anio) => throw new System.NotImplementedException();
        public Task AddAsync(Presupuesto entity) => throw new System.NotImplementedException();
        public Task UpdateAsync(Presupuesto entity) => throw new System.NotImplementedException();
        public Task DeleteAsync(int id) => throw new System.NotImplementedException();
    }
}
