using Agendamento.Core.Entities;
using Agendamento.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Agendamento.WebApp.Components.Layout
{
    public partial class MainLayout
    {
        [Inject]
        private AppDbContext DbContext { get; set; } = null!;

        [Inject]
        private NavigationManager NavManager { get; set; } = null!;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

        protected string UserInitials { get; set; } = "?";
        protected List<Modulo> ListaDeModulos { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdString, out var userId))
                {
                    var dadosUsuario = await DbContext.Usuarios
                        .AsNoTracking()
                        .Include(u => u.RotinasPermitidas)
                            .ThenInclude(r => r.Modulo)
                        .FirstOrDefaultAsync(u => u.Id == userId);

                    if (dadosUsuario != null)
                    {
                        UserInitials = GetInitials(dadosUsuario.NomeCompleto);

                        ListaDeModulos = dadosUsuario.RotinasPermitidas
                        .Select(r => r.Modulo)
                        .Where(m => m != null)
                        .Distinct()
                        .ToList();
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