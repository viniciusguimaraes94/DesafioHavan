using System;
using System.Threading.Tasks;
using backend.Data.Repository.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoedaController : ControllerBase
    {
        public readonly IMoedaRepository _moedaRepository;
        private readonly IRepository _repository;
        
        public MoedaController(IRepository repository, IMoedaRepository moedaRepository)
        {
            this._repository = repository;
            this._moedaRepository = moedaRepository;            
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var result = await _moedaRepository.ObterTodos(incluirOperacao: true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todas as moedas, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpGet("{moedaId}")]
        public async Task<IActionResult> ObterPeloId(int moedaId)
        {
            try
            {
                var result = await _moedaRepository.ObterPeloId(moedaId, incluirOperacao: true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter a moeda pelo Id, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SalvarPeloId(Moeda moedaModelo)
        {
            try
            {
                this._repository.Add(moedaModelo);

                if(!await this._repository.SaveChanges())
                {
                    return BadRequest("Não foi possivel salvar a moeda!");
                }

                return Ok(moedaModelo);
            }
            catch( Exception ex)
            {
                return BadRequest($"Ao incluir o moeda, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpPut("{clientId}")]
        public async Task<IActionResult> Editar(int clientId, Moeda moedaModelo)
        {
            try
            {
                var moeda = await _moedaRepository.ObterPeloId(clientId, false);

                if (moeda == null)
                {
                    return NotFound("Moeda não localizada!");
                }

                _repository.Update(moedaModelo);

                if (!await _repository.SaveChanges())
                {
                    return BadRequest("Não foi possível editar a moeda!");
                }
                return Ok(moedaModelo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao editar o moeda, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpDelete("{moedaId}")]
        public async Task<IActionResult> Remover(int moedaId)
        {
            try
            {
                var moeda = await _moedaRepository.ObterPeloId(moedaId, false);

                if (moeda == null)
                {
                    return NotFound("Moeda não localizada!");
                }

                _repository.Delete(moeda);

                if (!await _repository.SaveChanges())
                {
                    return BadRequest("Não foi possível remover a moeda!");
                }
                return Ok("Moeda removida com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao remover a moeda ocorreu o erro: {ex}");
            }
        }
    }
}