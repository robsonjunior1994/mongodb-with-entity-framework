using api.DTOs;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;
        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }
        // GET: api/<TarefaController>
        [HttpGet]
        public IActionResult Get()
        {
            TarefaDTO? tarefa = new();
            var tarefas = _tarefaService.ObterTodasAsTarefas();
            return Ok(new ResponseDTO { sucesso = true, dados = tarefas, mensagem= "Tarefa encontrada com sucesso."});
        }

        // GET api/<TarefaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                
                return BadRequest(new ResponseDTO { sucesso = false, mensagem = $"ID inválido. O ID deve ser um ObjectId hexadecimal de 24 caracteres." });
            }

            var tarefa = _tarefaService.ObterTarefaPorId(objectId);
            if (tarefa != null)
            {
                return Ok(new ResponseDTO { sucesso = true, dados = tarefa, mensagem = "" });
            }
            
            return NotFound(new ResponseDTO { sucesso = false, mensagem = $"Tarefa com ID {id} não encontrada." });
        }

        // POST api/<TarefaController>
        [HttpPost]
        public IActionResult Post([FromBody] TarefaDTO novaTarefa)
        {
            if (ModelState.IsValid)
            {
                _tarefaService.AdicionarTarefa(novaTarefa);
                return Ok(new ResponseDTO { sucesso = true, dados = novaTarefa, mensagem = "Tarefa adicionada com sucesso!" });
            }
            return BadRequest(new ResponseDTO { sucesso = false });
        }

        // PUT api/<TarefaController>
        [HttpPut]
        public IActionResult Put([FromBody] TarefaDTO tarefaAtualizada)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!ObjectId.TryParse(tarefaAtualizada.Id, out ObjectId objectId))
                    {
                        
                        return BadRequest(new ResponseDTO { sucesso = false, mensagem = $"ID inválido. O ID deve ser um ObjectId hexadecimal de 24 caracteres." });
                    }

                    TarefaDTO? tarefa = _tarefaService.ObterTarefaPorId(objectId);
                    if (tarefa == null)
                    {
                        
                        return NotFound(new ResponseDTO { sucesso = false, mensagem = $"Tarefa com ID {tarefaAtualizada.Id} não encontrada." });
                    }

                    tarefa.Texto = tarefaAtualizada.Texto;
                    tarefa.Concluida = tarefaAtualizada.Concluida;

                    _tarefaService.EditarTarefa(tarefa);
                    
                    return Ok(new ResponseDTO { sucesso = true, dados = tarefa, mensagem = "Tarefa atualizada com sucesso!" });
                }
                else
                {
                    return BadRequest(new ResponseDTO { sucesso = false });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new ResponseDTO { sucesso = false, mensagem = $"Erro ao atualizar a tarefa: {ex.Message}" }));
            }
        }

        // DELETE api/<TarefaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest(new ResponseDTO { sucesso = false, mensagem = "ID inválido. O ID deve ser um ObjectId hexadecimal de 24 caracteres." });
            }

            try
            {
                TarefaDTO? tarefa = _tarefaService.ObterTarefaPorId(objectId);
                if (tarefa == null)
                {
                    return NotFound(new ResponseDTO { sucesso = false, mensagem = $"Tarefa com ID {id} não encontrada." });
                }
                _tarefaService.ExcluirTarefa(tarefa);

                return Ok(new ResponseDTO { sucesso = true, dados = tarefa, mensagem = "Tarefa excluída com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, (new ResponseDTO { sucesso = false, mensagem = $"Erro ao excluir a tarefa: {ex.Message}" }));
            }
        }
    }
}
