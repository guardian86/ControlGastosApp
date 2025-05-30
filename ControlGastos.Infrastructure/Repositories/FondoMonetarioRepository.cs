using ControlGastos.Core.Entities;
using ControlGastos.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGastos.Infrastructure.Repositories
{
    public class FondoMonetarioRepository : IFondoMonetarioRepository
    {
        public Task AddAsync(FondoMonetario entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FondoMonetario>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FondoMonetario> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(FondoMonetario entity)
        {
            throw new NotImplementedException();
        }
    }
}
