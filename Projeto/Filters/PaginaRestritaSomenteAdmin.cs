using Projeto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;



namespace Projeto.Filters
{


    public class PaginaRestritaSomenteAdmin : ActionFilterAttribute
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

                UsuarioAdmModel usuarioAdm = JsonConvert.DeserializeObject<UsuarioAdmModel>(sessaoUsuario);

                

                if (usuarioAdm == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" } });
                }

                if(usuarioAdm.Perfil != Enums.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Restrito" }, { "Action", "Index" } });

                }
            }
            base.OnActionExecuted(context);
        }
    }
}
