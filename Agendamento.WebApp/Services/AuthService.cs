using Agendamento.Core.Entities;
using Agendamento.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Agendamento.WebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Usuario?> LoginAsync(string login, string senha)
        {
            var usuario = await _dbContext.Usuarios
                                .Include(u => u.RotinasPermitidas)
                                .FirstOrDefaultAsync(u => u.Login == login);

            if (usuario == null)
            {
                return null;
            }

            bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash);

            if (senhaValida)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Login),
                    new Claim(ClaimTypes.GivenName, usuario.NomeCompleto),
                };

                foreach (var rotina in usuario.RotinasPermitidas)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rotina.Nome));
                }

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                };

                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);


                return usuario;
            }

            return null;
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}