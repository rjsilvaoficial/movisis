using System.ComponentModel.DataAnnotations;

namespace MovisisCadastro.ViewModels
{
    public class CidadeInputViewModel
    {
        [Required(ErrorMessage = "Você precisa preencher o Nome!")]
        [RegularExpression(@"^[a-zA-Z'\s]{3,60}$", ErrorMessage = "Nome deve conter apenas letras e espaço (se houver sobrenome)!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Nome recebe entre 3 e 60 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Você precisa preencher a UF com duas letras!")]
        [RegularExpression(@"^[a-zA-Z]{2}$", ErrorMessage = "Você precisa preencher a UF com duas letras!")]
        public string UF { get; set; }
    }
}
