using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Repository.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repository.Services
{
    public class OperacaoRepository : IOperacaoRepository
    {
        private readonly DataContext _context;

        public OperacaoRepository(DataContext context)
        {
            this._context = context; 
        }

        public async Task<Operacao> ObterPeloId(int id)
        {
            IQueryable<Operacao> consulta = this._context.Operacao;
            consulta = consulta.Include(o => o.cliente);
            consulta = consulta.Include(o => o.moedaOrigem);
            consulta = consulta.Include(o => o.moedaDestino);

            consulta = consulta.AsNoTracking()
                            .OrderBy(p => p.id)
                            .Where(p => p.id == id);

            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Operacao>> ObterPorDataECliente(DateTime dataInicial, DateTime datafinal, int clienteId)
        {
            IQueryable<Operacao> consulta = this._context.Operacao;
            consulta = consulta.Include(o => o.cliente);
            consulta = consulta.Include(o => o.moedaOrigem);
            consulta = consulta.Include(o => o.moedaDestino);

            consulta = consulta.AsNoTracking()
                            .OrderBy(o => o.id)
                            .Where(o => o.clienteId == clienteId && (dataInicial <= o.dataOperacao && datafinal >= o.dataOperacao));

            return await consulta.ToArrayAsync();
        }

        public async Task<IEnumerable<Operacao>> ObterTodos()
        {
            IQueryable<Operacao> consulta = this._context.Operacao;
            consulta = consulta.Include(o => o.cliente);
            consulta = consulta.Include(o => o.moedaOrigem);
            consulta = consulta.Include(o => o.moedaDestino);
            consulta = consulta.AsNoTracking().OrderBy(p => p.id);
            return await consulta.ToArrayAsync();
        }

        public decimal ObterTotalDasOperacoes()
        {
            IQueryable<Operacao> consulta = this._context.Operacao;
            
            decimal valorTotalConvertido = consulta.AsNoTracking().Sum(o => o.valorConvertido);

            return valorTotalConvertido;
        }

        public decimal ObterTotalDasTaxas()
        {
            IQueryable<Operacao> consulta = this._context.Operacao;
            
            decimal valorTotalTaxas = consulta.AsNoTracking().Sum(o => o.taxa);

            return valorTotalTaxas;
        }
    }
}