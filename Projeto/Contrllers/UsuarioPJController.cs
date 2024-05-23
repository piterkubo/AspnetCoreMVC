using Projeto.Filters;
using Projeto.helper;
using Projeto.Models;
using Projeto.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioPJController : Controller
    {
        private readonly IUsuarioPJRepositorio _usuarioPJRepositorio;
        private readonly IEmail _email;


        public UsuarioPJController(IUsuarioPJRepositorio usuarioPJRepositorio, IEmail email)
        {

            _usuarioPJRepositorio = usuarioPJRepositorio;
            _email = email;
        }


        public IActionResult Index()
        {
            List<UsuarioPJModel> usuarioPJ = _usuarioPJRepositorio.BuscarRegistro();
            return View(usuarioPJ);
        }


        public IActionResult Criar()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Criar(UsuarioPJModel UsuarioPJ)
        {

            var VerificarLogin = _usuarioPJRepositorio.BuscarPorLogin(UsuarioPJ.Login);
            
            try
            {
                
                
                
                
                if (UsuarioPJ.Email != UsuarioPJ.ConfirmEmail)
                {
                    TempData["MensagemErro"] = $"Ops o e-mail coincidem! Tente novamente";
                }



                else if (UsuarioPJ.Login == null)
                {
                    TempData["MensagemErro"] = $"Ops Há diversos campos que não estão preenchido";
                }




                else
                {


                    if (ModelState.IsValid)
                    {
                       if(VerificarLogin == null)
                       {



                            UsuarioPJ.Login = UsuarioPJ.Login.Replace(".", "").Replace("-", "").Replace("/", "");

                            UsuarioPJ.Nome = UsuarioPJ.Nome.ToUpper();

                            UsuarioPJ.SobreNome = UsuarioPJ.SobreNome.ToUpper();

                            UsuarioPJ.RazaoSocial = UsuarioPJ.RazaoSocial.ToUpper();

                            UsuarioPJ.DataAbertura = UsuarioPJ.DataAbertura;

                            UsuarioPJ.Email = UsuarioPJ.Email;

                            UsuarioPJ.ConfirmEmail = UsuarioPJ.ConfirmEmail;

                            UsuarioPJ.AutorizacaoLGPD = UsuarioPJ.AutorizacaoLGPD;

                            _usuarioPJRepositorio.Adicionar(UsuarioPJ);


                            UsuarioPJ = _usuarioPJRepositorio.BuscarPorEmailELogin(UsuarioPJ.Email, UsuarioPJ.Login);

                            
                            if (UsuarioPJ != null)
                            {
                                string novaSenha = UsuarioPJ.GerarNovaSenha();


                                string mensagem = $"Olá, sua nova senha é: {novaSenha}<br><br>Atenciosamente,<br>Grupo Guitta.";



                                bool emailEnviado = _email.Enviar(UsuarioPJ.Email, "Grupo Guitta (Sistema de Cadastro - Nova Senha)", mensagem);

                                if (emailEnviado)
                                {
                                    _usuarioPJRepositorio.Atualizar(UsuarioPJ);
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
                return View(UsuarioPJ);


            }

            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos cadastrar seu usuario, tente novamente, detalhe erro:{erro.Message}";

                return RedirectToAction("Index");

            }

        }




        public IActionResult Editar(int id)
        {
            UsuarioPJModel usuarioPJ = _usuarioPJRepositorio.ListarPorId(id);
            return View(usuarioPJ);
        }






        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Editar(UsuarioSemSenhaPJModel usuarioSemSenhaPJModel)
        {
            try
            {
                UsuarioPJModel usuarioPJ = null;

                if (ModelState.IsValid)
                {
                    usuarioPJ = new UsuarioPJModel()
                    {
                        Id = usuarioSemSenhaPJModel.Id,
                        Nome = usuarioSemSenhaPJModel.Nome,
                        SobreNome = usuarioSemSenhaPJModel.SobreNome,
                        RazaoSocial = usuarioSemSenhaPJModel.RazaoSocial,
                        Login = usuarioSemSenhaPJModel.Login,
                        DataAbertura = usuarioSemSenhaPJModel.DataAbertura,
                        Email = usuarioSemSenhaPJModel.Email,
                        ConfirmEmail = usuarioSemSenhaPJModel.ConfirmEmail,                        
                        Perfil = usuarioSemSenhaPJModel.Perfil,
                        Status = usuarioSemSenhaPJModel.Status,
                    };

                    usuarioPJ = _usuarioPJRepositorio.Atualizar(usuarioPJ);
                    TempData["MensagemSucesso"] = "Usuario alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(usuarioPJ);
            }

            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops não conseguimos atualizar seu usuario, tente novamente, detalhe erro:{erro.Message}";

                return RedirectToAction("Index");
            }


        }




        public IActionResult Deletar(int id)
        {
            UsuarioPJModel usuarioPJ = _usuarioPJRepositorio.ListarPorId(id);
            return View(usuarioPJ);
        }


        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioPJRepositorio.Apagar(id);

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
