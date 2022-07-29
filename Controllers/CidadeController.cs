using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovisisCadastro.Context;
using MovisisCadastro.Models;
using MovisisCadastro.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MovisisCadastro.Controllers
{
    [Route("api/v1/cidade")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public CidadeController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("buscartodas")] //Busca todas as cidades
        public async Task<ActionResult> BuscarTodas()
        {
            try
            {

                var todasCidades = await _context.Cidades.AsNoTracking().ToListAsync();

                if (todasCidades.Count > 0)
                {
                    var respostaOkVM = new RespostaCidadeViewModel(true, "O campo Data apresentará a lista com todas as cidades cadastradas!");
                    respostaOkVM.Data.AddRange(todasCidades);
                    return Ok(respostaOkVM);
                }
                
                return NotFound(new RespostaCidadeViewModel(false, "Nenhum resultado encontrado!"));
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaCidadeViewModel(false, "Servidor indisponível no momento!"));
            }

        }


        [HttpGet]
        [Route("buscarsemelhantes")] //Busca todas as cidades dentro de um critério
        public async Task<ActionResult> BuscarSemelhantes([FromQuery] string nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModel(false, "Erro nos valores preenchidos, a lista deles está em Data", ModelState.SelectMany(itens => itens.Value.Errors)
                    .Select(erro => erro.ErrorMessage)));
            }

            try
            {
                var cidadeVM = await _context.Cidades.Where(cidade => cidade.Nome.Contains(nome.ToUpper())).ToListAsync();

                if (cidadeVM.Count > 0)
                {
                    var respostaOkVM = new RespostaCidadeViewModel(true, $"O campo Data apresentará a lista com todas as cidades cadastradas com {nome.ToUpper()} no nome!");
                    respostaOkVM.Data.AddRange(cidadeVM);
                    return Ok(respostaOkVM);
                }
                
                return NotFound(new RespostaCidadeViewModel(false, "Nenhum resultado encontrado!"));
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaCidadeViewModel(false, "Servidor indisponível no momento!"));
            }


        }


        [HttpGet]
        [Route("buscaruma")] //Busca um resultado específico
        public async Task<ActionResult> BuscarUma([FromQuery] string nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModel(false, "Erro nos valores preenchidos, a lista deles está em Data", ModelState.SelectMany(itens => itens.Value.Errors)
                    .Select(erro => erro.ErrorMessage)));
            }


            try
            {
                var cidadeVM = await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Nome == nome.ToUpper());
                if (cidadeVM != null)
                {
                    var respostaOkVM = new RespostaCidadeViewModel(true, $"O campo Data apresentará a de {nome.ToUpper()}!");
                    respostaOkVM.Data.Add(cidadeVM);
                    return Ok(respostaOkVM);
                }
                return NotFound(new RespostaCidadeViewModel(false, "Nenhum resultado encontrado!"));
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaCidadeViewModel(false, "Servidor indisponível no momento!"));
            }
        }


        private async Task<Cidade> VerificarExistencia(string nome, string uf)
        {
            var cidadeBuscada = await _context.Cidades.FirstOrDefaultAsync(cidade => cidade.Nome == nome && cidade.UF == uf);
            return cidadeBuscada;
        }



        [HttpPost]
        [Route("criar")]
        public async Task<ActionResult> CriarCidade([FromBody] Cidade cidade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModel(false, "Erro nos valores preenchidos, a lista deles está em Data", ModelState.SelectMany(itens => itens.Value.Errors)
                    .Select(erro => erro.ErrorMessage)));
            }

            cidade.Nome = cidade.Nome.ToUpper();
            cidade.UF = cidade.UF.ToUpper();

            try
            {
                var cidadeJaExistente = await VerificarExistencia(cidade.Nome, cidade.UF);
                if (cidadeJaExistente == null)
                {
                    _context.Cidades.Add(cidade);
                    await _context.SaveChangesAsync();
                    var respostaOkVM = new RespostaCidadeViewModel(true, $"A cidade de {cidade.Nome.ToUpper()} foi cadastrada!");
                    respostaOkVM.Data.Add(cidade);
                    //Http 201
                    return Created("", respostaOkVM);
                }
                var respostaNOKVM = new RespostaCidadeViewModel(false, "Esta cidade já está cadastrada!");
                respostaNOKVM.Data.Add(cidadeJaExistente);

                //Justificativa para 422 https://pt.stackoverflow.com/questions/394699/status-http-para-usu%C3%A1rio-j%C3%A1-cadastrado
                return UnprocessableEntity(respostaNOKVM);

            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaCidadeViewModel(false, "Servidor indisponível no momento!"));
            }
        }


        [HttpPatch] //Atualiza parcialmente uma entidade
        [Route("renomear")]
        public async Task<ActionResult> RenomearCidade([FromQuery] string nomeAtual, [FromQuery] string nomeNovo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModel(false, "Erro nos valores preenchidos, a lista deles está em Data", ModelState.SelectMany(itens => itens.Value.Errors)
                    .Select(erro => erro.ErrorMessage)));
            }

            try
            {
                var cidadeAtualizada = await _context.Cidades.FirstOrDefaultAsync(cidade => cidade.Nome == nomeAtual.ToUpper());
                
                if (cidadeAtualizada != null)
                {
                    cidadeAtualizada.Nome = nomeNovo.ToUpper();
                    _context.Cidades.Update(cidadeAtualizada);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                
                return NotFound();
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaCidadeViewModel(false, "Servidor indisponível no momento!"));
            }

        }


        [HttpDelete]
        [Route("excluir")]
        public async Task<ActionResult<Cidade>> ExcluirCidade([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModel(false, "Erro nos valores preenchidos, a lista deles está em Data", ModelState.SelectMany(itens => itens.Value.Errors)
                    .Select(erro => erro.ErrorMessage)));
            }

            try
            {
                var cidadeAtualizada = await _context.Cidades.FirstOrDefaultAsync(cidade => cidade.CidadeId == id);
                if (cidadeAtualizada != null)
                {
                    _context.Cidades.Remove(cidadeAtualizada);
                    await _context.SaveChangesAsync();
                    return Ok(cidadeAtualizada);
                }
                return NotFound();
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaCidadeViewModel(false, "Servidor indisponível no momento!"));
            }
        }

    }
}
