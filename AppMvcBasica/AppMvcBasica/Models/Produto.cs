using System;

namespace AppMvcBasica.Models
{
    public class Produto: Entity
    {
        public Guid FornecedorId { get; set; }
        public string Nome { get; set; }
    }
}
