using Projeto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;



namespace Projeto.Filters
{
    


    public class PaginaParaUsuarioLogado :ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"Controller", "Login" },{"Action","Index" } });
            }
            else
            {

                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
                UsuarioAdmModel usuarioAdm = JsonConvert.DeserializeObject<UsuarioAdmModel>(sessaoUsuario);
                UsuarioPJModel usuarioPJ = JsonConvert.DeserializeObject<UsuarioPJModel>(sessaoUsuario);

                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" } });
                }

                else if(usuarioAdm == null) 
                {

                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" } });

                }

                else if (usuarioPJ == null)
                {

                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" } });

                }

            }
            base.OnActionExecuted(context);
        }
    }
}
