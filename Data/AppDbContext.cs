using Microsoft.EntityFrameworkCore;
using WorkerContrRendaFixa.Models;

namespace WorkerContrRendaFixa.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contratacao> Contratacoes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
    }
}
