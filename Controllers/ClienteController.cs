using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovisisCadastro.Context;
using MovisisCadastro.Models;
using MovisisCadastro.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovisisCadastro.Controllers
{
    [Route("api/v1/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public ClienteController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("buscartodos")] //Busca todas as cidades
        public async Task<ActionResult> BuscarTodos()
        {
            try
            {

                var todosClientes = await _context.Clientes.AsNoTracking().ToListAsync();

                if (todosClientes.Count > 0)
                {
                    var respostaOkVM = new RespostaClienteViewModel(true, "O campo Data apresentará a lista com todos os clientes cadastrados!");
                    respostaOkVM.Data.AddRange(todosClientes);
                    return Ok(respostaOkVM);
                }

                return NotFound(new RespostaClienteViewModel(false, "Nenhum resultado encontrado!"));
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaClienteViewModel(false, "Servidor indisponível no momento!"));
            }

        }


        [HttpGet]
        [Route("buscarsemelhantes")] //Busca todos os clientes dentro de um critério
        public async Task<ActionResult> BuscarSemelhantes([FromQuery] string nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModel(false, "Erro nos valores preenchidos, mais detalhes em Data!", ModelState.SelectMany(itens => itens.Value.Errors)
                    .Select(erro => erro.ErrorMessage)));
            }

            try
            {
                var clienteVM = await _context.Clientes.Where(cliente => cliente.Nome.Contains(nome.ToUpper())).ToListAsync();

                if (clienteVM.Count > 0)
                {
                    var respostaOkVM = new RespostaClienteViewModel(true, $"Encontrado um ou mais clientes com {nome.ToUpper()} no nome, mais detalhes em Data!");
                    respostaOkVM.Data.AddRange(clienteVM);
                    return Ok(respostaOkVM);
                }

                return NotFound(new RespostaClienteViewModel(false, "Nenhum resultado encontrado!"));
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaClienteViewModel(false, "Servidor indisponível no momento!"));
            }
        }


        [HttpGet]
        [Route("buscarum")] //Busca um resultado específico
        public async Task<ActionResult> BuscarUm([FromQuery] string nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModel(false, "Erro nos valores preenchidos, mais detalhes em Data!", ModelState.SelectMany(itens => itens.Value.Errors)
                    .Select(erro => erro.ErrorMessage)));
            }


            try
            {
                var clienteVM = await _context.Clientes.FirstOrDefaultAsync(cidade => cidade.Nome == nome.ToUpper());
                if (clienteVM != null)
                {
                    var respostaOkVM = new RespostaClienteViewModel(true, $"Cliente {nome.ToUpper()} encontrado, mais detalhes em Data!");
                    respostaOkVM.Data.Add(clienteVM);
                    return Ok(respostaOkVM);
                }
                return NotFound(new RespostaClienteViewModel(false, "Nenhum resultado encontrado!"));
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaClienteViewModel(false, "Servidor indisponível no momento!"));
            }
        }


        private async Task<Cliente> VerificarExistencia(string nome, string apelido, int cidadeId, string telefone, DateTime dataNascimento)
        {
            var clienteBuscado = await _context.Clientes
                .FirstOrDefaultAsync(cliente => cliente.Nome == nome && cliente.Apelido == apelido
                && cliente.CidadeId == cidadeId && cliente.Telefone == telefone
                && cliente.DataNascimento == dataNascimento);
            return clienteBuscado;
        }



        [HttpPost]
        [Route("criar")]
        public async Task<ActionResult> CriarCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModel(false, "Erro nos valores preenchidos, mais detalhes em Data", ModelState.SelectMany(itens => itens.Value.Errors)
                    .Select(erro => erro.ErrorMessage)));
            }
            try
            {
                var clienteExistente = await VerificarExistencia(cliente.Nome, cliente.Apelido, cliente.CidadeId, cliente.Telefone, cliente.DataNascimento);
                if (clienteExistente != null)
                {
                    var respostaNOKVM = new RespostaClienteViewModel(false, "Este cliente já está cadastrado, mais detalhes em Data!");
                    respostaNOKVM.Data.Add(clienteExistente);

                    //Justificativa para 422 https://pt.stackoverflow.com/questions/394699/status-http-para-usu%C3%A1rio-j%C3%A1-cadastrado

                    return UnprocessableEntity(respostaNOKVM);
                }

                cliente.Nome = cliente.Nome.ToUpper();
                cliente.Apelido = cliente.Apelido.ToUpper();
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                var respostaOKVM = new RespostaClienteViewModel(true, $"Cliente {cliente.Nome.ToUpper()} cadastrado com sucesso, mais detalhes em Data!");
                respostaOKVM.Data.Add(cliente);
                return Created("",respostaOKVM);
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaClienteViewModel(false, "Servidor indisponível no momento!"));
            }
        }


        [HttpPatch] //Atualiza parcialmente uma entidade
        [Route("atualizar")]
        public async Task<ActionResult> AtualizarCliente([FromQuery] string nome, [FromBody] ClientePatchViewModel clienteAtualizado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModel(false, "Erro nos valores preenchidos, mais detalhes em Data", ModelState.SelectMany(itens => itens.Value.Errors)
                    .Select(erro => erro.ErrorMessage)));
            }

            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Nome == nome.ToUpper());

                if (cliente != null)
                {
                    if(clienteAtualizado.Nome != cliente.Nome)
                    {
                        cliente.Nome = clienteAtualizado.Nome.ToUpper();
                    }
                    if (clienteAtualizado.Telefone != cliente.Telefone)
                    {
                        cliente.Telefone = clienteAtualizado.Telefone;
                    }

                    _context.Clientes.Update(cliente);
                    await _context.SaveChangesAsync();
                    var respostaOkVM = new RespostaClienteViewModel(true, $"Cliente {cliente.Nome.ToUpper()} atualizado com sucesso, mais detalhes em Data!");
                    respostaOkVM.Data.Add(cliente);

                    return Ok(respostaOkVM);
                }

                return NotFound(new RespostaClienteViewModel(false, "Nenhum resultado encontrado!"));
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaClienteViewModel(false, "Servidor indisponível no momento!"));
            }

        }


        [HttpDelete]
        [Route("excluir")]
        public async Task<ActionResult> ExcluirCidade([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModel(false, "Erro nos valores preenchidos, mais detalhes em Data", ModelState.SelectMany(itens => itens.Value.Errors)
                    .Select(erro => "O valor id aceita valores numéricos de 0 à 2147483647"))); //Passível de substituição por string pura
            }

            try
            {
                var clienteAtualizado = await _context.Clientes.FirstOrDefaultAsync(cliente => cliente.ClienteId == id);
                if (clienteAtualizado != null)
                {
                    _context.Clientes.Remove(clienteAtualizado);
                    await _context.SaveChangesAsync();
                    clienteAtualizado.CidadeId = id;
                    var respostaOkVM = new RespostaClienteViewModel(true, $"Cliente {clienteAtualizado.Nome.ToUpper()} excluído com sucesso, mais detalhes em Data!");
                    respostaOkVM.Data.Add(clienteAtualizado);

                    return Ok(respostaOkVM);
                }
                return NotFound(new RespostaClienteViewModel(false, "Nenhum resultado encontrado!"));
            }
            catch (SqlException)
            {
                return StatusCode(500, new RespostaClienteViewModel(false, "Servidor indisponível no momento!"));
            }
        }

    }
}


