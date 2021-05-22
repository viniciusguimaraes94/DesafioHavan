using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Data.Repository.Interfaces
{
    public interface IClienteRepository
    {
         Task<IEnumerable<Cliente>> ObterTodos(bool incluirOperacao);

        Task<Cliente> ObterPeloId(int id, bool incluirOperacao);      
    
    }
}