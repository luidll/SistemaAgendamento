using Microsoft.AspNetCore.Components;

namespace Agendamento.DesktopApp.Components.Pages
{
    public partial class Login
    {
        [Inject]
        private NavigationManager NavManager { get; set; } = null!;

        private string login = string.Empty;
        private string senha = string.Empty;
        private string? mensagemErro;

        private void FazerLogin()
        {
            if (login == "admin" && senha == "admin")
            {
                mensagemErro = null;

                NavManager.NavigateTo("/dashboard");
            }
            else
            {
                mensagemErro = "Login ou senha inválidos.";
            }
        }
    }
}