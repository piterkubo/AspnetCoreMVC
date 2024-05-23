using Projeto.Filters;
using Projeto.helper;
using Projeto.Models;
using Projeto.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{
    [PaginaParaUsuarioLogado]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEmail _email;


        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IEmail email)
        {

            _usuarioRepositorio = usuarioRepositorio;
            _email = email;
        }


        public IActionResult Index()
        {
            List<UsuarioModel> usuario = _usuarioRepositorio.BuscarRegistro();
            return View(usuario);
        }

        
        public IActionResult Criar()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Criar(UsuarioModel Usuario)
        {

            var VerificarLogin = _usuarioRepositorio.BuscarPorLogin(Usuario.Login);
            
            try
            {


                if (Usuario.Email != Usuario.ConfirmEmail)
                {
                    TempData["MensagemErro"] = $"Ops o e-mail coincidem! Tente novamente";
                }

              
                else if (Usuario.ConfirmEmail == null)
                {
                    TempData["MensagemErro"] = $"Ops Há diversos campos que não estão preenchido";
                }



                else
                {

                    if (ModelState.IsValid)
                    {
                        if (VerificarLogin == null)
                        {
                            Usuario.Nome = Usuario.Nome.ToUpper();

                            Usuario.SobreNome = Usuario.SobreNome.ToUpper();

                            _usuarioRepositorio.Adicionar(Usuario);


                            Usuario = _usuarioRepositorio.BuscarPorEmailELogin(Usuario.Email, Usuario.Login);


                            if (Usuario != null)
                            {
                                string novaSenha = Usuario.GerarNovaSenha();


                                string mensagem = $"Olá, sua nova senha é: {novaSenha}<br><br>Atenciosamente,<br>Grupo Guitta.";



                                bool emailEnviado = _email.Enviar(Usuario.Email, "Grupo Guitta (Sistema de Cadastro - Nova Senha)", mensagem);

                                if (emailEnviado)
                                {
                                    _usuarioRepositorio.Atualizar(Usuario);
                                    TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                                }


                                else
                                {
                                    TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail.  Tente Novamente.";

                                }

                                return RedirectToAction("Index");

                            }

                        }
                        TempData["MensagemErro"] = $"Ops Login ja cadastrado! Tente novamente";


                    }


                }

                return View(Usuario);
            }

            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos cadastrar seu usuario, tente novamente, detalhe erro:{erro.Message}";

                return RedirectToAction("Index");

            }

        }


        

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }





        
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome.ToUpper(),
                        SobreNome = usuarioSemSenhaModel.SobreNome.ToUpper(),
                        Login = usuarioSemSenhaModel.Login.ToUpper(),
                        Email = usuarioSemSenhaModel.Email,
                        ConfirmEmail = usuarioSemSenhaModel.ConfirmEmail,                        
                        Perfil = usuarioSemSenhaModel.Perfil,
                        Status = usuarioSemSenhaModel.Status,
                        AutorizacaoLGPD = usuarioSemSenhaModel.AutorizacaoLGPD
                    };

                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuario alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }

            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops não conseguimos atualizar seu usuario, tente novamente, detalhe erro:{erro.Message}";

                return RedirectToAction("Index");
            }


        }



        [PaginaParaUsuarioLogado]
        public IActionResult Deletar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        
        [PaginaParaUsuarioLogado]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuario deletado com sucesso";
                }

                else
                {
                    TempData["MensagemErro"] = "Ops não conseguimos atualizar seu usuario, tente novamente";
                }
                return RedirectToAction("Index");
            }

            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops não conseguimos atualizar seu usuario, tente novamente:{erro.Message}";
                return RedirectToAction("Index");
            }

        }
    }
}
