using Projeto.Models;
using Projeto.helper;
using Projeto.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Projeto.Filters;
using System.Xml;

namespace Projeto.Controllers
{
    [PaginaParaUsuarioLogado]
    public class AlterarDadosUsuarioController : Controller
    {
        
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        private readonly ISessao _sessao;

        public AlterarDadosUsuarioController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;


        }
        public IActionResult Index()
        {
            UsuarioModel Usuario = _sessao.BuscarSessaoDoUsuario();


            AlterarDadosUsuarioModel VerUsuario = new AlterarDadosUsuarioModel();


            

          
            if (VerUsuario != null)
            {
                VerUsuario.Id = Usuario.Id;
                VerUsuario.Nome = Usuario.Nome.ToUpper();
                VerUsuario.SobreNome = Usuario.SobreNome.ToUpper();
                VerUsuario.Email = Usuario.Email;
                VerUsuario.ConfirmEmail = Usuario.ConfirmEmail;

            }

            return View(VerUsuario);

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(AlterarDadosUsuarioModel alterarDadosUsuario)
        {
            try
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                alterarDadosUsuario.Id = usuarioLogado.Id;


                if (ModelState.IsValid)
                {
                                       

                    usuarioLogado = _usuarioRepositorio.AlterarPerfil(alterarDadosUsuario);                    
                    
                    TempData["MensagemSucesso"] = "Usuario editado com sucesso";

                    _sessao.AtualizarSessaoDoUsuario(usuarioLogado);
                    return RedirectToAction("Index", "Home");
                    
                }

                return View("Index", alterarDadosUsuario);

            }

            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos editar o usuario, tente novamente, detalhe erro:{erro.Message}";
                return View(alterarDadosUsuario);
            }
        }


    }
}
