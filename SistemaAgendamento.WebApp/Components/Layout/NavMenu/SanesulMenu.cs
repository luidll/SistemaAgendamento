namespace SistemaAgendamento.WebApp.Components.Layout.NavMenu
{
    public class SanesulMenu
    {
        public string? TituloMenu { get; set; }
        public string? UrlMenu { get; set; } = "#";
        public string? Icone { get; set; }

        public List<SanesulDropDownItem> ItensDoMenu { get; set; } = new();
    }

    public class SanesulDropDownItem
    {
        public string UrlItem { get; set; } = "#";
        public string? NomeItem { get; set; }
        public bool Separador { get; set; } = false;
    }
}