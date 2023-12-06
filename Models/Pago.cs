using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiColegioPagos.Models
{
    public class Pago
    {
        [Key]
        public int Pag_id { get; set; }

        [Required, Range(1,10)]
        public int Pag_cuota { get; set;}

        [Required]
        public int Estudiante {  get; set; }

        [Required]
        public int Pension { get; set; }
    }
}
