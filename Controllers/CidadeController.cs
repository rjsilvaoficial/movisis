using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovisisCadastro.Context;
using MovisisCadastro.Models;
using System;
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
        [Route("listartodascidades")]
        public async Task<ActionResult<Cidade>> ListarCidades()
        {
            var cidadeVM = await _context.Cidades.AsNoTracking().ToListAsync();
            if (cidadeVM.Count > 0)
            {
                return Ok(cidadeVM);
            }
            return NotFound("Nenhum resultado encontrado!");

        }

        [HttpGet]
        [Route("buscarpornomesparecidos")]
        public async Task<ActionResult<Cidade>> BuscarCidades([FromQuery] string nome)
        {
            var cidadeVM = await _context.Cidades.Where(cidade => cidade.Nome.Contains(nome)).ToListAsync();
            if (cidadeVM.Count > 0)
            {
                return Ok(cidadeVM);
            }
            return NotFound("Nenhum resultado encontrado!");

        }

        [HttpGet]
        [Route("buscarpornomecompleto")]
        public async Task<ActionResult<Cidade>> BuscarUmaCidade([FromQuery] string nome)
        {
            var cidadeVM = await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Nome == nome);
            if (cidadeVM != null)
            {
                return Ok(cidadeVM);
            }
            return NotFound("Nenhum resultado encontrado!");

        }



        private async Task<Cidade> VerificarSeCidadeExiste(string nome, string uf)
        {
            var cidadeBuscada = await _context.Cidades.FirstOrDefaultAsync(cidade => cidade.Nome == nome && cidade.UF == uf);
            return cidadeBuscada;
        }


        [HttpPost]
        [Route("criar")]
        public async Task<ActionResult<Cidade>> CriarCidade([FromBody] Cidade cidade)
        {
            cidade.Nome = cidade.Nome.ToUpper();
            cidade.UF = cidade.UF.ToUpper();
            try
            {
                var cidadeJaExistente = VerificarSeCidadeExiste(cidade.Nome, cidade.UF);
                if (cidadeJaExistente == null)
                {
                    _context.Cidades.Add(cidade);
                    await _context.SaveChangesAsync();
                    //Http 201
                    return Created("", cidade);
                }

                //Justificativa para 422 https://pt.stackoverflow.com/questions/394699/status-http-para-usu%C3%A1rio-j%C3%A1-cadastrado
                return UnprocessableEntity(cidadeJaExistente);

            }
            catch (DbUpdateException)
            {
                return UnprocessableEntity("Deu ruim");
            }
        }

        [HttpPatch] //Atualiza parcialmente uma entidade
        [Route("renomear")]
        public async Task<ActionResult> RenomearCidade([FromQuery] string nomeAtual, [FromQuery] string nomeNovo)
        {
            try
            {
                var cidadeAtualizada = await _context.Cidades.FirstOrDefaultAsync(cidade => cidade.Nome == nomeAtual);
                if (cidadeAtualizada != null)
                {
                    cidadeAtualizada.Nome = nomeNovo;
                    _context.Cidades.Update(cidadeAtualizada);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            catch (DbUpdateException)
            {
                return UnprocessableEntity("Deu ruim ai patrão");
            }

        }

        [HttpDelete]
        [Route("excluir")]
        public async Task<ActionResult<Cidade>> ExcluirCidade([FromQuery]int id)
        {

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
            catch (DbUpdateException)
            {
                return UnprocessableEntity("Deu ruim ai patrão");
            }
        }

    }
}
