using System;
using System.ComponentModel.DataAnnotations;

namespace MovisisCadastro.ViewModels
{
    public class ClienteInputViewModel
    {
        [Required(ErrorMessage = "Você precisa preencher o nome")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Nome recebe entre 3 e 60 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Você precisa incluir o telefone!")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Telefone deve ter entre 10 e 13 números")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Você precisa atribuir um ID a cidade!")]
        [Range(1,int.MaxValue,ErrorMessage ="")]
        public int CidadeId { get; set; }

        //public Cidade Cidade { get; set; }

        [Required(ErrorMessage = "Você precisa preencher o apelido!")]
        public string Apelido { get; set; }

        [Required(ErrorMessage = "Você precisa preencher a data de nascimento!")]
        public DateTime DataNascimento { get; set; }
    }
}
