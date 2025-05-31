using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Infrastructure.Data;

namespace ControlGastos.Infrastructure.Repositories
{
    public class GastoEncabezadoRepository : IGastoEncabezadoRepository
    {
        private readonly ApplicationDbContext _context;

        public GastoEncabezadoRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<GastoEncabezado>> GetAllAsync()
        {
            return await _context.GastoEncabezados.ToListAsync();
        }

        public async Task<GastoEncabezado?> GetByIdAsync(int id)
        {
            return await _context.GastoEncabezados.FindAsync(id);
        }

        public async Task<IEnumerable<GastoEncabezado>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.GastoEncabezados
                                 .Where(g => g.Id == usuarioId)
                                 .ToListAsync();
        }

        public async Task AddAsync(GastoEncabezado entity)
        {
            await _context.GastoEncabezados.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GastoEncabezado entity)
        {
            _context.GastoEncabezados.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.GastoEncabezados.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}