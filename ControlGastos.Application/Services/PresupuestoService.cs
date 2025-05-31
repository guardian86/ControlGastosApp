using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Application.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IPresupuestoRepository _repository;
        public PresupuestoService(IPresupuestoRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Presupuesto>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Presupuesto> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<Presupuesto>> GetByUsuarioIdAsync(int usuarioId) => _repository.GetByUsuarioIdAsync(usuarioId);
        public Task<Presupuesto> GetByUsuarioTipoGastoMesAnioAsync(int usuarioId, int tipoGastoId, int mes, int anio) => _repository.GetByUsuarioTipoGastoMesAnioAsync(usuarioId, tipoGastoId, mes, anio);
        public Task AddAsync(Presupuesto entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(Presupuesto entity) => _repository.UpdateAsync(entity);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);

       
    }
}
