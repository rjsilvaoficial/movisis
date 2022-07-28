using MovisisCadastro.Models;
using System.Threading.Tasks;

namespace MovisisCadastro.Repositories
{
    public interface ICidadeRepository
    {

        Task<Cidade> BuscarCidade(string nome);


        Task<Cidade> CriarCidade(Cidade cidade);
    }
}
