using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Data.Repository.Interfaces
{
    public interface IOperacaoRepository
    {
        Task<IEnumerable<Operacao>> ObterTodos();
        Task<Operacao> ObterPeloId(int id);  
        Task<IEnumerable<Operacao>> ObterPorDataECliente(DateTime dataInicial, DateTime datafinal, int clienteId);
        decimal ObterTotalDasOperacoes();
        decimal ObterTotalDasTaxas();
    }
}