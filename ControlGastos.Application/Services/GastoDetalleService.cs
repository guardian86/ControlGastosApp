using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlGastos.Application.Services
{
    public class GastoDetalleService : IGastoDetalleService
    {
        private readonly IGastoDetalleRepository _repository;
        public GastoDetalleService(IGastoDetalleRepository repository)
        {
            _repository = repository;
        }
        public Task<GastoDetalle> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<GastoDetalle>> GetByGastoEncabezadoIdAsync(int encabezadoId) => _repository.GetByGastoEncabezadoIdAsync(encabezadoId);
        public Task AddAsync(GastoDetalle entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(GastoDetalle entity) => _repository.UpdateAsync(entity);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
