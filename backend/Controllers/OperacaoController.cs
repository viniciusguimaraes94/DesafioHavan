using System;
using System.Threading.Tasks;
using backend.Data.Repository.Interfaces;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperacaoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IOperacaoRepository _operacaoRepository;
        private readonly ICalculoServico _calculoServico;

        public OperacaoController(IRepository repository, 
                                  IOperacaoRepository operacaoRepository,
                                  ICalculoServico calculoServico)
        {
            this._repository = repository;
            this._operacaoRepository = operacaoRepository;
            this._calculoServico = calculoServico;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var result = await _operacaoRepository.ObterTodos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todas as operações, ocorreu o erro: {ex.Message}");
            }
        }
        
        [HttpGet("{dataInicio}/{dataFinal}/{clienteId}")]
        public async Task<IActionResult> ObterPeloClienteEPeriodo(DateTime dataInicio, DateTime dataFinal, int clienteId)
        {
            try
            {
                var result = await _operacaoRepository.ObterPorDataECliente(dataInicio, dataFinal, clienteId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todas as operações, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Operacao operacaoModelo)
        {
            try
            {
                Operacao operacao = await ObterOperacaoComValoresCalculados(operacaoModelo);

                this._repository.Add(operacao);

                if(!await this._repository.SaveChanges())
                {
                    return BadRequest("Não foi possivel salvar a operação!");
                }

                return Ok(operacao);
            }
            catch( Exception ex)
            {
                return BadRequest($"Ao incluir a operação, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpPut("{operacaoId}")]
        public async Task<IActionResult> Editar(int operacaoId, Operacao operacaoModelo)
        {
            try
            {
                var operacao = await _operacaoRepository.ObterPeloId(operacaoId);

                if (operacao == null)
                {
                    return NotFound("Operação não localizado!");
                }

                _repository.Update(operacaoModelo);

                if (!await _repository.SaveChanges())
                {
                    return BadRequest("Não foi possível editar a operaçao!");
                }
                return Ok(operacaoModelo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao editar a operação, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpDelete("{operacaoId}")]
        public async Task<IActionResult> Remover(int operacaoId)
        {
            try
            {
                var operacao = await _operacaoRepository.ObterPeloId(operacaoId);

                if (operacao == null)
                {
                    return NotFound("Operação não localizado!");
                }

                _repository.Delete(operacao);

                if (!await _repository.SaveChanges())
                {
                    return BadRequest("Não foi possível remover a operação!");
                }
                return Ok("Operação removida com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao remover a operação ocorreu o erro: {ex}");
            }
        }

        private async Task<Operacao> ObterOperacaoComValoresCalculados(Operacao operacaoModelo) {
            decimal valorCalculado = 
              await _calculoServico.ObterValorCalculadoPorMoedaOrigemEDestino(
                  operacaoModelo.moedaOrigemId,
                  operacaoModelo.moedaDestinoId,
                  operacaoModelo.valorOriginal
                );

            decimal valorTaxa = _calculoServico.ObterValorCalculadoTaxa(valorCalculado);

            return new Operacao() {
              clienteId = operacaoModelo.clienteId,
              dataOperacao = operacaoModelo.dataOperacao,
              moedaDestinoId = operacaoModelo.moedaDestinoId,
              moedaOrigemId = operacaoModelo.moedaOrigemId,
              valorOriginal = operacaoModelo.valorOriginal,
              valorConvertido = valorCalculado,
              taxa = valorTaxa
            };
        }
    }
}