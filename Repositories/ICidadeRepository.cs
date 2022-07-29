using MovisisCadastro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovisisCadastro.Repositories
{
    public interface ICidadeRepository
    {

        Task<List<Cidade>> BuscarTodas();


        Task<List<Cidade>> BuscarSemelhantes(string nome);
    }
}
