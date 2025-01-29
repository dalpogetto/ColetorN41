using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
    public class EntregaResponse
    {
        public List<Entrega> listaEntrega { get; set; }
    }

    public class Entrega
    {
        public string codEntrega { get; set; }
        public string nomeAbrev { get; set; }

        public string identific => $"{codEntrega} - {nomeAbrev}";

        public int nrProcesso {get;set;}
    }
}
