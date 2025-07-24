using Microsoft.AspNetCore.Identity;

namespace Identities
{
    public class RolIdentity: IdentityRole<string>
    {
        public RolIdentity()
        {
            
        }

        public RolIdentity(string roleName, string codEmpresa) : base()
        {
            CodEmpresa = codEmpresa; // Asignar el código de empresa
            Name = roleName; // Asignar el nombre del rol
        }

        public string CodEmpresa { get; set; }
    }
}
