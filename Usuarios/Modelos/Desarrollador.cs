using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Usuarios.Modelos
{
    // Esta clase representa el Modelo y la tabla en la base de datos
    public class Desarrollador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Username { get; set; }
        public string? Rol { get; set; }
        public string? JuegoAsignado { get; set; }
        public int RobuxGenerados { get; set; }
        public string? Estatus { get; set; }
    }
}