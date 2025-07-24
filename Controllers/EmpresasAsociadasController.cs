using Microsoft.AspNetCore.Authorization;
using backend_transport.Context;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace backend_transport.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/empresas/asociadas")]
    public class EmpresasAsociadasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmpresasAsociadasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Crear empresa asociada
        [HttpPost]
        public async Task<IActionResult> CrearEmpresaAsociada([FromBody] EmpresasAsociadas empresaAsociada)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar que la empresa principal exista
            var empresaPrincipal = await _context.Empresas.FindAsync(empresaAsociada.codEmpresa);
            if (empresaPrincipal == null)
                return BadRequest("La empresa principal no existe.");

            _context.EmpresasAsociadas.Add(empresaAsociada);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerEmpresaAsociadaPorId), new { codEmpresaAsociada = empresaAsociada.codEmpresaAsociada }, empresaAsociada);
        }

        // Obtener todas las empresas asociadas
        [HttpGet]
        public async Task<IActionResult> ObtenerEmpresasAsociadas()
        {
            var empresas = await _context.EmpresasAsociadas
                .Include(e => e.Empresas)
                .ToListAsync();
            return Ok(empresas);
        }

        // Obtener empresa asociada por codEmpresaAsociada
        [HttpGet("{codEmpresaAsociada}")]
        public async Task<IActionResult> ObtenerEmpresaAsociadaPorId(string codEmpresaAsociada)
        {
            var empresa = await _context.EmpresasAsociadas
                .Include(e => e.Empresas)
                .FirstOrDefaultAsync(e => e.codEmpresaAsociada == codEmpresaAsociada);

            if (empresa == null)
                return NotFound();

            return Ok(empresa);
        }

        // Actualizar empresa asociada
        [HttpPut("{codEmpresaAsociada}")]
        public async Task<IActionResult> ActualizarEmpresaAsociada(string codEmpresaAsociada, [FromBody] EmpresasAsociadas empresaActualizada)
        {
            if (codEmpresaAsociada != empresaActualizada.codEmpresaAsociada)
                return BadRequest("El código de empresa asociada no coincide.");

            var empresa = await _context.EmpresasAsociadas.FindAsync(codEmpresaAsociada);
            if (empresa == null)
                return NotFound();

            // Validar que la empresa principal exista
            var empresaPrincipal = await _context.Empresas.FindAsync(empresaActualizada.codEmpresa);
            if (empresaPrincipal == null)
                return BadRequest("La empresa principal no existe.");

            // Actualiza los campos
            empresa.NIT = empresaActualizada.NIT;
            empresa.foto_del_logo = empresaActualizada.foto_del_logo;
            empresa.celular = empresaActualizada.celular;
            empresa.correo = empresaActualizada.correo;
            empresa.codEmpresa = empresaActualizada.codEmpresa;

            await _context.SaveChangesAsync();
            return Ok(empresa);
        }

        // Eliminar empresa asociada
        [HttpDelete("{codEmpresaAsociada}")]
        public async Task<IActionResult> EliminarEmpresaAsociada(string codEmpresaAsociada)
        {
            var empresa = await _context.EmpresasAsociadas.FindAsync(codEmpresaAsociada);
            if (empresa == null)
                return NotFound();

            _context.EmpresasAsociadas.Remove(empresa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Obtener todas las empresas asociadas de una empresa principal
        [HttpGet("por-principal/{codEmpresa}")]
        public async Task<IActionResult> ObtenerEmpresasAsociadasPorEmpresaPrincipal(string codEmpresa)
        {
            var empresasAsociadas = await _context.EmpresasAsociadas
                .Include(e => e.Empresas)
                .Where(e => e.codEmpresa == codEmpresa)
                .ToListAsync();

            if (empresasAsociadas == null || empresasAsociadas.Count == 0)
                return NotFound($"No hay empresas asociadas para la empresa principal con código {codEmpresa}.");

            return Ok(empresasAsociadas);
        }
    }
}
