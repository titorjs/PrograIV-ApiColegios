using ApiColegioPagos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiColegioPagos.Views
{
    public class RegistroPago
    {

        [Required, Range(1, 10)]
        public int Pag_cuota { get; set; }

        [Required]
        public int Estudiante { get; set; }

        [Required]
        public int Pension { get; set; }
    }
}
