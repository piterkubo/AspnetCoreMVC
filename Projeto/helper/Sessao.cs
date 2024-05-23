using Projeto.Models;
using Newtonsoft.Json;


namespace Projeto.helper
{


    public class Sessao: ISessao

    {
        private readonly IHttpContextAccessor _httpContext;



        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }



        public UsuarioModel BuscarSessaoDoUsuario()
        {

            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

        }



       public void CriarSessaoDoUsuario(UsuarioModel usuario)
       {
            string valor = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);

       }


        public void AtualizarSessaoDoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }




        public void RemoverSessaoDoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }





        public UsuarioAdmModel BuscarSessaoDoUsuarioAdm()
        {

            string sessaoUsuarioAdm = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuarioAdm)) return null;

            return JsonConvert.DeserializeObject<UsuarioAdmModel>(sessaoUsuarioAdm);

        }



        public void CriarSessaoDoUsuarioAdm(UsuarioAdmModel usuarioAdm)
        {
            string valor = JsonConvert.SerializeObject(usuarioAdm);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);

        }

        public void RemoverSessaoDoUsuarioAdm()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }






        public UsuarioPJModel BuscarSessaoDoUsuarioPJ()
        {

            string sessaoUsuarioPJ = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuarioPJ)) return null;

            return JsonConvert.DeserializeObject<UsuarioPJModel>(sessaoUsuarioPJ);

        }



        public void CriarSessaoDoUsuarioPJ(UsuarioPJModel usuarioPJ)
        {
            string valor = JsonConvert.SerializeObject(usuarioPJ);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);

        }



        public void AtualizarSessaoDoUsuarioPJ(UsuarioPJModel usuarioPJ)
        {
            string valor = JsonConvert.SerializeObject(usuarioPJ);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }




        public void RemoverSessaoDoUsuarioPJ()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }


        




    }
}
