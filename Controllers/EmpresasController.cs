using Microsoft.AspNetCore.Authorization;
using Entities;
using Microsoft.AspNetCore.Mvc;
using backend_transport.Context;
using Microsoft.EntityFrameworkCore;

namespace backend_transport.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmpresasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Crear empresa
        [HttpPost]
        public async Task<IActionResult> CrearEmpresa(Empresas empresa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerEmpresaPorId), new { codEmpresa = empresa.codEmpresa }, empresa);
        }

        // Obtener todas las empresas
        [HttpGet]
        public async Task<IActionResult> ObtenerEmpresas()
        {
            var empresas = await _context.Empresas.ToListAsync();
            return Ok(empresas);
        }

        // Obtener empresa por codEmpresa
        [HttpGet("{codEmpresa}")]
        public async Task<IActionResult> ObtenerEmpresaPorId(string codEmpresa)
        {
            var empresa = await _context.Empresas.FindAsync(codEmpresa);
            if (empresa == null)
                return NotFound();

            return Ok(empresa);
        }

        // Actualizar empresa
        [HttpPut("{codEmpresa}")]
        public async Task<IActionResult> ActualizarEmpresa(string codEmpresa, Empresas empresaActualizada)
        {
            if (codEmpresa != empresaActualizada.codEmpresa)
                return BadRequest("El código de empresa no coincide.");

            var empresa = await _context.Empresas.FindAsync(codEmpresa);
            if (empresa == null)
                return NotFound();

            // Actualiza los campos
            empresa.NIT = empresaActualizada.NIT;
            empresa.colorPrimario = empresaActualizada.colorPrimario;
            empresa.colorSecundario = empresaActualizada.colorSecundario;
            empresa.foto_del_logo = empresaActualizada.foto_del_logo;
            empresa.celular = empresaActualizada.celular;
            empresa.correo = empresaActualizada.correo;
            // No actualices codEmpresa ni FechaCreacionUnix

            await _context.SaveChangesAsync();
            return Ok(empresa);
        }

        // Eliminar empresa
        [HttpDelete("{codEmpresa}")]
        public async Task<IActionResult> EliminarEmpresa(string codEmpresa)
        {
            var empresa = await _context.Empresas.FindAsync(codEmpresa);
            if (empresa == null)
                return NotFound();

            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
