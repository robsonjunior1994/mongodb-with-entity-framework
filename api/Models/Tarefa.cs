using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Tarefa
    {
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "Uma tarefa deve ser fornecida")]
        [Display(Name = "Tarefa")]
        public required string Texto { get; set; }
        public bool Concluida { get; set; } = false;

    }
}
