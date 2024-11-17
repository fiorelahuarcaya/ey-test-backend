using ey_techical_test.Dto;
using ey_techical_test.Models;
using ey_techical_test.Services;
using ey_techical_test.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace ey_techical_test.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly JwtHelper _jwtHelper;

        public AuthController(IUsuarioService usuarioService, JwtHelper jwtHelper)
        {
            _usuarioService = usuarioService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<ApiResponse<Usuario>>> Signup(Usuario usuario)
        {
            try
            {
                var newUsuario = await _usuarioService.SignupAsync(usuario);
                return Ok(new ApiResponse<Usuario>
                {
                    Status = "success",
                    Message = "Usuario registrado correctamente.",
                    Data = newUsuario
                });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Status = "error",
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Status = "error",
                    Message = "Ocurrió un error al registrar el usuario.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Details = ex.InnerException?.Message ?? ex.Message
                    }
                });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] UsuarioLoginDto usuarioLogin)
        {
            try
            {
                var usuario = await _usuarioService.LoginAsync(usuarioLogin.Correo, usuarioLogin.Contrasenia);

                // Generar el token usando JwtHelper
                var token = _jwtHelper.GenerarToken(usuario);

                return Ok(new ApiResponse<string>
                {
                    Status = "success",
                    Message = "Inicio de sesión exitoso.",
                    Data = token
                });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Status = "error",
                    Message = ex.Message
                });
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<string>>> Login()
        {
            return Ok(new ApiResponse<string>
            {
                Status = "success",
                Message = "Prueba Exitosa.",
                Data = "ON"
            });
        }

}
