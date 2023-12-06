using System.ComponentModel.DataAnnotations;

namespace ApiColegioPagos.Views
{
    public class EstudianteVista
    {
        [Key]
        public int Est_id { get; set; }

        [Required, MaxLength(10)]
        public string Est_cedula { get; set; }

        [Required, MaxLength(100)]
        public string Est_nombre { get; set; }

        [Required, MaxLength(150)]
        public string Est_direccion { get; set; }

        [Required]
        public bool Est_activo { get; set; }

        [Required]
        public int Pension { get; set; }

        public EstudianteVista(EstudianteVista estudiante) { 
            Est_id = estudiante.Est_id;
            Est_direccion = estudiante.Est_direccion;
            Est_cedula = estudiante.Est_cedula;
            Est_activo = estudiante.Est_activo;
            Est_nombre = estudiante.Est_nombre;
            Pension = estudiante.Pension;
        }
    }
}
