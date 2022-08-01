using MovisisCadastro.Models;
using MovisisCadastro.ViewModels;

namespace MovisisCadastro.Services.Converters
{
    public static class ClientConvertService
    {
        public static Cliente CriarClienteDoClienteInput(ClienteInputViewModel clienteInput)
        {
            Cliente cliente = new Cliente
            {
                Nome = clienteInput.Nome.ToUpper(),
                Apelido = clienteInput.Apelido.ToUpper(),
                Telefone = clienteInput.Telefone,
                CidadeId = clienteInput.CidadeId,
                DataNascimento = clienteInput.DataNascimento
            };

            return cliente;


        }
    }
}
