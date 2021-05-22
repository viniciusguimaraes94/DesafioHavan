using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    public interface ICalculoServico
    {
        decimal ObterValorCalculadoTaxa(decimal valorCalculado);
        Task<decimal> ObterValorCalculadoPorMoedaOrigemEDestino(int moedaOrigemId, int moedaDestinoId, decimal valorEntrada);
    }
}