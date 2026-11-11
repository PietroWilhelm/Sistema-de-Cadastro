using System.Text.Json;
using ControleDeContatos.Models.Usuario;

namespace ControleDeContatos.Helper;

public class Sessao : ISessao
{
    private readonly IHttpContextAccessor _httpContext;
    
    public Sessao(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }
    
    public void CriarSessaoDoUsuario(UsuarioModel usuario)
    {
        // Serializa para Json
        string usuarioJson = JsonSerializer.Serialize(usuario);
        _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", usuarioJson );
    }

    public void RemoverSessaoDoUsuario()
    {
        _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
    }

    public UsuarioModel BuscarSessaoDoUsuario()
    {
        string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

        if (string.IsNullOrEmpty(sessaoUsuario)) return null;
        
        // desserializar para Json
        return JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);
    }
}