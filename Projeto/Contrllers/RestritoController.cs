using Projeto.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{
    [PaginaParaUsuarioLogado]

    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
