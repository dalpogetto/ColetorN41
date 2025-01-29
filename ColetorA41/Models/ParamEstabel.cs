using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
    

    public class ParamEstabel
    {
        public string codEntrega { get; set; }
        public string serieSai { get; set; }
        public string codEstabel { get; set; }
        public string serieEntra { get; set; }
        public string nomeEstabel { get; set; }
        public string nomeAbrev => string.IsNullOrEmpty(nomeEstabel)  ? string.Empty : nomeEstabel.Split('-')[1];
        public string rpw { get; set; }
        public int codTranspEntra { get; set; }
        public int codTranspSai { get; set; }
        public string nomeTranspEnt { get; set; }
        public string nomeTranspSai { get; set; }
    }

    public class ParamEstabelecimentoRequest
    {
        public ParamEstabel paramsTela { get; set; }
    }

    public class ParamEstabelecimentoResponse
    {
        public int total { get; set; }
        public bool hasNext { get; set; }
        public List<ParamEstabel> items { get; set; }
    }

}
