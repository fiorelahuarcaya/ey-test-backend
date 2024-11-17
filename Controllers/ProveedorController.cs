using ey_techical_test.Services;
using ey_techical_test.Utilities;
using ey_techical_test.Validators;
using ey_technical_test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ey_technical_test.Controllers
{
    [Authorize]
    [Route("api/proveedor")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Proveedor>>> RegistrarProveedor(Proveedor proveedor)
        {
            try
            {
                // Validar proveedor
                if (!ProveedorValidator.ValidarProveedor(proveedor, out string mensajeError))
                {
                    return BadRequest(new ApiResponse<Proveedor>
                    {
                        Status = "error",
                        Message = "Error al validar los campos.",
                        Error = new
                        {
                            Code = "VALIDATION_ERROR",
                            Details = mensajeError
                        }
                    });
                }

                // Registrar proveedor
                var newProveedor = await _proveedorService.RegistrarProveedorAsync(proveedor);


                if (newProveedor != null)
                {
                    return Ok(new ApiResponse<Proveedor>
                    {
                        Status = "success",
                        Message = "Proveedor registrado correctamente.",
                        Data = newProveedor
                    });
                }

                throw new Exception("Error en el controlador al registrar el proveedor.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Status = "error",
                    Message = "Ocurrió un error al registrar el proveedor.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Details = ex.InnerException?.Message ?? ex.Message
                    }
                });
            }
        }



        [HttpPut]
        public async Task<ActionResult<ApiResponse<Proveedor>>> ModificarProveedor(Proveedor proveedor)
        {
            try
            {
                // Validar proveedor
                if (!ProveedorValidator.ValidarProveedor(proveedor, out string mensajeError))
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Status = "error",
                        Message = "Error al validar los campos.",
                        Error = new
                        {
                            Code = "VALIDATION_ERROR",
                            Details = mensajeError
                        }
                    });
                }
                var proveedorActualizado =  await _proveedorService.ModificarProveedorAsync(proveedor);

                return Ok(new ApiResponse<Proveedor>
                {
                    Status = "success",
                    //Message = $"Proveedor con ID {proveedor.ProveedorId} modificado correctamente.",
                    Message = $"Proveedor modificado correctamente.",
                    Data = proveedorActualizado
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Status = "error",
                    Message = ex.Message,
                    Error = new { Code = "NOT_FOUND", Details = ex.Message }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Status = "error",
                    Message = "Ocurrió un error al modificar el proveedor.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Details = ex.InnerException?.Message ?? ex.Message
                    }
                });
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Proveedor>>> ObtenerProveedor(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Status = "error",
                        Message = "El ID proporcionado no es válido.",
                        Error = new { Code = "INVALID_ID", Details = "El ID debe ser mayor a 0." }
                    });
                }

                var proveedor = await _proveedorService.ObtenerProveedorAsync(id);

                return Ok(new ApiResponse<Proveedor>
                {
                    Status = "success",
                    Message = "Proveedor obtenido correctamente.",
                    Data = proveedor
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Status = "error",
                    Message = ex.Message,
                    Error = new
                    {
                        Code = "NOT_FOUND",
                        Details = $"El proveedor con ID {id} no existe en la base de datos."
                    }
                });
            }
            catch (Exception ex)
            {
                var originalException = ex.InnerException;

                return StatusCode(500, new ApiResponse<string>
                {
                    Status = "error",
                    Message = "Ocurrió un error al obtener el proveedor.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Details = originalException?.Message ?? ex.Message
                    }
                });
            }
        }


        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Proveedor>>>> ListarProveedores()
        {
            try
            {
                var proveedores = await _proveedorService.ListarProveedoresAsync();
                return Ok(new ApiResponse<List<Proveedor>>
                {
                    Status = "success",
                    Message = "Proveedores listados correctamente.",
                    Data = proveedores
                });
            }
            catch (Exception ex)
            {
                // Extraer la excepción original
                var originalException = ex.InnerException;

                return StatusCode(500, new ApiResponse<string>
                {
                    Status = "error",
                    Message = "Ocurrió un error al listar los proveedores.",
                    Error = new
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Details = originalException?.Message ?? ex.Message
                    }
                });
            }
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> EliminarProveedor(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Status = "error",
                        Message = "El ID proporcionado no es válido.",
                        Error = new { Code = "INVALID_ID", Details = "El ID debe ser mayor a 0." }
                    });
                }

                await _proveedorService.EliminarProveedorAsync(id);
                return Ok(new ApiResponse<string>
                {
                    Status = "success",
                    //Message = $"Proveedor con ID {id} eliminado correctamente.",
                    Message = $"Proveedor eliminado correctamente.",
                    Data = null
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Status = "error",
                    Message = ex.Message,
                    Error = new
                    {
                        Code = "NOT_FOUND",
                        Details = $"El proveedor con ID {id} no existe en la base de datos."
                    }
                });
            }
            catch (Exception ex)
            {
                var originalException = ex.InnerException;

                return StatusCode(500, new ApiResponse<string>
                {
                    Status = "error",
                    Message = "Ocurrió un error al eliminar el proveedor.",
                    Error = new
                    {
                        Code = "DELETE_ERROR",
                        Details = originalException?.Message ?? ex.Message
                    }
                });
            }
        }

    }
}