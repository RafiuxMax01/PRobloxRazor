using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Usuarios.Modelos
{
    public class Desarrollador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Username { get; set; }
        public string? Rol { get; set; }
        public string? JuegoAsignado { get; set; }
        public int RobuxGenerados { get; set; }

        // ESTE ES EL CAMPO QUE FALTA O ESTÁ MAL ESCRITO
        public string? Estatus { get; set; }
    }
}