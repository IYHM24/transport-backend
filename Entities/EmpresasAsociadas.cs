using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    public class EmpresasAsociadas
    {
        [Key]
        public string codEmpresaAsociada { get; set; }

        [Required]
        [MaxLength(20)]
        public string NIT { get; set; } = string.Empty;


        [MaxLength(250)]
        [Url]
        public string foto_del_logo { get; set; } = string.Empty;

        [MaxLength(20)]
        public string celular { get; set; } = string.Empty;

        [MaxLength(100)]
        [EmailAddress]
        public string correo { get; set; } = string.Empty;

        // Relación con el rol
        [ForeignKey("Empresas")]
        public required string codEmpresa { get; set; }
        public Empresas Empresas { get; set; } = null!;

        public EmpresasAsociadas()
        {
            codEmpresaAsociada = Guid.NewGuid().ToString();
        }
    }
}
