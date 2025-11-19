using System.Collections.Generic;
using System.Reflection;

namespace SistemaAgendamento.Domain.Entities
{
    public class Sistema
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<Modulo> Modulos { get; set; } = new List<Modulo>();
    }
}