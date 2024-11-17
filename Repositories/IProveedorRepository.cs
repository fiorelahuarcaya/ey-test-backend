using ey_technical_test.Models;

namespace ey_techical_test.Repositories
{
    public interface IProveedorRepository
    {
        Task<Proveedor> RegistrarProveedorAsync(Proveedor proveedor);
        Task<Proveedor> ModificarProveedorAsync(Proveedor proveedor);
        Task<Proveedor> ObtenerProveedorAsync(int proveedorId);
        Task<List<Proveedor>> ListarProveedoresAsync();
        Task<int> EliminarProveedorAsync(int proveedorId);
    }
}