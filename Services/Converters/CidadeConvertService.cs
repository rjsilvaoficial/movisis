using MovisisCadastro.Models;
using MovisisCadastro.ViewModels;

namespace MovisisCadastro.Services.Converters
{
    public static class CidadeConvertService
    {
        public static Cidade CriarCidadeDaCidadeInput(CidadeInputViewModel cidadeInput)
        {
            Cidade cidade = new Cidade
            {
                Nome = cidadeInput.Nome.ToUpper(),
                UF = cidadeInput.UF.ToUpper(),
            };

            return cidade;


        }
    }
}
