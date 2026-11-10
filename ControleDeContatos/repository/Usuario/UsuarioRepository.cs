using ControleDeContatos.Data;
using ControleDeContatos.Models.Usuario;

namespace ControleDeContatos.repository;

public class UsuarioRepository : IUsuarioRepository
{
    //somente Leitura
    private readonly BancoContext _bancoContext;

    public UsuarioRepository(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }


    public UsuarioModel ListarPorId(int id)
    {
        return _bancoContext.Usuarios.FirstOrDefault(u => u.Id == id);
    }

    public List<UsuarioModel> BuscarTodos()
    {
        // Ajuste na consulta para ordenar os Usuarios pelo Id em ordem crescente
        var usuarios = _bancoContext.Usuarios.OrderBy(u => u.Id).ToList();
        return usuarios;
    }

    public UsuarioModel Adicionar(UsuarioModel usuario)
    {
        usuario.DataCadastro = DateTime.UtcNow;
        // Adiciona o Usuario no banco de dados
        _bancoContext.Usuarios.Add(usuario);
        // Salva as alterações no banco de dados
        _bancoContext.SaveChanges();
        return usuario;
    }

    public UsuarioModel Atualizar(UsuarioModel usuario)
    {
        UsuarioModel usuarioDB = ListarPorId(usuario.Id);
        if (usuarioDB == null) throw new Exception("Houve um erro na atualização do usuário");
        
        usuarioDB.Nome = usuario.Nome;
        usuarioDB.Login = usuario.Login;
        usuarioDB.Email = usuario.Email;
        usuarioDB.Perfil = usuario.Perfil;
        usuarioDB.DataAlteracao = DateTime.UtcNow;
        _bancoContext.Usuarios.Update(usuarioDB);
        _bancoContext.SaveChanges();
        return usuarioDB;
    }
    
    public bool Apagar(int id)
    {
        UsuarioModel usuarioDB = ListarPorId(id);
        if (usuarioDB == null) throw new Exception("Houve um erro na deleção do usuário");

        _bancoContext.Usuarios.Remove(usuarioDB);
        _bancoContext.SaveChanges();
        return true;
    }
}