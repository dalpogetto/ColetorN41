using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
   
public class OrdemServicoResponse
    {
       // public List<Tela> tela { get; set; }
       // public List<OrdemServico> ordens { get; set; }

        public string cRowId { get; set; }
    }

    public class Tela
    {
        public string TOTAL { get; set; }
        public string os { get; set; }
        public string usada { get; set; }
        public string branco { get; set; }
    }

    public class OrdemServico
    {
        public string situacao { get; set; }
        public string codfilial { get; set; }
        public string flag { get; set; }
        public int codemitente { get; set; }
        public string crowId { get; set; }
        public string rprordemservico { get; set; }
        public float Chamado { get; set; }
        public int NumOS { get; set; }
        public string Serie { get; set; }
    }

}
