using ControleDeContatos.Models;

namespace ControleDeContatos.repository;

public interface IContatoRepository
{
    ContatoModel ListarPorId(int id);
    List<ContatoModel> BuscarTodos();
    ContatoModel Adiconar(ContatoModel contato);
    ContatoModel Atualizar(ContatoModel contato);
    bool Apagar(int id);
}