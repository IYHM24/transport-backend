using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using Identities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_transport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController
        (
            UserManager<UserIdentity> userManager,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized("Credenciales incorrectas.");
            }
            //
            if (await _userManager.IsLockedOutAsync(user))
            {
                return BadRequest("La cuenta está bloqueada debido a múltiples intentos fallidos. Intenta nuevamente más tarde.");
            }

            // Generar el token JWT
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            //
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            //
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            //
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
            //
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register( RegisterModel model)
        {
            // Verificar si el usuario ya existe
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return BadRequest("El usuario ya existe.");
            }

            // Crear un nuevo usuario
            UserIdentity user = new UserIdentity
            {
                UserName = model.Username,
                Email = model.Email,
                CodEmpresa = model.CodEmpresa,
                RoleId = model.RolId, // Asignar el rol al usuario
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest($"Error al crear el usuario: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return Ok("Usuario creado exitosamente.");
        }
    }
}
