using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SistemaAgendamento.WebApp.Components.Layout
{
    public partial class MainLayout
    {
        [Inject]
        private IDbContextFactory<AppDbContext> DbFactory { get; set; } = null!;

        [Inject]
        private NavigationManager NavManager { get; set; } = null!;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

        protected string UserInitials { get; set; } = "?";
        protected string UserName { get; set; } = "?";
        protected Dictionary<Modulo, List<Rotina>> ModulosComRotinas { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            using var db = DbFactory.CreateDbContext();
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdString, out var userId))
                {
                    var dadosUsuario = await db.Usuarios
                        .AsNoTracking()
                        .Include(u => u.RotinasPermitidas)
                            .ThenInclude(r => r.Modulo)
                        .FirstOrDefaultAsync(u => u.Id == userId);


                    if (dadosUsuario != null)
                    {
                        UserInitials = GetInitials(dadosUsuario.NomeCompleto);
                        UserName = dadosUsuario.NomeCompleto;

                        ModulosComRotinas = dadosUsuario.RotinasPermitidas
                            .Where(r => r.Modulo != null)
                            .OrderBy(r => r.Modulo!.Ordem)
                            .ThenBy(r => r.Ordem)
                            .GroupBy(r => r.Modulo!)
                            .ToDictionary(g => g.Key, g => g.ToList());
                    }
                }
            }
        }

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "?";

            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
                return parts[0][0].ToString().ToUpper();

            return (parts[0][0].ToString() + parts[^1][0].ToString()).ToUpper();
        }
    }
}