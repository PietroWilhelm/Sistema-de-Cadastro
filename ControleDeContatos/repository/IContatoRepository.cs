using ControleDeContatos.Models;

namespace ControleDeContatos.repository;

public interface IContatoRepository
{
    List<ContatoModel> BuscarTodos();
    ContatoModel Adiconar(ContatoModel contato);
    ContatoModel Atulizar(ContatoModel contato);
    ContatoModel Apagar(int idContato);
}