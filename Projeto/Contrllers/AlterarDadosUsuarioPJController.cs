using Projeto.Filters;
using Projeto.helper;
using Projeto.Models;
using Projeto.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{

    [PaginaParaUsuarioLogado]
    public class AlterarDadosUsuarioPJController : Controller
    {
        private readonly IUsuarioPJRepositorio _usuarioPJRepositorio;

        private readonly ISessao _sessao;



        public AlterarDadosUsuarioPJController(IUsuarioPJRepositorio usuarioPJRepositorio, ISessao sessao)
        {
            _usuarioPJRepositorio = usuarioPJRepositorio;
            _sessao = sessao;


        }


        public IActionResult Index()
        {
            UsuarioPJModel UsuarioPJ = _sessao.BuscarSessaoDoUsuarioPJ();


            AlterarDadosUsuarioPJModel VerUsuarioPJ = new AlterarDadosUsuarioPJModel();


            if (VerUsuarioPJ != null)
            {
                VerUsuarioPJ.Id = UsuarioPJ.Id;
                VerUsuarioPJ.Nome = UsuarioPJ.Nome.ToUpper();
                VerUsuarioPJ.SobreNome = UsuarioPJ.SobreNome.ToUpper();
                VerUsuarioPJ.RazaoSocial = UsuarioPJ.RazaoSocial.ToUpper();
                VerUsuarioPJ.Email = UsuarioPJ.Email;
                VerUsuarioPJ.ConfirmEmail = UsuarioPJ.ConfirmEmail;

            }

            return View(VerUsuarioPJ);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(AlterarDadosUsuarioPJModel alterarDadosUsuarioPJ)
        {
            try
            {
                UsuarioPJModel usuarioPJLogado = _sessao.BuscarSessaoDoUsuarioPJ();
                alterarDadosUsuarioPJ.Id = usuarioPJLogado.Id;


                if (ModelState.IsValid)
                {


                    usuarioPJLogado = _usuarioPJRepositorio.AlterarPerfil(alterarDadosUsuarioPJ);

                    TempData["MensagemSucesso"] = "Usuario editado com sucesso";

                    _sessao.AtualizarSessaoDoUsuarioPJ(usuarioPJLogado);
                    return RedirectToAction("Index", "Home");

                }

                return View("Index", alterarDadosUsuarioPJ);

            }


            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos editar o usuario, tente novamente, detalhe erro:{erro.Message}";
                return View(alterarDadosUsuarioPJ);
            }


        }

    }
}
