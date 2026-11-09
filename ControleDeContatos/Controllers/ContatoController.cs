using ControleDeContatos.Models;
using ControleDeContatos.repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class ContatoController : Controller
{
    // somento Leitura
    private readonly IContatoRepository _contatoRepository;

    public ContatoController(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }
    
    
    public IActionResult Index()
    {
        List<ContatoModel> contatos = _contatoRepository.BuscarTodos();
        return View(contatos);
    }
    
    public IActionResult Criar()
    {
        return View();
    }
    
    public IActionResult Editar(int id)
    {
        ContatoModel contato = _contatoRepository.ListarPorId(id);
        return View(contato);
    }
    
    public IActionResult ApagarConfirmacao()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(ContatoModel contato)
    {
        _contatoRepository.Adiconar(contato);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Alterar(ContatoModel contato)
    {
        _contatoRepository.Atualizar(contato);
        return RedirectToAction("Index");
    }
}