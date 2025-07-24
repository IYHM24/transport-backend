using Microsoft.AspNetCore.Authorization;
using Entities;
using Microsoft.AspNetCore.Mvc;
using backend_transport.Context;
using Microsoft.EntityFrameworkCore;
using Model;

namespace backend_transport.Controllers
{
    //[Authorize]
    [ApiController]
    [Authorize(Roles = "Owner")]
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
        public IActionResult CrearEmpresa(CrearEmpresaModel dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Empresas empresa = new Empresas
            {
                NIT = dto.NIT,
                colorPrimario = dto.colorPrimario,
                colorSecundario = dto.colorSecundario,
                foto_del_logo = dto.foto_del_logo,
                celular = dto.celular,
                correo = dto.correo,
                FechaInicioMembresia = dto.FechaInicioMembresia,
                FechaFinMembresia = dto.FechaFinMembresia
                // codEmpresa se genera automáticamente
            };

            _context.Empresas.Add(empresa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(CrearEmpresa), new { id = empresa.codEmpresa }, empresa);
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
        public async Task<IActionResult> ActualizarEmpresa(string codEmpresa, ActualizarEmpresaModel dto)
        {
            var empresa = await _context.Empresas.FindAsync(codEmpresa);
            if (empresa == null)
                return NotFound();

            if (dto.NIT != null)
                empresa.NIT = dto.NIT;

            if (dto.colorPrimario != null)
                empresa.colorPrimario = dto.colorPrimario;

            if (dto.colorSecundario != null)
                empresa.colorSecundario = dto.colorSecundario;

            if (dto.foto_del_logo != null)
                empresa.foto_del_logo = dto.foto_del_logo;

            if (dto.celular != null)
                empresa.celular = dto.celular;

            if (dto.correo != null)
                empresa.correo = dto.correo;

            if (dto.FechaInicioMembresia.HasValue)
                empresa.FechaInicioMembresia = dto.FechaInicioMembresia.Value;

            if (dto.FechaFinMembresia.HasValue)
                empresa.FechaFinMembresia = dto.FechaFinMembresia.Value;

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
