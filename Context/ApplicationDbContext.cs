using Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entities; // Asegúrate de importar el namespace donde está Modulos


namespace backend_transport.Context
{
    public class ApplicationDbContext: IdentityDbContext<UserIdentity, RolIdentity, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet para la entidad Modulos
        public DbSet<Modulos> Modulos { get; set; }

        //DbSet para la entidad Permisos
        public DbSet<Permisos> Permisos { get; set; }

        // DbSet para la entidad Empresas
        public DbSet<Empresas> Empresas { get; set; }

        // DbSet para la entidad Empresas asociadas
        public DbSet<EmpresasAsociadas> EmpresasAsociadas { get; set; }
    }
}
