using Microsoft.AspNetCore.Mvc;
using Projeto.Repositorio;
using Projeto.Models;
using Projeto.helper;

namespace Projeto.Controllers
{
    public class AlterarSenhaUsuarioPJController : Controller
    {
        private readonly IUsuarioPJRepositorio _usuarioPJRepositorio;

        private readonly ISessao _sessao;

        public AlterarSenhaUsuarioPJController(IUsuarioPJRepositorio usuarioPJRepositorio, ISessao sessao)
        {
            _usuarioPJRepositorio = usuarioPJRepositorio;
            _sessao = sessao;
           
        }



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Alterar(int id)
        {
           
            UsuarioPJModel usuarioPJ = _usuarioPJRepositorio.ListarPorId(id);                
            
            return View(usuarioPJ);
            

           
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(AlterarSenhaUsuarioPJModel alterarSenhaUsuarioPJ)
        {
            try
            {
                UsuarioPJModel usuarioLogado = _sessao.BuscarSessaoDoUsuarioPJ();
                alterarSenhaUsuarioPJ.Id = usuarioLogado.Id;

                if (ModelState.IsValid)
                {
                    _usuarioPJRepositorio.AlterarSenha(alterarSenhaUsuarioPJ);

                    TempData["MensagemSucesso"] = "Usuario editado com sucesso";
                    _sessao.RemoverSessaoDoUsuario();
                    return RedirectToAction("Index", "Login");
                }

                return View("Index", alterarSenhaUsuarioPJ);

            }

            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos editar o usuario, tente novamente, detalhe erro:{erro.Message}";
                return View("Index", alterarSenhaUsuarioPJ);
            }
        }
    }
}




