using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControleDeContatos.Filters;

public class PaginaParaUsuarioLogado : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

        if (string.IsNullOrEmpty(sessaoUsuario))
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            return;
        }

        base.OnActionExecuting(context);
    }
}