using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Application.DTOs.Requests.Desktop
{
    public class ModuloRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public int SistemaId { get; set; }
        public string Icon { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "A ordem deve ser igual ou maior que zero.")]
        public int Ordem { get; set; }
    }
}
