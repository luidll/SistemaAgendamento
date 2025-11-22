using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Application.DTOs.Requests.Desktop
{
    public class RotinaRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public int ModuloId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "A ordem deve ser igual ou maior que zero.")]
        public int Ordem { get; set; }
        public string Url { get; set; } = string.Empty;

    }
}
