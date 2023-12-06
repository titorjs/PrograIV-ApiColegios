using ApiColegioPagos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiColegioPagos.Data
{
    public class ApiColegioPagosDbContext: DbContext
    {
        public ApiColegioPagosDbContext()
        {
        }

        public ApiColegioPagosDbContext(DbContextOptions<ApiColegioPagosDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Estudiante> Estudiantes { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<Pension> Pensiones { get; set; }
        public virtual DbSet<Global> Globals { get; set; }
    }
}
