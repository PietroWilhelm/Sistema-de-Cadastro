using ControleDeContatos.Models.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ControleDeContatos.ViewComponents;

public class Menu : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        string? sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

        if (string.IsNullOrEmpty(sessaoUsuario))
            return Content(string.Empty);

        UsuarioModel? usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);

        if (usuario is null)
            return Content(string.Empty);

        return View(usuario);
    }
}