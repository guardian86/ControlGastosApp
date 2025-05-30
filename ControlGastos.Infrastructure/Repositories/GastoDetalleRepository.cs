// En ControlGastos.Infrastructure/Repositories/GastoDetalleRepository.cs
using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Infrastructure.Data; // Asegúrate que este es el namespace de tu DbContext
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Infrastructure.Repositories
{
    public class GastoDetalleRepository : IGastoDetalleRepository
    {
        private readonly ApplicationDbContext _context;

        
        public GastoDetalleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<GastoDetalle?> GetByIdAsync(int id)
        {
            return await _context.GastoDetalles.FindAsync(id);
        }

        public async Task<IEnumerable<GastoDetalle>> GetByGastoEncabezadoIdAsync(int encabezadoId)
        {

            return await _context.GastoDetalles
                                 .Where(d => d.GastoEncabezadoId == encabezadoId)
                                 .ToListAsync();
        }

        public async Task AddAsync(GastoDetalle entity)
        {
            await _context.GastoDetalles.AddAsync(entity);
        }

        public Task UpdateAsync(GastoDetalle entity)
        {
            
            _context.GastoDetalles.Update(entity);
            
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                
                _context.GastoDetalles.Remove(entity);
                
            }
        }
    }
}