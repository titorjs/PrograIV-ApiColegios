using System.ComponentModel.DataAnnotations;

namespace ApiColegioPagos.Views
{
    public class ActualizacionEstudiante
    {
        [Required, MaxLength(100)]
        public string Est_nombre { get; set; }

        [Required, MaxLength(150)]
        public string Est_direccion { get; set; }

        [Required]
        public int Pension { get; set; }
    }
}
