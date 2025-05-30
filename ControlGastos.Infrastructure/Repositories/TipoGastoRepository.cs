using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Infrastructure.Data;

namespace ControlGastos.Infrastructure.Repositories
{
    public class TipoGastoRepository : ITipoGastoRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoGastoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TipoGasto entity)
        {
            await _context.TiposGasto.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.TiposGasto.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TipoGasto>> GetAllAsync()
        {
            return await _context.TiposGasto.ToListAsync();
        }

        public async Task<TipoGasto?> GetByIdAsync(int id)
        {
            return await _context.TiposGasto.FindAsync(id);
        }

        public async Task UpdateAsync(TipoGasto entity)
        {
            _context.TiposGasto.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}