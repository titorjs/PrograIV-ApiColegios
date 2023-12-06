using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiColegioPagos.Models
{
    public class Estudiante
    {
        [Key]
        public int Est_id { get; set; }

        [MinLength(8)]
        public string contrasenia { get; set; } = "Estudiantes2023";

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

        public static Boolean validarCedula(String cedula)
        {
            var len = cedula.Length;
            if (len != 10) return false;

            try
            {
                int sumaI = 0;
                int sumaP = 0;
                int aux = 0;
                int verificador = int.Parse(cedula.Substring(9, 1));

                for(int i = 0; i < len - 1; i+= 2)
                {
                    aux = int.Parse(cedula.Substring(i, 1));

                    aux *= 2;

                    if(aux > 9) aux -= 9;

                    sumaI += aux;
                }
                for (int i = 1; i < len - 1; i += 2)
                {
                    aux = int.Parse(cedula.Substring(i, 1));
                    sumaP += aux;
                }

                int modulo = 10 - (sumaI + sumaP) % 10;


                return modulo == verificador;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
