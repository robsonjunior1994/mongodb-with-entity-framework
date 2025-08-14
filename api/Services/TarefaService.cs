using api.DTOs;
using api.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace api.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly MongoDbContext _context;
        public TarefaService(MongoDbContext context)
        {
            _context = context;
        }
        public void AdicionarTarefa(TarefaDTO novaTarefa)
        {
            var tarefa = new Tarefa
            {
                Texto = novaTarefa.Texto
            };
            _context.Tarefas.Add(tarefa);

            _context.ChangeTracker.DetectChanges();
            //Aqui poderiamos usar um sistema de log para registrar as mudanças
            Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
            
            _context.SaveChanges();
        }

        public void ExcluirTarefa(TarefaDTO tarefaExcluir)
        {
            var tarefaParaExcluir = _context.Tarefas.Where(c => c.Id == new ObjectId(tarefaExcluir.Id)).FirstOrDefault();
            if (tarefaParaExcluir != null)
            {
                _context.Tarefas.Remove(tarefaParaExcluir);
                _context.ChangeTracker.DetectChanges();
                //Aqui poderiamos usar um sistema de log para registrar as mudanças
                Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("A tarefa para atualizar não foi encontrada.");
            }
        }

        public void EditarTarefa(TarefaDTO tarefaAtualizada)
        {
            var tarefaParaAtualizar = _context.Tarefas.FirstOrDefault(c => c.Id == new ObjectId(tarefaAtualizada.Id));
            if (tarefaParaAtualizar != null)
            {
                tarefaParaAtualizar.Texto = tarefaAtualizada.Texto;
                tarefaParaAtualizar.Concluida = tarefaAtualizada.Concluida;
                _context.Tarefas.Update(tarefaParaAtualizar);
                _context.ChangeTracker.DetectChanges();
                //Aqui poderiamos usar um sistema de log para registrar as mudanças
                Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
                _context.SaveChanges();

            }
            else
            {
                throw new ArgumentException("A tarefa para atualizar não foi encontrada.");
            }
        }

        public IEnumerable<TarefaDTO> ObterTodasAsTarefas()
        {
            List<TarefaDTO> tarefasDTO = new List<TarefaDTO>();
            var tarefas = _context.Tarefas.OrderBy(c => c.Id).AsNoTracking().AsEnumerable<Tarefa>();
            foreach (var tarefa in tarefas)
            {
                tarefasDTO.Add(new TarefaDTO(tarefa.Id.ToString(), tarefa.Texto, tarefa.Concluida));
            }
            return tarefasDTO;
        }

        public TarefaDTO? ObterTarefaPorId(ObjectId id)
        {
            var tarefa = _context.Tarefas.FirstOrDefault(c => c.Id == id);
            if (tarefa != null)
            {
                return new TarefaDTO(tarefa.Id.ToString(), tarefa.Texto, tarefa.Concluida);
            }
            return null;
        }
    }
}
