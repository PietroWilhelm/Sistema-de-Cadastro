using System.Text.Json;
using ControleDeContatos.Enums;
using ControleDeContatos.Models.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace ControleDeContatos.Filters;

// Exige login e perfil Admin. Usuários logados sem perfil Admin vão para a tela restrita.
public class PaginaParaAdmin : PaginaParaUsuarioLogado
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        if (context.Result != null) return;

        var sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
        var usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);

        if (usuario?.Perfil != PerfilEnum.Admin)
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
        }
    }
}
