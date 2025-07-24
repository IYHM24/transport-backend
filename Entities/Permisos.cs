using Identities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Permisos
    {
        [Key]
        public int Id { get; set; }

        // Relación con el módulo
        [ForeignKey("Modulo")]
        public int ModuloId { get; set; }
        public Modulos Modulo { get; set; } = null!;

        // Relación con el rol
        [ForeignKey("Rol")]
        public required string RolId { get; set; } = string.Empty;
        public RolIdentity Rol { get; set; } = null!;

        // Permisos
        public bool Ver { get; set; } = false;
        public bool Agregar { get; set; } = false;
        public bool Eliminar { get; set; } = false;
        public bool Actualizar { get; set; } = false;
    }
}
