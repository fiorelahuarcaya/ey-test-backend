using System;

namespace ey_technical_test.Models
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }
        public required string RazonSocial { get; set; }
        public required string NombreComercial { get; set; }
        public required string IdentificacionTributaria { get; set; }  // 11 caracteres
        public required string NumeroTelefonico { get; set; }
        public required string CorreoElectronico { get; set; }
        public required string SitioWeb { get; set; }
        public required string DireccionFisica { get; set; }
        public required string Pais { get; set; }
        public decimal FacturacionAnual { get; set; }
        public DateTime FechaUltimaEdicion { get; set; }
    }
}
