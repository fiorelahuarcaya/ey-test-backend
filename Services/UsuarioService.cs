using ey_techical_test.Models;
using ey_techical_test.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ey_techical_test.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> SignupAsync(Usuario usuario)
        {
            if (await _usuarioRepository.VerificarCorreoAsync(usuario.Correo))
                throw new ValidationException("El correo ya está registrado.");

            
            Console.WriteLine($"UsuarioService::contrasenia:: {usuario.Contrasenia}");

            usuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia);
            Console.WriteLine($"UsuarioService::Contrasenia despues es:: {usuario.Contrasenia}");

            return await _usuarioRepository.SignupAsync(usuario);
        }

        public async Task<Usuario?> LoginAsync(string correo, string contrasenia)
        {
            var usuario = await _usuarioRepository.LoginAsync(correo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(contrasenia, usuario.Contrasenia))
                throw new ValidationException("Correo o contraseña incorrectos.");

            return usuario;
        }

        public async Task<bool> VerificarCorreoAsync(string correo)
        {
            return await _usuarioRepository.VerificarCorreoAsync(correo);
        }
    }

}
