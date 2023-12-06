using ApiColegioPagos.Data;
using ApiColegioPagos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiColegioPagos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly ApiColegioPagosDbContext _context;

        public AdminController(ApiColegioPagosDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin adminValidar)
        {
            try
            {
                Admin admin = await _context.Admins.FirstOrDefaultAsync( x => x.Id == adminValidar.Id);

                if(admin == null)
                {
                    return BadRequest("No existe el administrador");
                }

                if(admin.contrasenia == adminValidar.contrasenia)
                {
                    return Ok(true);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> CambioContrasenia(Admin adminCambio, String nuevaContrasenia)
        {
            try
            {
                if(nuevaContrasenia.Length < 8)
                {
                    return BadRequest("La contraseña debe tener almenos 8 carateres.");
                }

                Admin admin = await _context.Admins.FirstOrDefaultAsync(x => x.Id == adminCambio.Id);

                if( admin == null)
                {
                    return BadRequest("No existe tal administrador");
                }

                if (admin.contrasenia == adminCambio.contrasenia)
                {
                    admin.contrasenia = nuevaContrasenia;
                    _context.Admins.Update(admin);
                    _context.SaveChanges();
                    return Ok(true);
                }

                return BadRequest("La contraseña actual no coincide");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
