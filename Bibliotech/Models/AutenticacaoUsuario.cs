using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotech.Models
{
    public class AutenticacaoUsuario
    {
        public bool IsAutenticado { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string Token { get; set; }
        public string Mensagem { get; set; }
    }
}
