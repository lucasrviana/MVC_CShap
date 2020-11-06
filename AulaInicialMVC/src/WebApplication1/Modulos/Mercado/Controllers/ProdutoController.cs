using DevOI.UI.WebModelo.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevOI.UI.WebModelo.Teste.Controllers
{
    [Area("Mercado")]
    public class ProdutoController : Controller
    {
        //private readonly IPedidoRepository _pedidoContext;

        //public ProdutoController(IPedidoRepository pedidoContext)
        //{
        //    _pedidoContext = pedidoContext;
        //}

        public IActionResult Index([FromServices]IPedidoRepository _pedidoContext)
        {
            var produto = _pedidoContext.GetPedidos();
            return View();
        }
    }
}