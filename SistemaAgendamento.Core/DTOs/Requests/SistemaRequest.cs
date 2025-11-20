using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAgendamento.Application.DTOs.Requests
{
    public class SistemaRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public bool Ativo { get; set; }
    }
}
