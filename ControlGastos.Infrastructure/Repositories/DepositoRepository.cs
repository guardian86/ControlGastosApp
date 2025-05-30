using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlGastos.Infrastructure.Repositories
{
    public class DepositoRepository : IDepositoRepository
    {
        private readonly ApplicationDbContext _context;

        public DepositoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Deposito?> GetByIdAsync(int id)
        {
            return await _context.Depositos.FindAsync(id);
        }

        public async Task<IEnumerable<Deposito>> GetByFondoMonetarioIdAsync(int fondoId)
        {
            return await _context.Depositos
                                 .Where(d => d.FondoMonetarioId == fondoId)
                                 .ToListAsync();
        }

        public async Task AddAsync(Deposito entity)
        {
            await _context.Depositos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Deposito entity)
        {
            _context.Depositos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Depositos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}