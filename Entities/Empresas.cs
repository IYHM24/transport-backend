using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Empresas
    {
        [Key]
        public string codEmpresa { get; set; }

        [Required]
        [MaxLength(20)]
        public string NIT { get; set; } = string.Empty;

        [MaxLength(30)]
        public string colorPrimario { get; set; } = string.Empty;

        [MaxLength(30)]
        public string colorSecundario { get; set; } = string.Empty;

        [MaxLength(250)]
        [Url]
        public string foto_del_logo { get; set; } = string.Empty;

        [MaxLength(20)]
        public string celular { get; set; } = string.Empty;

        [MaxLength(100)]
        [EmailAddress]
        public string correo { get; set; } = string.Empty;

        public int FechaInicioMembresia { get; set; } = 0;
        public int FechaFinMembresia { get; set; } = 0;

        public Empresas()
        {
            codEmpresa = Guid.NewGuid().ToString();
        }
    }
}
