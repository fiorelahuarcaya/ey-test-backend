using ey_techical_test.Repositories;
using ey_techical_test.Utilities;
using ey_techical_test.Validators;
using ey_technical_test.Models;
using System.ComponentModel.DataAnnotations;

namespace ey_techical_test.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<Proveedor> RegistrarProveedorAsync(Proveedor proveedor)
        {
            try
            {
                var newProveedor = await _proveedorRepository.RegistrarProveedorAsync(proveedor);

                return newProveedor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de servicio al registrar el proveedor.", ex);
            }
        }


        public async Task<Proveedor> ModificarProveedorAsync(Proveedor proveedor)
        {
            try
            {
                var result = await _proveedorRepository.ModificarProveedorAsync(proveedor);

                return result;
            }

            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de servicio al modificar el proveedor.", ex);
            }
        }


        public async Task<Proveedor> ObtenerProveedorAsync(int proveedorId)
        {
            try
            {
                var proveedor = await _proveedorRepository.ObtenerProveedorAsync(proveedorId);

                if (proveedor == null)
                {
                    throw new NotFoundException($"No se encontró un proveedor con ID {proveedorId}.");
                }

                return proveedor;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de servicio al obtener el proveedor.", ex);
            }
        }


        public async Task<List<Proveedor>> ListarProveedoresAsync()
        {
            try
            {
                return await _proveedorRepository.ListarProveedoresAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de servicio al listar proveedores", ex);
            }
        }


        public async Task<int> EliminarProveedorAsync(int proveedorId)
        {
            try
            {
                var result = await _proveedorRepository.EliminarProveedorAsync(proveedorId);
                if (result == 0)
                {
                    throw new NotFoundException($"No se encontró un proveedor con ID {proveedorId} para eliminar.");
                }

                return result;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de servicio al eliminar el proveedor.", ex);
            }
        }
    }
}