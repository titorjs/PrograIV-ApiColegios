using ApiColegioPagos.Data;
using ApiColegioPagos.Models;
using ApiColegioPagos.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiColegioPagos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : Controller
    {
        private readonly ApiColegioPagosDbContext _context;

        public PagoController(ApiColegioPagosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            try
            {
                List<Pago> pagos = await _context.Pagos.ToListAsync();
                return Ok(pagos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("estudiante/id/{id}")]
        public async Task<IActionResult> getAllId(int id)
        {
            try
            {
                List<Pago> pagos = await _context.Pagos.ToListAsync();
                List<Pago> pagoEstudiante = pagos.FindAll(x => x.Estudiante == id);

                return Ok(pagoEstudiante);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("impagos")]
        public async Task<IActionResult> obtenerImpagos()
        {
            try
            {
                List<Estudiante> estudiantes = (_context.Estudiantes.ToList()).FindAll(x => x.Est_activo);
                List<ImpagoEstudiante> impagos = new List<ImpagoEstudiante>();
                Pago ultimoPago;
                int cuota = (await _context.Globals.FindAsync(1)).Glo_valor;

                foreach (Estudiante est in estudiantes)
                {
                    ultimoPago = (await _context.Pagos.ToListAsync())
                        .FindAll(x => x.Estudiante == est.Est_id).OrderByDescending(x => x.Pag_cuota).First();

                    if (ultimoPago.Pag_cuota < cuota)
                    {
                        impagos.Add(
                            new ImpagoEstudiante{
                                Est_id = est.Est_id,
                                Est_cedula = est.Est_cedula,
                                Est_nombre = est.Est_nombre,
                                cuotaActual = ultimoPago.Pag_cuota
                            }    
                        );
                    }
                }

                return Ok(impagos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pago/{id}")]
        public async Task<IActionResult> encontrarPago(int id)
        {
            try
            {
                Pago pagos = await _context.Pagos.FirstOrDefaultAsync(x => x.Pag_id == id);

                if (pagos == null) return BadRequest("No se ha encontrado el pago con id " + id);

                return Ok(pagos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("revertir/{id}")]
        public async Task<IActionResult> revertirUltimoPago(int id)
        {
            try
            {
                Pago ultimoPago = (await _context.Pagos.ToListAsync())
                        .FindAll(x => x.Estudiante == id).OrderByDescending(x => x.Pag_cuota).First();

                if (ultimoPago.Pag_cuota == 0) return BadRequest("No se puede eliminar el pago inicial");

                _context.Pagos.Remove(ultimoPago);
                await _context.SaveChangesAsync();

                return Ok(ultimoPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*[HttpPost("pagar/{id}")]
        public async Task<IActionResult> pagar(int id)
        {
            try
            {
                Estudiante est = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Est_id == id);

                if (est == null)
                {
                    return BadRequest("El estudiante no existe");
                }

                List<Pago> pagos = await _context.Pagos.ToListAsync();
                List<Pago> pagoEstudiante = pagos.FindAll(x => x.Estudiante == id);

                if (pagoEstudiante.IsNullOrEmpty())
                {
                    return BadRequest("No hay pagos registrados para el estudiante");
                }

                Pago ultimoPago = pagoEstudiante.OrderByDescending(x => x.Pag_cuota).First();

                Global global = await _context.Globals.FirstOrDefaultAsync(x => x.Glo_id == 1);
                int cuota = global.Glo_valor;

                if (cuota <= ultimoPago.Pag_cuota)
                {
                    return BadRequest("El estudiante ya se encuentra al día");
                }

                Pago pago = new Pago
                {
                    Estudiante = est.Est_id,
                    Pag_cuota = ultimoPago.Pag_cuota + 1,
                    Pension = est.Pension
                };

                await _context.Pagos.AddAsync(pago);
                await _context.SaveChangesAsync();

                return Ok(pago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        [HttpPost("pagar/{id}/{cantidad}")]
        public async Task<IActionResult> pagarId(int id, int cantidad)
        {
            try
            {
                Estudiante est = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Est_id == id);

                if (est == null)
                {
                    return BadRequest("El estudiante no existe");
                }

                Pago ultimoPago = ((await _context.Pagos.ToListAsync()).FindAll(x => x.Estudiante == id)).OrderByDescending(x => x.Pag_cuota).First();

                Global global = await _context.Globals.FirstOrDefaultAsync(x => x.Glo_id == 1);
                int cuota = global.Glo_valor;

                if (cuota <= ultimoPago.Pag_cuota)
                {
                    return BadRequest("El estudiante ya se encuentra al día");
                }

                if( cuota < ultimoPago.Pag_cuota + cantidad)
                {
                    return BadRequest("Las cuotas ingresadas sobrepasan el pago pendiente");
                }

                List<Pago> lista = new List<Pago>();
                Pago pago;

                for (int i = ultimoPago.Pag_cuota + 1; i <= ultimoPago.Pag_cuota + cantidad; i++)
                {
                     pago = new Pago
                    {
                        Estudiante = est.Est_id,
                        Pag_cuota = i,
                        Pension = est.Pension
                    };

                    lista.Add( pago );
                }

                await _context.Pagos.AddRangeAsync(lista);
                await _context.SaveChangesAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
