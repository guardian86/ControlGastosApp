using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Infrastructure.Data;

namespace ControlGastos.Infrastructure.Repositories
{
    public class FondoMonetarioRepository : IFondoMonetarioRepository
    {
        private readonly ApplicationDbContext _context;

        public FondoMonetarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FondoMonetario entity)
        {
            await _context.FondosMonetarios.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.FondosMonetarios.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FondoMonetario>> GetAllAsync()
        {
            return await _context.FondosMonetarios.ToListAsync();
        }

        public async Task<FondoMonetario?> GetByIdAsync(int id)
        {
            return await _context.FondosMonetarios.FindAsync(id);
        }

        public async Task UpdateAsync(FondoMonetario entity)
        {
            _context.FondosMonetarios.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}