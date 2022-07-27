using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovisisCadastro.Models
{
    [Table("CLIENTE")]
    public class Cliente
    {
        [Column("ID")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Você precisa preencher o nome")]
        [Column("NOME")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Nome recebe entre 3 e 60 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Você precisa incluir o telefone!")]
        [Column("TELEFONE")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Telefone deve ter entre 10 e 13 números")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Você precisa atribuir um ID a cidade!")]
        [Column("ID_CIDADE")]
        public int CidadeId { get; set; }


        public Cidade Cidade { get; set; }

        [Required(ErrorMessage = "Você precisa preencher o apelido!")]
        [Column("APELIDO")]
        public string Apelido { get; set; }

        [Required(ErrorMessage = "Você precisa preencher a data de nascimento!")]
        [Column("DATA_NASCIMENTO")]
        public DateTime DataNascimento { get; set; }

    }
}
