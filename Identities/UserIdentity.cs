using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identities
{
    public class UserIdentity: IdentityUser<string>
    {
        public string? CodEmpresa { get; set; }
        
        // Relación: Un usuario pertenece a un rol
        [ForeignKey("RoleId")]
        public string? RoleId { get; set; }
        public RolIdentity? Role { get; set; }
    }
}
