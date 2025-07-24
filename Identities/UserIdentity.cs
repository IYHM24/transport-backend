using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identities
{
    public class UserIdentity: IdentityUser<string>
    {
        public string? CodEmpresa { get; set; }
        
        public UserIdentity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
