using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace CoreIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClaim()
        {
            return View("Secret");
        }

        [Authorize(Policy = "PodeLer")]
        public IActionResult SecretClaimContains()
        {
            return View("Secret");
        }

        [ClaimAuthorize("Produtos", "Ler")]
        public IActionResult SecretClaimCustom()
        {
            return View("Secret");
        }

        [Route("/Erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {

            HttpStatusCode erro = (HttpStatusCode)id;


            var modelErro = new ErrorViewModel()
            {
                ErroCode = id,
                Titulo = erro.ToString()
            };

            if (id == 500)
                modelErro.Mensagem = "Ocorreu um erro interno";
            else if(id == 404)
                modelErro.Mensagem = "A pagina não existe!";
            else if (id == 403)
                modelErro.Mensagem = "Voce não tem permissão para acessar essa pagina";
            else
                modelErro.Mensagem = "Consulte o setor de desenvolvimento";


            return View("Error",modelErro);
        }
    }
}
