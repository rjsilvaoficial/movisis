using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovisisCadastro.Context;
using MovisisCadastro.Models;
using System.Collections.Generic;
using System.Linq;
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



        public async Task<List<Cidade>> BuscarTodas()
        {
            try
            {
                return await _context.Cidades.AsNoTracking().ToListAsync();
            }
            catch (SqlException ex)
            {
                throw ex;
            }


        }   
        
        public async Task<List<Cidade>> BuscarSemelhantes(string nome)
        {
            try
            {
                return await _context.Cidades.Where(cidade => cidade.Nome.Contains(nome.ToUpper())).ToListAsync();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}

