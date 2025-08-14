using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class MongoDbContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }

        public MongoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tarefa>();
        }

    }
}
