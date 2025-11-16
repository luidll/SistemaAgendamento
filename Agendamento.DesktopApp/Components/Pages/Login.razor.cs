using Microsoft.AspNetCore.Components;

namespace Agendamento.DesktopApp.Components.Pages
{
    public partial class Login
    {
        [Inject]
        private NavigationManager NavManager { get; set; }

        private string login;
        private string senha;
        private string mensagemErro;

        private void FazerLogin()
        {
            if (login == "admin" && senha == "admin")
            {
                mensagemErro = null;

                NavManager.NavigateTo("/sistemas");
            }
            else
            {
                mensagemErro = "Login ou senha inválidos.";
            }
        }
    }
}