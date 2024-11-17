using ey_techical_test.Models;

namespace ey_techical_test.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> SignupAsync(Usuario usuario);
        Task<Usuario?> LoginAsync(string correo, string contrasenia);
        Task<bool> VerificarCorreoAsync(string correo);
    }

}
