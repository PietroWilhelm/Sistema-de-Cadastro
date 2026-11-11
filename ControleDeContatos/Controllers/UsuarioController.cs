using ControleDeContatos.Models.Usuario;
using ControleDeContatos.repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class UsuarioController : Controller
{
    
    // somento Leitura
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    
    // GET
    public IActionResult Index()
    {
        List<UsuarioModel> usuarios = _usuarioRepository.BuscarTodos();
        return View(usuarios);
    }
    
    public IActionResult Criar()
    {
        return View();
    }
    
    public IActionResult ApagarConfirmacao(int id)
    {
        UsuarioModel usuario = _usuarioRepository.ListarPorId(id);
        return View(usuario);
    }
    
    public IActionResult Apagar(int id)
    {
        try
        {
            bool apagado = _usuarioRepository.Apagar(id);
            if (apagado)
            {
                TempData["MensagemSucesso"] = "Usuário apagado com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Não foi possivel apagar seu usuário";
            }
            return RedirectToAction("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Não foi possivel apagar seu usuário, mais detalhes do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
        
    }
    
    [HttpPost]
    public IActionResult Criar(UsuarioModel usuario)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.Adicionar(usuario);
                TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Não foi possivel cadastrar o seu Usuário, tente novamente!, detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    } 
    
    public IActionResult Editar(int id)
    {
        UsuarioModel usuario = _usuarioRepository.ListarPorId(id);
        return View(usuario);
    }
    
    [HttpPost]
    public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
    {
        try
        {
            UsuarioModel usuario = null;
            if (ModelState.IsValid)
            {
                usuario = new UsuarioModel()
                {
                    Id = usuarioSemSenhaModel.Id,
                    Nome = usuarioSemSenhaModel.Nome,
                    Login = usuarioSemSenhaModel.Login,
                    Email = usuarioSemSenhaModel.Email,
                    Perfil = usuarioSemSenhaModel.Perfil
                };
                
               usuario = _usuarioRepository.Atualizar(usuario);
                TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                return RedirectToAction("Index");
            }

            return View("Editar", usuario);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Não foi possivel atualizar o seu Usuário, tente novamente!, detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }
}