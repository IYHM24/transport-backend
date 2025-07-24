using Identities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace backend_transport.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleConrtoller: ControllerBase
    {
        private readonly RoleManager<RolIdentity> _roleManager;

        public RoleConrtoller(RoleManager<RolIdentity> roleManager)
        {
            _roleManager = roleManager;
        }

        // Endpoint para crear un nuevo rol
        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            if (string.IsNullOrWhiteSpace(model.RoleName))
            {
                return BadRequest("El nombre del rol no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(model.CodEmpresa))
            {
                return BadRequest("El codigo de empresa del rol no puede estar vacío.");
            }


            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (roleExists)
            {
                return BadRequest("El rol ya existe.");
            }

            var result = await _roleManager.CreateAsync(new RolIdentity(model.RoleName, model.CodEmpresa));
            if (!result.Succeeded)
            {
                return BadRequest($"Error al crear el rol: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return Ok($"Rol '{model.RoleName}' creado exitosamente.");
        }

        // Endpoint para listar todos los roles
        [HttpGet("list")]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles
                .Select(r => new { Id = r.Id, Name = r.Name })
                .ToList();
            return Ok(roles);
        }
    }
}
