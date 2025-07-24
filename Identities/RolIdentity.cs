using Microsoft.AspNetCore.Identity;

namespace Identities
{
    public class RolIdentity : IdentityRole<string>
    {
        public RolIdentity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public RolIdentity(string roleName, string codEmpresa) : base()
        {
            Id = Guid.NewGuid().ToString(); // Genera el GUID automáticamente
            CodEmpresa = codEmpresa;
            Name = roleName;
        }

        public string CodEmpresa { get; set; }
    }
}
