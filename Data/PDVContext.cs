using Microsoft.EntityFrameworkCore;
using PDVStore.Models;

namespace PDVStore.Data
{
    public class PDVContext : DbContext
    {
        public PDVContext(DbContextOptions<PDVContext> options) : base(options)
        {
        }

        public DbSet<UsuarioCaixa> UsuarioCaixa { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItemVendas { get; set; }
        public DbSet<FormaPagamento> FormaPagamentos { get; set; }
        public DbSet<ItemVenda> ItensVendas { get; set; }
        public DbSet<UsuarioCaixa> Usuarios { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data: use static values to avoid EF Core pending model changes warning
            modelBuilder.Entity<UsuarioCaixa>().HasData(new UsuarioCaixa
            {
                Id = 1,
                Nome = "Admin",
                SenhaHash = "$2a$11$abcdefghijklmnopqrstuv",
                // Use a fixed DateTime value instead of DateTime.Now to keep the model stable
                CreatedAt = new DateTime(2026, 7, 3, 0, 0, 0, DateTimeKind.Utc)
            });
        }
    }
}
