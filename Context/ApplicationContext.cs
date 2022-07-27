using Microsoft.EntityFrameworkCore;
using MovisisCadastro.Models;

namespace MovisisCadastro.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        DbSet<Cliente> Clientes { get; set; }
        DbSet<Cidade> Cidades { get; set; }

    }
}
