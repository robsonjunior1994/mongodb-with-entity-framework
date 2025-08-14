using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class TarefaDTO
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Uma tarefa deve ser fornecida")]
        [Display(Name = "Tarefa")]
        public string? Texto { get; set; }
        public bool Concluida { get; set; } = false;
        public TarefaDTO() { }
        public TarefaDTO(string? id, string? texto, bool concluida)
        {
            Id = id;
            Texto = texto;
            Concluida = concluida;
        }
    }
}
