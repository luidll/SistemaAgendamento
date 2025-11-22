using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Infrastructure.Data;
using System.Security.Claims;
using System.Linq;

namespace SistemaAgendamento.WebApp.Components.Layout.NavMenu
{
    public class SanesulNavMenuBarBase : ComponentBase
    {
        [Inject]
        private IDbContextFactory<AppDbContext> DbFactory { get; set; } = default!;

        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

        protected List<SanesulMenu> SanesulMenus { get; set; } = new();
        protected string UserName { get; set; } = "Visitante";
        protected string UserInitials { get; set; } = "?";

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                UserName = user.FindFirstValue(ClaimTypes.GivenName) ?? "Usuário";
                UserInitials = GetInitials(UserName);

                var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdString, out var userId))
                {
                    await CarregarMenusDoBanco(userId);
                }
            }
        }

        private async Task CarregarMenusDoBanco(int userId)
        {
            using var context = await DbFactory.CreateDbContextAsync();

            var usuario = await context.Usuarios
                .AsNoTracking()
                .Include(u => u.RotinasPermitidas)
                .ThenInclude(r => r.Modulo)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (usuario == null) return;

            var modulosAgrupados = usuario.RotinasPermitidas
                .Where(r => r.Modulo != null)
                .GroupBy(r => r.Modulo!)
                .OrderBy(g => g.Key.Ordem)
                .ToList();

            SanesulMenus.Clear();
            SanesulMenus.Add(new SanesulMenu
            {
                TituloMenu = "Dashboard",
                UrlMenu = "/dashboard",
                Icone = "fa fa-home"
            });

            foreach (var grupo in modulosAgrupados)
            {
                var modulo = grupo.Key;

                var novoMenu = new SanesulMenu
                {
                    TituloMenu = modulo.Nome,
                    Icone = modulo.Icon ?? "fa fa-circle",
                    ItensDoMenu = new List<SanesulDropDownItem>()
                };

                foreach (var rotina in grupo.OrderBy(r => r.Ordem))
                {


                    novoMenu.ItensDoMenu.Add(new SanesulDropDownItem
                    {
                        NomeItem = rotina.Nome,
                        UrlItem = rotina.Url
                    });
                }

                SanesulMenus.Add(novoMenu);
            }
        }

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return "?";
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1) return parts[0][0].ToString().ToUpper();
            return (parts[0][0].ToString() + parts[^1][0].ToString()).ToUpper();
        }
    }
}