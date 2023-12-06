using System.ComponentModel.DataAnnotations;

namespace ApiColegioPagos.Views
{
    public class ImpagoEstudiante
    {
        public int Est_id { get; set; }
        public string Est_cedula { get; set; }
        public string Est_nombre { get; set; }
        public int cuotaActual {  get; set; }
    }
}
