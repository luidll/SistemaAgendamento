using System.Collections.Generic;

namespace Agendamento.Core.Entities
{
    public class Modulo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public int SistemaId { get; set; }

        public virtual Sistema Sistema { get; set; }

        public virtual ICollection<Rotina> Rotinas { get; set; } = new List<Rotina>();
    }
}