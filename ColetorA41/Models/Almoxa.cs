using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{

    public class Almoxa
    {
        public string? CodEstabel { get; set; }
        public int CodUsuario { get; set; }
        public string? Senha { get; set; }
    }

    public class AlmoxaRequest
    {
        public int NrProcess { get; set; }
        public string? CodEstabel { get; set; }
        public int CodUsuario { get; set; }
        public string? Senha { get; set; }
    }

    public class AlmoxaResponse
    {
        public bool senhaValida { get; set; }
        public string? mensagem { get; set; }
    }


}
