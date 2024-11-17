namespace ey_techical_test.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public required string Correo { get; set; }
        public required string Contrasenia { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

}
