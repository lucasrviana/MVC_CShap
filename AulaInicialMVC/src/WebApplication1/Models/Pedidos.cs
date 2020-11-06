using System;

namespace DevOI.UI.WebModelo.Models
{
    public class Pedidos
    {
        public Guid ID { get; set; }

        public Pedidos()
        {
            ID = Guid.NewGuid();
        }
    }
}
