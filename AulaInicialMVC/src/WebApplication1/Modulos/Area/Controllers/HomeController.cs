using Microsoft.AspNetCore.Mvc;

namespace DevOI.UI.WebModelo.Controllers
{
    [Area("Area")]
    //[Route("Area/[controller]/[action]")]
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
