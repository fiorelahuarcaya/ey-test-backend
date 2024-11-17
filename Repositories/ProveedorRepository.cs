using ey_techical_test.Repositories;
using ey_techical_test.Utilities;
using ey_technical_test.Data;
using ey_technical_test.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace ey_technical_test.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly AppDbContext _context;

        public ProveedorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Proveedor> RegistrarProveedorAsync(Proveedor proveedor)
        {
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "usp_registrarProveedor";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@razonSocial", proveedor.RazonSocial));
                        command.Parameters.Add(new SqlParameter("@nombreComercial", proveedor.NombreComercial));
                        command.Parameters.Add(new SqlParameter("@identificacionTributaria", proveedor.IdentificacionTributaria));
                        command.Parameters.Add(new SqlParameter("@numeroTelefonico", proveedor.NumeroTelefonico));
                        command.Parameters.Add(new SqlParameter("@correoElectronico", proveedor.CorreoElectronico));
                        command.Parameters.Add(new SqlParameter("@sitioWeb", proveedor.SitioWeb));
                        command.Parameters.Add(new SqlParameter("@direccionFisica", proveedor.DireccionFisica));
                        command.Parameters.Add(new SqlParameter("@pais", proveedor.Pais));
                        command.Parameters.Add(new SqlParameter("@facturacionAnual", proveedor.FacturacionAnual));

                        // Ejecutar el comando
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Mapear los datos del proveedor recién creado
                                return new Proveedor
                                {
                                    ProveedorId = reader.GetInt32(reader.GetOrdinal("ProveedorId")),
                                    RazonSocial = reader.GetString(reader.GetOrdinal("RazonSocial")),
                                    NombreComercial = reader.GetString(reader.GetOrdinal("NombreComercial")),
                                    IdentificacionTributaria = reader.GetString(reader.GetOrdinal("IdentificacionTributaria")),
                                    NumeroTelefonico = reader.GetString(reader.GetOrdinal("NumeroTelefonico")),
                                    CorreoElectronico = reader.GetString(reader.GetOrdinal("CorreoElectronico")),
                                    SitioWeb = reader.GetString(reader.GetOrdinal("SitioWeb")),
                                    DireccionFisica = reader.GetString(reader.GetOrdinal("DireccionFisica")),
                                    Pais = reader.GetString(reader.GetOrdinal("Pais")),
                                    FacturacionAnual = reader.GetDecimal(reader.GetOrdinal("FacturacionAnual")),
                                    FechaUltimaEdicion = reader.GetDateTime(reader.GetOrdinal("FechaUltimaEdicion"))
                                };
                            }
                        }
                    }

                    throw new Exception("No se pudo registrar el proveedor.");
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al registrar el proveedor en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al registrar el proveedor.", ex);
            }
        }



        public async Task<Proveedor> ModificarProveedorAsync(Proveedor proveedor)
        {
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "usp_modificarProveedor";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.Add(new SqlParameter("@proveedorId", proveedor.ProveedorId));
                        command.Parameters.Add(new SqlParameter("@razonSocial", proveedor.RazonSocial));
                        command.Parameters.Add(new SqlParameter("@nombreComercial", proveedor.NombreComercial));
                        command.Parameters.Add(new SqlParameter("@identificacionTributaria", proveedor.IdentificacionTributaria));
                        command.Parameters.Add(new SqlParameter("@numeroTelefonico", proveedor.NumeroTelefonico));
                        command.Parameters.Add(new SqlParameter("@correoElectronico", proveedor.CorreoElectronico));
                        command.Parameters.Add(new SqlParameter("@sitioWeb", proveedor.SitioWeb));
                        command.Parameters.Add(new SqlParameter("@direccionFisica", proveedor.DireccionFisica));
                        command.Parameters.Add(new SqlParameter("@pais", proveedor.Pais));
                        command.Parameters.Add(new SqlParameter("@facturacionAnual", proveedor.FacturacionAnual));

                        // Ejecutar el comando
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Mapear el proveedor actualizado
                                return new Proveedor
                                {
                                    ProveedorId = reader.GetInt32(reader.GetOrdinal("ProveedorId")),
                                    RazonSocial = reader.GetString(reader.GetOrdinal("RazonSocial")),
                                    NombreComercial = reader.GetString(reader.GetOrdinal("NombreComercial")),
                                    IdentificacionTributaria = reader.GetString(reader.GetOrdinal("IdentificacionTributaria")),
                                    NumeroTelefonico = reader.GetString(reader.GetOrdinal("NumeroTelefonico")),
                                    CorreoElectronico = reader.GetString(reader.GetOrdinal("CorreoElectronico")),
                                    SitioWeb = reader.GetString(reader.GetOrdinal("SitioWeb")),
                                    DireccionFisica = reader.GetString(reader.GetOrdinal("DireccionFisica")),
                                    Pais = reader.GetString(reader.GetOrdinal("Pais")),
                                    FacturacionAnual = reader.GetDecimal(reader.GetOrdinal("FacturacionAnual")),
                                    FechaUltimaEdicion = reader.GetDateTime(reader.GetOrdinal("FechaUltimaEdicion"))
                                };
                            }
                        }

                        throw new Exception("No se pudo modificar el proveedor.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al ejecutar la operación de modificación en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al modificar el proveedor.", ex);
            }
        }


        public async Task<Proveedor> ObtenerProveedorAsync(int proveedorId)
        {
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "usp_obtenerProveedor";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@proveedorId", proveedorId));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Proveedor
                                {
                                    ProveedorId = reader.GetInt32(reader.GetOrdinal("ProveedorId")),
                                    RazonSocial = reader.GetString(reader.GetOrdinal("RazonSocial")),
                                    NombreComercial = reader.GetString(reader.GetOrdinal("NombreComercial")),
                                    IdentificacionTributaria = reader.GetString(reader.GetOrdinal("IdentificacionTributaria")),
                                    NumeroTelefonico = reader.GetString(reader.GetOrdinal("NumeroTelefonico")),
                                    CorreoElectronico = reader.GetString(reader.GetOrdinal("CorreoElectronico")),
                                    SitioWeb = reader.GetString(reader.GetOrdinal("SitioWeb")),
                                    DireccionFisica = reader.GetString(reader.GetOrdinal("DireccionFisica")),
                                    Pais = reader.GetString(reader.GetOrdinal("Pais")),
                                    FacturacionAnual = reader.GetDecimal(reader.GetOrdinal("FacturacionAnual")),
                                    FechaUltimaEdicion = reader.GetDateTime(reader.GetOrdinal("FechaUltimaEdicion"))
                                };
                            }
                        }
                    }

                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al ejecutar la operación de obtención en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener el proveedor.", ex);
            }
        }



        public async Task<List<Proveedor>> ListarProveedoresAsync()
        {
            try
            {
                return await _context.Proveedores.FromSqlRaw("EXEC usp_listarProveedores").ToListAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al consultar la base de datos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al listar proveedores", ex);
            }
        }

        public async Task<int> EliminarProveedorAsync(int proveedorId)
        {
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "usp_eliminarProveedor";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@proveedorId", proveedorId));

                        var outputParam = new SqlParameter("@result", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        await command.ExecuteNonQueryAsync();

                        return (int)outputParam.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al ejecutar la operación de eliminación en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al eliminar el proveedor.", ex);
            }
        }


    }
}