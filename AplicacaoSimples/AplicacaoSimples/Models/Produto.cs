using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoSimples.Models
{
    public class Produto : Entity
    {
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O Campo {0} precisa ter {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O Campo {0} precisa ter {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O Campo {0} precisa ter {2} e {1} caracteres", MinimumLength = 2)]
        public string Imagem { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        // EF Relations
        public Fornecedor Fornecedor { get; set; }
    }
}
