using System.Collections.Generic;

namespace Agendamento.Core.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string SenhaHash { get; set; }
        public virtual ICollection<Rotina> RotinasPermitidas { get; set; } = new List<Rotina>();
    }
}