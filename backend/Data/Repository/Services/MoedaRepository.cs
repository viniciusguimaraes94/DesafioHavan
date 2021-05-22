using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Repository.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repository.Services
{
    public class MoedaRepository : IMoedaRepository
    {
        private readonly DataContext _context;

        public MoedaRepository(DataContext context)
        {
            this._context = context; 
        }

        public async Task<Moeda> ObterPeloId(int id, bool incluirOperacao)
        {
            IQueryable<Moeda> consulta = this._context.Moeda;
            
            if (incluirOperacao)
            {
                consulta = consulta.Include(m => m.operacoesDestino);
                consulta = consulta.Include(m => m.operacoesOrigem);
            }

            consulta = consulta.AsNoTracking()
                            .OrderBy(m => m.id)
                            .Where(m => m.id == id);

            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Moeda>> ObterTodos(bool incluirOperacao)
        {
            IQueryable<Moeda> consulta = this._context.Moeda;

            if (incluirOperacao)
            {
                consulta = consulta.Include(c => c.operacoesDestino);
                consulta = consulta.Include(c => c.operacoesOrigem);
            }

            consulta = consulta.AsNoTracking().OrderBy(p => p.id);
            return await consulta.ToArrayAsync();
        }

        public async Task<decimal> ObterValorMoeda(int id)
        {
            IQueryable<Moeda> consulta = this._context.Moeda;
            

            consulta = consulta.AsNoTracking()
                            .OrderBy(m => m.id)
                            .Where(m => m.id == id);
            var moeda = await consulta.FirstOrDefaultAsync();
            return moeda.valor;
        }
    }
}