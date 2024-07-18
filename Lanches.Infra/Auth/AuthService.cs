using Lanches.Core.Entities;
using Lanches.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lanches.Infraestructure.Auth
{
    public class AuthService : ControllerBase, IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Autentica o usuário no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Login
        ///     {
        ///      "password": "password",
        ///      "email": "email@email.com"
        ///     }
        ///
        /// </remarks>
        /// <param name="Login">Dados do login.</param>
        /// <response code="201">Retorna o token de acesso</response>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
                return BadRequest();


            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded)
                return BadRequest();

            var userRoles = await _userManager.GetRolesAsync(user);

            var key = Encoding.ASCII.GetBytes(_configuration["JwtBearerTokenSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials =
               new SigningCredentials(
                   new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["JwtBearerTokenSettings:Audience"],
                Issuer = _configuration["JwtBearerTokenSettings:Issuer"],
                Expires = DateTime.UtcNow.AddMinutes(300),
                Subject = new ClaimsIdentity(await GetClaims(user, userRoles)),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);


            var stringToken = tokenHandler.WriteToken(token);
            return Ok(stringToken);
        }

        /// <summary>
        /// Cria um novo usuário no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Register
        ///     {
        ///      "password": "password",
        ///      "email": "email@email.com"
        ///     }
        ///
        /// </remarks>
        /// <param name="Login">Dados do login.</param>
        /// <response code="201">Retorna o token de acesso</response>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] Login login)
        {

            var userName = login.Email.Split('@')[0];
            User user = new User(userName, login.Email);

            var result = await _userManager.CreateAsync(user, login.Password);
            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, "Customer");

                var role = await _roleManager.FindByNameAsync("Customer");
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                var claimsToAdd = roleClaims.Select(c => new Claim(c.Type, c.Value));
                await _userManager.AddClaimsAsync(user, claimsToAdd);

                return Ok(new { UserId = user.Id, RegistrationResult = result });
            }

            return BadRequest();
        }

        /// <summary>
        /// Altera a role de um usuário para a role selecionada
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Register
        ///     {
        ///      "userId": "1",
        ///      "newRole": "role"
        ///     }
        ///
        /// </remarks>
        /// <param name="Login">Dados do login.</param>
        /// <response code="201">Retorna a confirmação da mudança</response>
        [HttpPost("ChangeUserRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeUserRoleModel model)
        {
            var userManager = _userManager;

            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(model.NewRole))
            {
                return BadRequest("Nova role é obrigatória.");
            }

            var currentRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, currentRoles);

            await userManager.AddToRoleAsync(user, model.NewRole);

            return Ok("Role alterada com sucesso.");
        }


        private async Task<IEnumerable<Claim>> GetClaims(User user, IList<string> userRoles)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
    };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claims.AddRange(await GetPermissionsAsync(userRoles));
            return claims;
        }

        private async Task<IEnumerable<Claim>> GetPermissionsAsync(IList<string> userRoles)
        {
            var permissions = new List<Claim>();

            foreach (var role in userRoles)
            {
                var roleEntity = await _roleManager.FindByNameAsync(role);
                if (roleEntity != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(roleEntity);

                    permissions.AddRange(roleClaims.Where(c => c.Type == "Permission"));
                }
            }

            return permissions;
        }
    }
}
