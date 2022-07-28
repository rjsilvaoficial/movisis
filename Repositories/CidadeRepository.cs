using MovisisCadastro.Context;
using MovisisCadastro.Models;
using System.Threading.Tasks;

namespace MovisisCadastro.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly ApplicationContext _context;
        public CidadeRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Task<Cidade> BuscarCidade(string nome)
        {
            throw new System.NotImplementedException();
        }

        public Task<Cidade> CriarCidade(Cidade cidade)
        {
            throw new System.NotImplementedException();
        }
    }
}
