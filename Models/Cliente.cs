using MovisisCadastro.Services.CustomValidations;
using MovisisCadastro.ViewModels;
using Newtonsoft.Json;
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
        [RegularExpression(@"^[a-zA-Z]{3,32}\s{0,1}[a-zA-Z]{0,18}$", ErrorMessage = "Nome deve conter apenas letras e espaço (se houver sobrenome)!")]
        public string Nome { get; set; }

        [Column("TELEFONE")]
        [Required(ErrorMessage = "Você precisa incluir o telefone!")]
        [RegularExpression(@"^[0-9]{8,13}$", ErrorMessage = "Telefone deve possuir apenas números!")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Telefone deve ter entre 10 e 13 números")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Você precisa atribuir um ID a cidade!")]
        [Column("ID_CIDADE")]
        public int CidadeId { get; set; }

        public Cidade Cidade { get; set; }

        [Required(ErrorMessage = "Você precisa preencher o apelido!")]
        [Column("APELIDO")]
        public string Apelido { get; set; }

        [Column("DATA_NASCIMENTO", TypeName = "date")]
        [JsonConverter(typeof(DataFormatValidationService), "dd-MM-yyyy")]
        [Required(ErrorMessage = "Data de nascimento deve ser preenchida com dia-mês_ano Ex: 25-12-2000!")]
        public DateTime DataNascimento { get; set; }

    }
}

//^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}$