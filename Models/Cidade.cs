using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovisisCadastro.Models
{
    [Table("CIDADE")]
    public class Cidade
    {
        [Column("ID")]
        public int CidadeId { get; set; }

        [Column("NOME")]
        [Required(ErrorMessage = "Você precisa preencher o Nome!")]
        [RegularExpression(@"^[a-zA-Z]{1,32}\s{0,1}[a-zA-Z]{0,18}$", ErrorMessage = "Nome deve conter apenas letras e espaço (se houver sobrenome)!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Nome recebe entre 3 e 60 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Você precisa preencher a UF!")]
        [Column("UF")]
        [StringLength(2,ErrorMessage = "UF recebe apenas duas letras!")]
        public string UF { get; set; }

        public ICollection<Cliente> Clientes { get; set; }

    }
}

