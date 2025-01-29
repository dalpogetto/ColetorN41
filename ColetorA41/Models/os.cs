using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{

    public class OSResponse
    {
        public List<SubTotais> tela { get; set; }
        public List<OS> ordens { get; set; }
    }

    public class SubTotais
    {
        public string TOTAL { get; set; }
        public string os { get; set; }
        public string usada { get; set; }
        public string branco { get; set; }
    }

    public class OS
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
