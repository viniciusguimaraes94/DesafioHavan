using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Repository.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repository.Services
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext _context;

        public ClienteRepository(DataContext context)
        {
            this._context = context; 
        }
        public async Task<Cliente> ObterPeloId(int id, bool incluirOperacao)
        {
            IQueryable<Cliente> consulta = this._context.Cliente;
            
            if (incluirOperacao)
            {
                consulta = consulta.Include(C => C.operacoes);
            }

            consulta = consulta.AsNoTracking()
                            .OrderBy(p => p.id)
                            .Where(p => p.id == id);

            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterTodos(bool incluirOperacao)
        {
            IQueryable<Cliente> consulta = this._context.Cliente;

            if (incluirOperacao)
            {
                consulta = consulta.Include(c => c.operacoes);
            }

            consulta = consulta.AsNoTracking().OrderBy(p => p.id);
            return await consulta.ToArrayAsync();
        }
    }
}