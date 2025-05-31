using ControlGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Core.Interfaces.Services
{
    public interface IPresupuestoService
    {
        Task<IEnumerable<Presupuesto>> GetAllAsync();
        Task<Presupuesto> GetByIdAsync(int id);
        Task<IEnumerable<Presupuesto>> GetByUsuarioIdAsync(int usuarioId);
        Task<Presupuesto> GetByUsuarioTipoGastoMesAnioAsync(int usuarioId, int tipoGastoId, int mes, int anio);
        Task AddAsync(Presupuesto entity);
        Task UpdateAsync(Presupuesto entity);
        Task DeleteAsync(int id);
    }
}
