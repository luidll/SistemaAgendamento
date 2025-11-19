using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agendamento.Core.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        [Required]
        public string Setor { get; set; } = string.Empty;
        public string Ramal { get; set; } = string.Empty;
        public string Login { get; set; }
        public string SenhaHash { get; set; }
        public virtual ICollection<Rotina> RotinasPermitidas { get; set; } = new List<Rotina>();
    }
}