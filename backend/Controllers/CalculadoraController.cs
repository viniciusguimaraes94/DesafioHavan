using System.Threading.Tasks;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculadoraController : ControllerBase
    {
        private readonly ICalculoServico _calculoServico;
        public CalculadoraController(ICalculoServico calculoServico)
        {
            this._calculoServico = calculoServico;
        }

        [HttpPost]
        public async Task<IActionResult> CalcularConversao(ConversaoMoeda conversaoModelo)
        {
            decimal valorCalculado = 
               await _calculoServico.ObterValorCalculadoPorMoedaOrigemEDestino(
                   conversaoModelo.moedaOrigemId,
                   conversaoModelo.moedaDestinoId,
                   conversaoModelo.valor
            );

            return Ok(valorCalculado);
        }
    }
}