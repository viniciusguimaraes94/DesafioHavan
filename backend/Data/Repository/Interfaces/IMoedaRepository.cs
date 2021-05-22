using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Data.Repository.Interfaces
{
    public interface IMoedaRepository
    {
         Task<IEnumerable<Moeda>> ObterTodos(bool incluirOperacao);

        Task<Moeda> ObterPeloId(int id, bool incluirOperacao);

        Task<decimal> ObterValorMoeda(int id);   
    }
}