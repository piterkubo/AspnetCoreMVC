using Microsoft.AspNetCore.Mvc;
using Projeto.Repositorio;
using Projeto.Models;
using Projeto.helper;

namespace Projeto.Controllers
{
    public class AlterarSenhaUsuarioAdmController : Controller
    {
        private readonly IUsuarioAdmRepositorio _usuarioAdmRepositorio;

        private readonly ISessao _sessao;

        public AlterarSenhaUsuarioAdmController(IUsuarioAdmRepositorio usuarioAdmRepositorio, ISessao sessao)
        {
            _usuarioAdmRepositorio = usuarioAdmRepositorio;
            _sessao = sessao;
           
        }



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Alterar(int id)
        {
           
            UsuarioAdmModel usuarioAdm = _usuarioAdmRepositorio.ListarPorId(id);                
            
            return View(usuarioAdm);
            

           
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(AlterarSenhaUsuarioAdmModel alterarSenhaUsuarioAdm)
        {
            try
            {
                UsuarioAdmModel usuarioLogado = _sessao.BuscarSessaoDoUsuarioAdm();
                alterarSenhaUsuarioAdm.Id = usuarioLogado.Id;

                if (ModelState.IsValid)
                {
                    _usuarioAdmRepositorio.AlterarSenha(alterarSenhaUsuarioAdm);

                    TempData["MensagemSucesso"] = "Usuario editado com sucesso";
                    _sessao.RemoverSessaoDoUsuario();
                    return RedirectToAction("Index", "Login");
                }

                return View("Index", alterarSenhaUsuarioAdm);

            }

            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos editar o usuario, tente novamente, detalhe erro:{erro.Message}";
                return View("Index", alterarSenhaUsuarioAdm);
            }
        }
    }
}




