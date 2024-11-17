using ey_technical_test.Models;
using System.Text.RegularExpressions;

namespace ey_techical_test.Validators
{
    public static class ProveedorValidator
    {
        public static bool ValidarProveedor(Proveedor proveedor, out string mensajeError)
        {
            const string UrlPattern = @"^((ftp|http|https):\/\/)?(www\.)?(?!.*(ftp|http|https|www\.))[a-zA-Z0-9_-]+(\.[a-zA-Z]+)+((\/)[\w#]+)*(\/\w+\?[a-zA-Z0-9_]+=\w+(&[a-zA-Z0-9_]+=\w+)*)?$";

            // Validar Razón Social (Alfanumérico, requerido)
            if (string.IsNullOrWhiteSpace(proveedor.RazonSocial))
            {
                mensajeError = "La razón social es requerida.";
                return false;
            }

            // Validar Nombre Comercial (Alfanumérico, requerido)
            if (string.IsNullOrWhiteSpace(proveedor.NombreComercial))
            {
                mensajeError = "El nombre comercial es requerido.";
                return false;
            }

            // Validar Identificación Tributaria (Numérica, 11 dígitos)
            if (!Regex.IsMatch(proveedor.IdentificacionTributaria, @"^\d{11}$"))
            {
                mensajeError = "La identificación tributaria debe ser numérica y contener exactamente 11 dígitos.";
                return false;
            }

            // Validar Número Telefónico (Formato telefónico)
            if (!Regex.IsMatch(proveedor.NumeroTelefonico, @"^\d{9}$"))
            {
                mensajeError = "El número telefónico debe contener exactamente 9 dígitos.";
                return false;
            }

            // Validar Correo Electrónico (Formato válido)
            if (!Regex.IsMatch(proveedor.CorreoElectronico, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mensajeError = "El correo electrónico no tiene un formato válido.";
                return false;
            }

            // Validar Sitio Web (Formato URL)
            if (!Regex.IsMatch(proveedor.SitioWeb, UrlPattern))
            {
                mensajeError = "El sitio web no tiene un formato válido.";
                return false;
            }

            // Validar Dirección Física (Requerida)
            if (string.IsNullOrWhiteSpace(proveedor.DireccionFisica))
            {
                mensajeError = "La dirección física es requerida.";
                return false;
            }

            // Validar Facturación Anual (Decimal positivo)
            if (proveedor.FacturacionAnual <= 0)
            {
                mensajeError = "La facturación anual debe ser un valor mayor a 0.";
                return false;
            }

            // Validación exitosa
            mensajeError = null;
            return true;
        }
    }
}
