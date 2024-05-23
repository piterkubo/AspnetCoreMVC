using Projeto.Data;
using Projeto.Enums;
using Projeto.Filters;
using Projeto.helper;
using Projeto.Models;
using Projeto.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using MySqlConnector;
using NuGet.Protocol.Plugins;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Projeto.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IUsuarioAdmRepositorio _usuarioAdmRepositorio;
        private readonly IUsuarioPJRepositorio _usuarioPJRepositorio;
       
        private readonly ISessao _sessao;
        private readonly IEmail _email;
     

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email,
            IUsuarioAdmRepositorio usuarioAdmRepositorio, IUsuarioPJRepositorio usuarioPJRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _usuarioAdmRepositorio = usuarioAdmRepositorio;
            _usuarioPJRepositorio = usuarioPJRepositorio;
            _sessao = sessao;
            _email = email;
          
            
        }



        public IActionResult Index()
        {
          

            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }


        public IActionResult NovoUsuario()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult NovoUsuario(NovoUsuarioModel Novousuario)
        {
            var VerificarLogin = _usuarioRepositorio.BuscarPorLogin(Novousuario.Login);

            UsuarioModel Usuario = new UsuarioModel();

            try
            {

                
                if (Novousuario.Email != Novousuario.ConfirmEmail)
                {
                    TempData["MensagemErro"] = $"Ops o e-mail coincidem! Tente novamente";
                }

                

                else if (Novousuario.ConfirmEmail == null)
                {
                    TempData["MensagemErro"] = $"Ops Há diversos campos que não estão preenchido";
                }



                else
                {

                    if (ModelState.IsValid)
                    {
                        if (VerificarLogin == null)
                        {

                            Usuario.Nome = Novousuario.Nome.ToUpper();
                            Usuario.SobreNome = Novousuario.SobreNome.ToUpper();
                            Usuario.Email = Novousuario.Email;
                            Usuario.ConfirmEmail = Novousuario.ConfirmEmail;
                            Usuario.Login = Novousuario.Login;
                            Usuario.AutorizacaoLGPD = Novousuario.AutorizacaoLGPD;
                            

                            _usuarioRepositorio.Adicionar(Usuario);


                           

                            Usuario = _usuarioRepositorio.BuscarPorEmailELogin(Usuario.Email, Usuario.Login);


                            if(Usuario != null)
                            {
                                string novaSenha = Usuario.GerarNovaSenha();


                                string mensagem = $"Olá, sua nova senha é: {novaSenha}<br><br>Atenciosamente,<br>GrupoTotal.";



                                bool emailEnviado = _email.Enviar(Usuario.Email, "Grupototal (Sistema de Cadastro - Nova Senha)", mensagem);

                                if (emailEnviado)
                                {
                                    _usuarioRepositorio.Atualizar(Usuario);
                                    TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                                }


                                else
                                {
                                    TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail.  Tente Novamente.";

                                }

                                return RedirectToAction("Index", "Login");

                            }

                        

                        }
                        

                    }
                    TempData["MensagemErro"] = $"Ops Login ja cadastrado! Tente novamente";


                }




                return View(Novousuario);
            }

            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos cadastrar seu usuario, tente novamente, detalhe erro:{erro.Message}";

                return RedirectToAction("Index");

            }





        }




        public IActionResult NovoUsuarioPJ()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult NovoUsuarioPJ(NovoUsuarioPJModel NovoUsuarioPJ)
        {


            var VerificarLogin = _usuarioPJRepositorio.BuscarPorLogin(NovoUsuarioPJ.Login);

            UsuarioPJModel UsuarioPJ = new UsuarioPJModel();

            try
            {


                if (NovoUsuarioPJ.Email != NovoUsuarioPJ.ConfirmEmail)
                {
                    TempData["MensagemErro"] = $"Ops o e-mail coincidem! Tente novamente";
                }

             
                else if (NovoUsuarioPJ.Login == null)
                {
                    TempData["MensagemErro"] = $"Ops Há diversos campos que não estão preenchido";
                }



                else
                {

                    if (ModelState.IsValid)
                    {


                        if (VerificarLogin == null)                        
                        {
                            UsuarioPJ.Login = NovoUsuarioPJ.Login.Replace(".", "").Replace("-", "").Replace("/", "");
                            
                            UsuarioPJ.Nome = NovoUsuarioPJ.Nome.ToUpper();

                            UsuarioPJ.SobreNome = NovoUsuarioPJ.SobreNome.ToUpper();

                            UsuarioPJ.RazaoSocial = NovoUsuarioPJ.RazaoSocial.ToUpper();

                            UsuarioPJ.DataAbertura = NovoUsuarioPJ.DataAbertura;

                            UsuarioPJ.Email = NovoUsuarioPJ.Email;                            

                            UsuarioPJ.ConfirmEmail = NovoUsuarioPJ.ConfirmEmail;

                            UsuarioPJ.AutorizacaoLGPD = NovoUsuarioPJ.AutorizacaoLGPD;

                            _usuarioPJRepositorio.Adicionar(UsuarioPJ);


                            UsuarioPJModel usuarioPJ = _usuarioPJRepositorio.BuscarPorEmailELogin(UsuarioPJ.Email, UsuarioPJ.Login);

                            if (usuarioPJ != null)
                            {
                                string novaSenha = usuarioPJ.GerarNovaSenha();


                                string mensagem = $"Olá, sua nova senha é: {novaSenha}<br><br>Atenciosamente,<br>GrupoTotal.";

                                bool emailEnviado = _email.Enviar(usuarioPJ.Email, "Sistema de Cadastro - Nova Senha", mensagem);

                                if (emailEnviado)
                                {
                                    _usuarioPJRepositorio.Atualizar(usuarioPJ);
                                    TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                                }


                                else
                                {
                                    TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail.  Tente Novamente.";

                                }

                                return RedirectToAction("Index", "Login");


                            }



                        }

                    }
                    TempData["MensagemErro"] = $"Ops Login ja cadastrado! Tente novamente";


                }



                return View(NovoUsuarioPJ);
            }

            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos cadastrar seu usuario, tente novamente, detalhe erro:{erro.Message}";

                return RedirectToAction("Index", "Login");

            }




        }










        public IActionResult RedefinirSenha()
        {
            return View();
        }


       


        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }



        [HttpPost]

        public IActionResult Logar(LoginModel loginModel)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                    
                    if (usuario != null)
                    {

                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha invalido(s). Favor tente novamente.";

                    }



                    UsuarioAdmModel usuarioAdm = _usuarioAdmRepositorio.BuscarPorLogin(loginModel.Login);
                    
                    if(usuarioAdm != null)
                    {
                        if (usuarioAdm.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuarioAdm(usuarioAdm);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha invalido(s). Favor tente novamente.";
                    }


                    UsuarioPJModel usuarioPJ = _usuarioPJRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuarioPJ != null)
                    {
                        if (usuarioPJ.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuarioPJ(usuarioPJ);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha invalido(s). Favor tente novamente.";
                    }





                    TempData["MensagemErro"] = $"Usuario e/ou senha invalido(s). Favor tente novamente.";
                }


                


                return View("Index");


            }



            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos realizar seu Login, tente novamente, detalhe erro:{erro.Message}";
                return RedirectToAction("Index");
            }

        }



        [HttpPost]

        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                       

                        string mensagem = $"Olá, sua Nova Senha é: {novaSenha}<br><br>Atenciosamente, <br><br><GrupoTotal.";

                       

                        bool emailEnviado = _email.Enviar(usuario.Email,"GrupoTotal (Sistema de Cadastro - Nova Senha)",mensagem);

                        if (emailEnviado) 
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                        }


                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail.  Tente Novamente.";

                        }

                        return RedirectToAction("Index", "Login");






                    }

                    UsuarioAdmModel usuarioAdm = _usuarioAdmRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuarioAdm != null)
                    {
                        string novaSenha = usuarioAdm.GerarNovaSenha();


                        string mensagem = $"Olá, sua Nova Senha é: {novaSenha}<br><br>Atenciosamente, <br><br>GrupoTotal.";

                        bool emailEnviado = _email.Enviar(usuarioAdm.Email, "Sistema de Cadastro - Nova Senha", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioAdmRepositorio.Atualizar(usuarioAdm);
                            TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                        }


                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail.  Tente Novamente.";

                        }

                        return RedirectToAction("Index", "Login");


                    }


                    UsuarioPJModel usuarioPJ = _usuarioPJRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuarioPJ != null)
                    {
                        string novaSenha = usuarioPJ.GerarNovaSenha();


                        string mensagem = $"Olá, sua Nova Senha é: {novaSenha}<br><br>Atenciosamente, <br><br>GrupoTotal.";

                        bool emailEnviado = _email.Enviar(usuarioPJ.Email, "Sistema de Cadastro - Nova Senha", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioPJRepositorio.Atualizar(usuarioPJ);
                            TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                        }


                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail.  Tente Novamente.";

                        }

                        return RedirectToAction("Index", "Login");


                    }



                    

                    TempData["MensagemErro"] = $"Ops não conseguimos redefinir sua senha. Favor verifique os dados informados.";
                }

                return View("Index");


            }



            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos redefinir sua senha, tente novamente, detalhe erro:{erro.Message}";
                return RedirectToAction("Index");
            }

        }


        public IActionResult ClienteDomus()
        {
            return View();
        }




        [HttpPost]
        public IActionResult ValidarClienteDomus(string login, string email, RedefinirSenhaModel redefinirSenhaModel)
        {
            
            if (login.Length == 11 ) {

                // Supondo que você tenha os parâmetros 'login' e 'email' vindo do formulário

                var clientesDomus = _usuarioRepositorio.GetClienteDomus();

                bool encontrado = false;

                


                NovoUsuarioModel UsuarioPFDomus = new NovoUsuarioModel();
                UsuarioPFDomus.Login = login; // atribua o valor de 'login' aqui
                var verificarLoginMysqlPF = _usuarioRepositorio.BuscarPorLogin(UsuarioPFDomus.Login);



                if (verificarLoginMysqlPF != null)
                {
                    TempData["MensagemErro"] = $"OBS: Login já foi ativado! Favor entre com contato com o seu operador !";
                    return RedirectToAction("Index", "Login");
                }


                else
                {
                    foreach (var clienteDomusconsultado in clientesDomus)
                    {



                        if (clienteDomusconsultado.Login == login && clienteDomusconsultado.Email == email)
                        {
                            encontrado = true;
                            _usuarioRepositorio.AdicionarClienteDomus(clienteDomusconsultado);
                            break; // Encerrar o loop, pois encontramos um correspondente             

                        }



                        else if (clienteDomusconsultado.Login == login || clienteDomusconsultado.Email == email)
                        {
                            TempData["MensagemErro"] = $"Login ou E-mail Invalidos!.  Tente Novamente.";
                        }


                    }


                    if (ModelState.IsValid)
                    {
                        UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                        if (usuario != null)
                        {
                            string novaSenha = usuario.GerarNovaSenha();


                            string mensagem = $"Olá, sua Nova Senha é: {novaSenha}<br><br>Atenciosamente, <br><br><GrupoTotal.";



                            bool emailEnviado = _email.Enviar(usuario.Email, "GrupoTotal (Sistema de Cadastro - Nova Senha)", mensagem);

                            if (emailEnviado)
                            {
                                _usuarioRepositorio.Atualizar(usuario);
                                TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                            }


                            else
                            {
                                TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail.  Tente Novamente.";

                            }

                            return RedirectToAction("Index", "Login");

                        }

                    }



                    else if (!encontrado)
                    {
                        TempData["MensagemErro"] = $"O email ou login digitado não existe.";
                    }
                    

                }

               

            }

            if (login.Length > 11)
            {

                // Supondo que você tenha os parâmetros 'login' e 'email' vindo do formulário

                var clientesDomus = _usuarioPJRepositorio.GetClienteDomus();

                bool encontrado = false;

                
                


                NovoUsuarioPJModel UsuarioPJDomus = new NovoUsuarioPJModel();
                UsuarioPJDomus.Login = login; // atribua o valor de 'login' aqui  

                var verificarLoginMysqlPJ = _usuarioPJRepositorio.BuscarPorLogin(UsuarioPJDomus.Login);



                if (verificarLoginMysqlPJ != null )
                {
                    TempData["MensagemErro"] = $"OBS: Login já foi ativado! Favor entre com contato com o seu operador !";
                    return RedirectToAction("Index", "Login");

                }


                else
                {
                                  
                    UsuarioPJDomus.Email = email;
                    string emailEncontrado = email;

                    foreach (var clienteDomusconsultado in clientesDomus)
                    {


                        // Divide a string de emails em vários emails
                        var emails = clienteDomusconsultado.Email.Split(';').Select(e => e.Trim());

                        if (clienteDomusconsultado.Login == login && emails.Contains(email))
                        {
                            encontrado = true;

                            emailEncontrado = email;

                            _usuarioPJRepositorio.AdicionarClienteDomus(clienteDomusconsultado);
                            break; // Encerrar o loop, pois encontramos um correspondente      


                        }



                        else if (clienteDomusconsultado.Login == login || clienteDomusconsultado.Email == email)
                        {
                            TempData["MensagemErro"] = $"Login ou E-mail Invalidos!.  Tente Novamente.";
                        }


                    }


                    if (ModelState.IsValid)
                    {
                        UsuarioPJModel usuarioPJ = _usuarioPJRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                        if (usuarioPJ != null)
                        {

                            // Separe os emails armazenados no banco de dados
                            string[] emailsArmazenados = usuarioPJ.Email.Split(';');

                            if (emailsArmazenados.Contains(redefinirSenhaModel.Email)) 
                            {
                                string novaSenha = usuarioPJ.GerarNovaSenha();


                                string mensagem = $"Olá, sua Nova Senha é: {novaSenha}<br><br>Atenciosamente, <br><br><GrupoTotal.";


                                bool emailEnviado = _email.Enviar(redefinirSenhaModel.Email, "GrupoTotal (Sistema de Cadastro - Nova Senha)", mensagem);


                                if (emailEnviado)
                                {
                                    _usuarioPJRepositorio.Atualizar(usuarioPJ);
                                    TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                                }


                                else
                                {
                                    TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail.  Tente Novamente.";

                                }

                                return RedirectToAction("Index", "Login");

                            }                           


                        }

                    }





                    else if (!encontrado)
                    {
                        TempData["MensagemErro"] = $"O email ou login digitado não existe.";
                    }



                }



            }

            return View("Index");

        }

    }

}



