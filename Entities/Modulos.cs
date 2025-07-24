using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Modulos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? Descripcion { get; set; }

        // Si es módulo principal, id_padre = 0; si es submódulo, id_padre = Id del módulo padre
        public int Id_Padre { get; set; } = 0;
    }
}
