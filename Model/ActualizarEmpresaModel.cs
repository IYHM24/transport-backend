using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ActualizarEmpresaModel
    {
        [MaxLength(20)]
        public string? NIT { get; set; }

        [MaxLength(30)]
        public string? colorPrimario { get; set; }

        [MaxLength(30)]
        public string? colorSecundario { get; set; }

        [MaxLength(250)]
        [Url]
        public string? foto_del_logo { get; set; }

        [MaxLength(20)]
        public string? celular { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string? correo { get; set; }

        public int? FechaInicioMembresia { get; set; }
        public int? FechaFinMembresia { get; set; }
    }
}
