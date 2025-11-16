using System.Collections.Generic;

namespace Agendamento.Core.Entities
{
    public class Rotina
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int ModuloId { get; set; }
        public virtual Modulo Modulo { get; set; }
        public virtual ICollection<Usuario> UsuariosPermitidos { get; set; } = new List<Usuario>();
    }
}