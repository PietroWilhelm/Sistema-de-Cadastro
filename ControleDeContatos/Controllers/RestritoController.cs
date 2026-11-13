using ControleDeContatos.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

[PaginaParaUsuarioLogado]
public class RestritoController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}