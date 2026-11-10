using ControleDeContatos.Models.Usuario;

namespace ControleDeContatos.repository;

public interface IUsuarioRepository
{
    UsuarioModel ListarPorId(int id);
    List<UsuarioModel> BuscarTodos();
    UsuarioModel Adicionar(UsuarioModel usuario);
    UsuarioModel Atualizar(UsuarioModel usuario);
    bool Apagar(int id);
}