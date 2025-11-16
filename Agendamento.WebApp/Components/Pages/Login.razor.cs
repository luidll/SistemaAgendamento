using Agendamento.WebApp.Services;
using Microsoft.AspNetCore.Components;

namespace Agendamento.WebApp.Components.Pages
{
    public partial class Login
    {
        [Inject]
        private IAuthService AuthService { get; set; } = null!; // Nosso "motor"

        [Inject]
        private NavigationManager NavManager { get; set; } = null!;

        private string login = string.Empty;
        private string senha = string.Empty;
        private string? mensagemErro;

        private async Task TentarLogin()
        {
            try
            {
                mensagemErro = null;
                var usuario = await AuthService.LoginAsync(login, senha);

                if (usuario != null)
                {
                    NavManager.NavigateTo("/dashboard", forceLoad: true);
                }
                else
                {
                    mensagemErro = "Usuário ou senha inválidos.";
                }
            }
            catch (Exception ex)
            {
                // Falha (erro de banco, etc.)
                mensagemErro = $"Erro ao tentar logar: {ex.Message}";
            }
        }
    }
}