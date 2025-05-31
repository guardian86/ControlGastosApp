using ControlGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Core.Interfaces.Repositories
{
    public interface IPresupuestoRepository
    {
        Task<Presupuesto> GetByIdAsync(int id);
        Task<IEnumerable<Presupuesto>> GetByUsuarioIdAsync(int usuarioId);
        Task<Presupuesto> GetByUsuarioTipoGastoMesAnioAsync(int usuarioId, int tipoGastoId, int mes, int anio);
        Task AddAsync(Presupuesto entity);
        Task UpdateAsync(Presupuesto entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<Presupuesto>> GetAllAsync();
    }
}
