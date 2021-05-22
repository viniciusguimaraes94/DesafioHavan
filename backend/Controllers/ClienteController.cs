using System;
using System.Threading.Tasks;
using backend.Data.Repository.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        public readonly IClienteRepository _clienteRepository;
        private readonly IRepository _repository;

        public ClienteController(IRepository repository, IClienteRepository clienteRepository)
        {
            this._repository = repository;
            this._clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var result = await _clienteRepository.ObterTodos(incluirOperacao: true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter todos os clientes, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpGet("{clienteId}")]
        public async Task<IActionResult> ObterPeloId(int clienteId)
        {
            try
            {
                var result = await _clienteRepository.ObterPeloId(clienteId, incluirOperacao: true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao obter o cliente pelo Id, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SalvarPeloId(Cliente clienteModelo)
        {
            try
            {
                this._repository.Add(clienteModelo);

                if(!await this._repository.SaveChanges())
                {
                    return BadRequest("Não foi possivel salvar o cliente!");
                }

                return Ok(clienteModelo);
            }
            catch( Exception ex)
            {
                return BadRequest($"Ao incluir o cliente, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpPut("{clientId}")]
        public async Task<IActionResult> Editar(int clientId, Moeda clienteModelo)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPeloId(clientId, false);

                if (cliente == null)
                {
                    return NotFound("Cliente não localizado!");
                }

                _repository.Update(clienteModelo);

                if (!await _repository.SaveChanges())
                {
                    return BadRequest("Não foi possível editar o cliente!");
                }
                return Ok(clienteModelo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao editar o cliente, ocorreu o erro: {ex.Message}");
            }
        }

        [HttpDelete("{clientId}")]
        public async Task<IActionResult> Remover(int clientId)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPeloId(clientId, false);

                if (cliente == null)
                {
                    return NotFound("Cliente não localizado!");
                }

                _repository.Delete(cliente);

                if (!await _repository.SaveChanges())
                {
                    return BadRequest("Não foi possível remover o cliente!");
                }
                return Ok("Cliente removido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ao remover o cliente ocorreu o erro: {ex}");
            }
        }

    }

}