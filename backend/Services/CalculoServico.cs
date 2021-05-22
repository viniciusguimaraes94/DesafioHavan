using System;
using System.Threading.Tasks;
using backend.Data.Repository.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class CalculoServico : ICalculoServico
    {
        private readonly IMoedaRepository _moedaRepository;

        public CalculoServico(IMoedaRepository moedaRepository)
        {
            this._moedaRepository = moedaRepository;

        }
        public async Task<decimal> ObterValorCalculadoPorMoedaOrigemEDestino(int moedaOrigemId, int moedaDestinoId, decimal valorEntrada)
        {
            decimal valorMoedaOrigem = await _moedaRepository.ObterValorMoeda(moedaOrigemId);
            decimal valorMoedaDestino = await _moedaRepository.ObterValorMoeda(moedaDestinoId);

            decimal valorCalculado = (valorMoedaOrigem * valorMoedaDestino) * valorEntrada;

            return Math.Round(valorCalculado, 2);
        }

        public decimal ObterValorCalculadoTaxa(decimal valorCalculado)
        {
            decimal valor = (valorCalculado * 10) / 100;
            return Math.Round(valor);
        }
    }
}