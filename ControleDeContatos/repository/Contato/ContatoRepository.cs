using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.repository;

public class ContatoRepository : IContatoRepository
{
    //somente Leitura
    private readonly BancoContext _bancoContext;

    public ContatoRepository(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }


    public ContatoModel ListarPorId(int id)
    {
        return _bancoContext.Contatos.FirstOrDefault(c => c.Id == id);
    }

    public List<ContatoModel> BuscarTodos()
    {
        // Ajuste na consulta para ordenar os contatos pelo Id em ordem crescente
        var contatos = _bancoContext.Contatos.OrderBy(c => c.Id).ToList();
        return contatos;
    }

    public ContatoModel Adicionar(ContatoModel contato)
    {   
        // Adiciona o contato no banco de dados
        _bancoContext.Contatos.Add(contato);
        // Salva as alterações no banco de dados
        _bancoContext.SaveChanges();
        return contato;
    }

    public ContatoModel Atualizar(ContatoModel contato)
    {
        ContatoModel contaotDB = ListarPorId(contato.Id);
        if (contaotDB == null) throw new Exception("Houve um erro na atualização do contato");
        
        contaotDB.Nome = contato.Nome;
        contaotDB.Email = contato.Email;
        contaotDB.Celular = contato.Celular;
        _bancoContext.Contatos.Update(contaotDB);
        _bancoContext.SaveChanges();
        return contaotDB;
    }
    
    public bool Apagar(int id)
    {
        ContatoModel contatoDB = ListarPorId(id);
        if (contatoDB == null) throw new Exception("Houve um erro na deleção do contato");

        _bancoContext.Contatos.Remove(contatoDB);
        _bancoContext.SaveChanges();
        return true;
    }
}