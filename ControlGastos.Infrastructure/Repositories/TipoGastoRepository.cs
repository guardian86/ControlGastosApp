using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGastos.Infrastructure.Repositories
{
    public class TipoGastoRepository : ITipoGastoRepository
    {
        public Task AddAsync(TipoGasto entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TipoGasto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TipoGasto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TipoGasto entity)
        {
            throw new NotImplementedException();
        }
    }
}
