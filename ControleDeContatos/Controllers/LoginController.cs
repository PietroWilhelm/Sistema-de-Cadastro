using ControleDeContatos.Helper;
using ControleDeContatos.Models.Login;
using ControleDeContatos.Models.Usuario;
using ControleDeContatos.repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class LoginController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ISessao _sessao;

    public LoginController(IUsuarioRepository usuarioRepository,ISessao sessao )
    {
        _usuarioRepository = usuarioRepository;
        _sessao = sessao;
    }
    
    // GET
    public IActionResult Index()
    {
        // se usuer estiver logado, redirecionar para home
        if (_sessao.BuscarSessaoDoUsuario() != null)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    public IActionResult Sair()
    {
        _sessao.RemoverSessaoDoUsuario();
        return RedirectToAction("Index", "Login");
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
                       // criação da sessão do user
                       _sessao.CriarSessaoDoUsuario(usuario);
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