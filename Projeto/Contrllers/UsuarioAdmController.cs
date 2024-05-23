using Projeto.Filters;
using Projeto.helper;
using Projeto.Models;
using Projeto.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioAdmController : Controller
    {
        private readonly IUsuarioAdmRepositorio _usuarioAdmRepositorio;
        private readonly IEmail _email;


        public UsuarioAdmController(IUsuarioAdmRepositorio usuarioAdmRepositorio, IEmail email)
        {

            _usuarioAdmRepositorio = usuarioAdmRepositorio;
            _email = email;
        }


        public IActionResult Index()
        {
            List<UsuarioAdmModel> usuarioAdm = _usuarioAdmRepositorio.BuscarRegistro();
            return View(usuarioAdm);
        }


        public IActionResult Criar()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Criar(UsuarioAdmModel usuarioAdm)
        {

            var VerificarLogin = _usuarioAdmRepositorio.BuscarPorLogin(usuarioAdm.Login);

            try
            {
                if (usuarioAdm.Email != usuarioAdm.ConfirmEmail)
                {
                    TempData["MensagemErro"] = $"Ops o e-mail coincidem! Tente novamente";
                }


                else if (usuarioAdm.ConfirmEmail == null)
                {
                    TempData["MensagemErro"] = $"Ops Há diversos campos que não estão preenchido";
                }


                else
                {
                    if (ModelState.IsValid)
                    {
                        if (VerificarLogin == null) 
                        {

                            usuarioAdm.Nome = usuarioAdm.Nome.ToUpper();

                            usuarioAdm.SobreNome = usuarioAdm.SobreNome.ToUpper();

                            _usuarioAdmRepositorio.Adicionar(usuarioAdm);


                            usuarioAdm = _usuarioAdmRepositorio.BuscarPorEmailELogin(usuarioAdm.Email, usuarioAdm.Login);



                            if (usuarioAdm != null)
                            {
                                string novaSenha = usuarioAdm.GerarNovaSenha();


                                string mensagem = $"Olá, sua nova senha é: {novaSenha}<br><br>Atenciosamente,<br>Grupo Guitta.";



                                bool emailEnviado = _email.Enviar(usuarioAdm.Email, "Grupo Guitta (Sistema de Cadastro - Nova Senha)", mensagem);

                                if (emailEnviado)
                                {
                                    _usuarioAdmRepositorio.Atualizar(usuarioAdm);
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

                

                return View(usuarioAdm);
            }

            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos cadastrar seu usuario, tente novamente, detalhe erro:{erro.Message}";

                return RedirectToAction("Index");

            }

        }




        public IActionResult Editar(int id)
        {
            UsuarioAdmModel usuarioAdm = _usuarioAdmRepositorio.ListarPorId(id);
            return View(usuarioAdm);
        }






        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Editar(UsuarioSemSenhaAdmModel usuarioSemSenhaAdmModel)
        {
            try
            {
                UsuarioAdmModel usuarioAdm = null;

                if (ModelState.IsValid)
                {
                    usuarioAdm = new UsuarioAdmModel()
                    {
                        Id = usuarioSemSenhaAdmModel.Id,
                        Nome = usuarioSemSenhaAdmModel.Nome,
                        SobreNome = usuarioSemSenhaAdmModel.SobreNome,
                        Login = usuarioSemSenhaAdmModel.Login,
                        Email = usuarioSemSenhaAdmModel.Email,
                        ConfirmEmail = usuarioSemSenhaAdmModel.ConfirmEmail,                        
                        Perfil = usuarioSemSenhaAdmModel.Perfil,
                        Status = usuarioSemSenhaAdmModel.Status,
                    };

                    usuarioAdm = _usuarioAdmRepositorio.Atualizar(usuarioAdm);
                    TempData["MensagemSucesso"] = "Usuario alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(usuarioAdm);
            }

            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops não conseguimos atualizar seu usuario, tente novamente, detalhe erro:{erro.Message}";

                return RedirectToAction("Index");
            }


        }




        public IActionResult Deletar(int id)
        {
            UsuarioAdmModel usuarioAdm = _usuarioAdmRepositorio.ListarPorId(id);
            return View(usuarioAdm);
        }


        public IActionResult Apagar(int id)
        {
            try
            {
                bool Apagado = _usuarioAdmRepositorio.Apagar(id);

                if (Apagado)
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
