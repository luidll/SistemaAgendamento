using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace SistemaAgendamento.WebApp.Components.Layout.NavMenu
{
    public class SanesulMenuItemBase : ComponentBase, IDisposable
    {
        [Inject]
        NavigationManager _navigationManager { get; set; } = default!;

        [Parameter]
        public Action? Evento { get; set; }

        [Parameter]
        public SanesulMenu SanesulMenu { get; set; } = new SanesulMenu();

        public bool MenuAtivo { get; set; } = false;

        protected override void OnInitialized()
        {
            _navigationManager.LocationChanged += HandleLocationChanged;
            UpdateActiveStates();
        }

        private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            UpdateActiveStates();
            StateHasChanged();
        }

        private void UpdateActiveStates()
        {
            string currentUri = new Uri(_navigationManager.Uri).AbsolutePath;
            MenuAtivo = SanesulMenu.ItensDoMenu.Exists(url =>
                currentUri.Equals(url.UrlItem, StringComparison.OrdinalIgnoreCase) ||
                (url.UrlItem != "/" && currentUri.StartsWith(url.UrlItem, StringComparison.OrdinalIgnoreCase)));
        }

        public void Dispose()
        {
            _navigationManager.LocationChanged -= HandleLocationChanged;
        }
    }
}