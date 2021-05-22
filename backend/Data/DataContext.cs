using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opcoes) : base(opcoes) {}

        public DbSet<Moeda> Moeda { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Operacao> Operacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Operacao>()
                        .HasOne(m => m.moedaOrigem)
                        .WithMany(m => m.operacoesOrigem)
                        .HasForeignKey(o => o.moedaOrigemId);

            modelBuilder.Entity<Operacao>()
                        .HasOne(m => m.moedaDestino)
                        .WithMany(m => m.operacoesDestino)
                        .HasForeignKey(o => o.moedaDestinoId);
        }
    }
}