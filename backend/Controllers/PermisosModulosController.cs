using Microsoft.AspNetCore.Mvc;
using backend_transport.Context;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace backend_transport.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/modulos")]
    public class PermisosModulosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PermisosModulosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("permiso")]
        public async Task<IActionResult> CrearPermiso(Permisos permiso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar que el módulo y el rol existan
            var moduloExiste = await _context.Modulos.FindAsync(permiso.ModuloId);
            var rolExiste = await _context.Roles.FindAsync(permiso.RolId);

            if (moduloExiste == null)
                return NotFound($"No existe el módulo con Id {permiso.ModuloId}");

            if (rolExiste == null)
                return NotFound($"No existe el rol con Id {permiso.RolId}");

            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CrearPermiso), new { id = permiso.Id }, permiso);
        }

        [HttpGet("permitidos/{idRol}")]
        public async Task<IActionResult> ObtenerModulosPermitidosPorRol(string idRol)
        {
            // Busca los permisos donde el rol coincide y tiene permiso de "Ver"
            var modulosPermitidos = await _context.Permisos
                .Where(p => p.RolId == idRol && p.Ver)
                .Select(p => p.Modulo)
                .ToListAsync();

            if (modulosPermitidos == null || !modulosPermitidos.Any())
                return NotFound($"No hay módulos permitidos para el rol con Id {idRol}");

            return Ok(modulosPermitidos);
        }

    }
}
