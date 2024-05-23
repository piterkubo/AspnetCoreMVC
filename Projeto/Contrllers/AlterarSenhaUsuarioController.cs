using Microsoft.AspNetCore.Mvc;
using Projeto.Repositorio;
using Projeto.Models;
using Projeto.helper;
using System.Drawing;

namespace Projeto.Controllers
{
    public class AlterarSenhaUsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        private readonly ISessao _sessao;

        public AlterarSenhaUsuarioController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            
           
        }


        public IActionResult Index()
        {

            return View();
           
        }

               


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(AlterarSenhaUsuarioModel alterarSenhaUsuario)
        {
            try
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                alterarSenhaUsuario.Id = usuarioLogado.Id;
                                             
                

                if (ModelState.IsValid)
                {                                     

                    _usuarioRepositorio.AlterarSenha(alterarSenhaUsuario);
                    TempData["MensagemSucesso"] = "Usuario editado com sucesso";
                    _sessao.RemoverSessaoDoUsuario();
                    return RedirectToAction("Index", "Login");
                }

                return View("Index", alterarSenhaUsuario);

            }

            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos editar o usuario, tente novamente, detalhe erro:{erro.Message}";
                return View("Index", alterarSenhaUsuario);
            }
        }
    }
}




