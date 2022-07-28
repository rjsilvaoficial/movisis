using Microsoft.EntityFrameworkCore;
using MovisisCadastro.Models;

namespace MovisisCadastro.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cidade> Cidades { get; set; }

    }
}
