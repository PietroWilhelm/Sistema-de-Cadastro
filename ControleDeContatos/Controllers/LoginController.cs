using ControleDeContatos.Models.Login;
using ControleDeContatos.Models.Usuario;
using ControleDeContatos.repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class LoginController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Entrar(LoginModel loginModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
               UsuarioModel usuario = _usuarioRepository.BuscarPorLogin(loginModel.Login);
               if (usuario != null)
               {
                   if (usuario.SenhaValida(loginModel.Senha))
                   {
                       return RedirectToAction("Index", "Home");
                   }   
                   TempData["MensagemErro"] = $"Senha do usuário é inválida. Por favor, tente novamente.";
               } 
               TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
            }
            return View("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Não Foi possivel realizar seu login, tente novamente, detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
        
    }
}