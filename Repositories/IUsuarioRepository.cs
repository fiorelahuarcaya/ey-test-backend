using ey_techical_test.Models;

namespace ey_techical_test.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> SignupAsync(Usuario usuario);
        Task<Usuario?> LoginAsync(string correo);
        Task<bool> VerificarCorreoAsync(string correo);
    }

}
