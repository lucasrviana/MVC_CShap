using DevOI.UI.WebModelo.Models;

namespace DevOI.UI.WebModelo.Data
{
    public class PedidoRepository: IPedidoRepository
    {
        public Pedidos GetPedidos()
        {
            return new Pedidos();
        }
    }

    public interface IPedidoRepository
    {
        Pedidos GetPedidos();
    }
}
