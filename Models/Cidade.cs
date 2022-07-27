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


        [Required(ErrorMessage = "Você precisa preencher o Nome!")]
        [Column("NOME")]

        [StringLength(60, MinimumLength = 3, ErrorMessage = "Nome recebe entre 3 e 60 caracteres!")]

        public string Nome { get; set; }

        [Required(ErrorMessage = "Você precisa preencher a UF!")]
        [Column("UF")]
        [StringLength(2,ErrorMessage = "UF recebe apenas duas letras!")]
        public string UF { get; set; }

        public ICollection<Cliente> Clientes { get; set; }

    }
}


//[RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]