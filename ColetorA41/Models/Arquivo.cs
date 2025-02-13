using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
    public class ArquivoResumo
    {
        public string Arquivo { get; set; }
        public int NumPedExec { get; set; }
    }

    public class EncerrarResponse
    {
        public string ok { get; set; }
    }
}
