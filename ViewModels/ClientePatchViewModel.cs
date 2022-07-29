using System.ComponentModel.DataAnnotations;

namespace MovisisCadastro.ViewModels
{
    public class ClientePatchViewModel
    {
        [Required(ErrorMessage = "Você precisa preencher o nome")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Nome recebe entre 3 e 60 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Você precisa incluir o telefone!")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Telefone deve ter entre 10 e 13 números")]
        public string Telefone { get; set; }
    }
}
