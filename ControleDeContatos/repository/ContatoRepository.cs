using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.repository;

public class ContatoRepository : IContatoRepository
{
    //somente Leitura
    private readonly BancoContext _bancoContext;

    public ContatoRepository(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }

    public List<ContatoModel> BuscarTodos()
    {
        return _bancoContext.Contatos.ToList();
    }

    public ContatoModel Adiconar(ContatoModel contato)
    {   
        // Adiciona o contato no banco de dados
        _bancoContext.Contatos.Add(contato);
        // Salva as alterações no banco de dados
        _bancoContext.SaveChanges();
        return contato;
    }

    public ContatoModel Atulizar(ContatoModel contato)
    {
        throw new NotImplementedException();
    }

    public ContatoModel Apagar(int idContato)
    {
        throw new NotImplementedException();
    }
}