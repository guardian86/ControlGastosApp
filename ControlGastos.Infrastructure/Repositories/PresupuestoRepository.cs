using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Infrastructure.Data;

namespace ControlGastos.Infrastructure.Repositories
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        private readonly ApplicationDbContext _context;

        public PresupuestoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Presupuesto>> GetAllAsync()
        {
            return await _context.Presupuestos.ToListAsync();
        }

        public async Task<Presupuesto?> GetByIdAsync(int id)
        {
            return await _context.Presupuestos.FindAsync(id);
        }

        public async Task<IEnumerable<Presupuesto>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Presupuestos
                                 .Where(p => p.UsuarioId == usuarioId)
                                 .ToListAsync();
        }

        public async Task<Presupuesto?> GetByUsuarioTipoGastoMesAnioAsync(int usuarioId, int tipoGastoId, int mes, int anio)
        {
            return await _context.Presupuestos
                                 .Where(p => p.UsuarioId == usuarioId &&
                                             p.TipoGastoId == tipoGastoId &&
                                             p.Mes == mes &&
                                             p.Anio == anio)
                                 .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Presupuesto entity)
        {
            await _context.Presupuestos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Presupuesto entity)
        {
            _context.Presupuestos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Presupuestos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

       
    }
}