using ApiColegioPagos.Data;
using ApiColegioPagos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiColegioPagos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionController : Controller
    {
        private readonly ApiColegioPagosDbContext _context;

        public PensionController(ApiColegioPagosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            try
            {
                List<Pension> pensiones = await _context.Pensiones.ToListAsync();
                return Ok(pensiones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                Pension pension = await _context.Pensiones.FirstOrDefaultAsync(x => x.Pen_id == id);
                return Ok(pension);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> set([FromBody] Pension pension)
        {
            try
            {
                //verificar que los datos recibidos son correctos
                if (pension == null || pension.Pen_valor < 0 || pension.Pen_nombre == null)
                {
                    return BadRequest("Los datos ingresados no son correctos");
                }

                await _context.Pensiones.AddAsync(pension);
                await _context.SaveChangesAsync();

                return Ok(pension);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("id/{id}/{nombre}")]
        public async Task<IActionResult> updateNombre(int id, string nombre)
        {
            try
            {
                Pension pen = await _context.Pensiones.FirstOrDefaultAsync(x => x.Pen_id == id);

                //Validar que exista la pensión
                if (pen == null)
                {
                    return BadRequest("No se ha encontrado la pensión con id " + id);
                }

                //Validar que el campo a actualizar sea correcto
                if (nombre == null)
                {
                    return BadRequest("Valores ingresados para la actualización inválidos");
                }

                //Actualizar el nombre
                pen.Pen_nombre = nombre;
                _context.Update(pen);
                await _context.SaveChangesAsync();

                return Ok(pen);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Debe considerar que no se puede eliminar si hay usuarios con esa pensión
        [HttpDelete("id/{id}")]
        public async Task<IActionResult> deletePension(int id)
        {
            try
            {
                Pension pen = await _context.Pensiones.FirstOrDefaultAsync(x => x.Pen_id == id);

                //Validar que exista la pensión
                if (pen == null)
                {
                    return BadRequest("No se ha encontrado la pensión con id " + id);
                }

                Estudiante est = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Pension == id);
                Pago pago = await _context.Pagos.FirstOrDefaultAsync(x => x.Pension == id);

                //Validar que no existan registros con esta pensión
                if (est != null || pago != null)
                {
                    return BadRequest("Existen registros con esta pensión, no se puede eliminar.");
                }

                //Eliminar la pensión
                _context.Pensiones.Remove(pen);
                await _context.SaveChangesAsync();

                return Ok(pen);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
