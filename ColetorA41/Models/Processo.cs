using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
    
    public class ProcessoResponse
    {
        public int statusProcesso { get; set; }
        public int nrProcesso { get; set; }
        public int tempoProcesso { get; set; }
        public string? situacaoProcesso { get; set; }
    }


    public class Processo
    {
        public int statusProcesso { get; set; }
        public int nrProcesso { get; set; }
        public int tempoProcesso { get; set; }
        public string? situacaoProcesso { get; set; }
    }

}
