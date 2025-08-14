using api.DTOs;
using api.Models;
using MongoDB.Bson;

namespace api.Services
{
    public interface ITarefaService
    {
        IEnumerable<TarefaDTO> ObterTodasAsTarefas();
        TarefaDTO? ObterTarefaPorId(ObjectId id);
        void AdicionarTarefa(TarefaDTO novaTarefa);
        void EditarTarefa(TarefaDTO tarefaAtualizada);
        void ExcluirTarefa(TarefaDTO tarefaParaExcluir);
    }
}
