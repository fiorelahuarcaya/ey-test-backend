using ey_techical_test.Models;
using ey_techical_test.Utilities;
using ey_technical_test.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ey_techical_test.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> SignupAsync(Usuario usuario)
        {
            try
            {
                // Usa una nueva conexión independiente
                using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "usp_signup";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@nombres", usuario.Nombres));
                        command.Parameters.Add(new SqlParameter("@apellidos", usuario.Apellidos));
                        command.Parameters.Add(new SqlParameter("@correo", usuario.Correo));
                        command.Parameters.Add(new SqlParameter("@contrasenia", usuario.Contrasenia));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Usuario
                                {
                                    UsuarioId = reader.GetInt32(reader.GetOrdinal("usuarioId")),
                                    Nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                    Apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                    Correo = reader.GetString(reader.GetOrdinal("correo")),
                                    Contrasenia = reader.GetString(reader.GetOrdinal("contrasenia")),
                                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("fechaCreacion"))
                                };
                            }
                        }
                    }
                }

                throw new Exception("No se pudo registrar el usuario.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"UsuarioRepository::error:: {ex}");
                throw new DatabaseException("Error al registrar el usuario.", ex);
            }
        }


        public async Task<Usuario?> LoginAsync(string correo)
        {
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "usp_login";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@correo", correo));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Usuario
                                {
                                    UsuarioId = reader.GetInt32(reader.GetOrdinal("usuarioId")),
                                    Nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                    Apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                    Correo = reader.GetString(reader.GetOrdinal("correo")),
                                    Contrasenia = reader.GetString(reader.GetOrdinal("contrasenia")),
                                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("fechaCreacion"))
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error al buscar el usuario.", ex);
            }
        }

        public async Task<bool> VerificarCorreoAsync(string correo)
        {
            Console.WriteLine($"UsuarioRepository::correo:: {correo}");

            try
            {
                // Usa una nueva conexión independiente
                using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "usp_verificar_correo";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        var existeParam = new SqlParameter("@existe", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
                        command.Parameters.Add(new SqlParameter("@correo", correo));
                        command.Parameters.Add(existeParam);

                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"UsuarioRepository::existeParam.Value :: {existeParam.Value}");

                        return (int)existeParam.Value == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"UsuarioRepository::error:: {ex}");
                throw new DatabaseException("Error al verificar el correo.", ex);
            }
        }

    }

}
