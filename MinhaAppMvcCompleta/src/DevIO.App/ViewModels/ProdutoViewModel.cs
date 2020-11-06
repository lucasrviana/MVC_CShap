using DevIO.App.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevIO.App.ViewModels
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel()
        {
            Ativo = true;
        }



        [Key]
        public Guid Id { get; set; }
        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O Campo {0} precisa ter {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O Campo {0} precisa ter {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }
        [DisplayName("Imagem do produto")]
        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }

        //[Moeda]
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }
        public double ValorDouble { get; set;  }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        public FornecedorViewModel Fornecedor { get; set; }

        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }

    }
}
