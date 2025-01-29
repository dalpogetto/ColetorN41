using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{

    public class EncResponse
    {
        public int total { get; set; }
        public bool hasNext { get; set; }
        public List<Enc> items { get; set; }
    }
    public class Enc
    {
        public string numEnc { get; set; }
        public int numOS { get; set; }
        public string chamado { get; set; }
        public string itCodigo { get; set; }
        public string itDescricao { get; set; }
        public string mensagem { get; set; }
        public string flag { get; set; }

    }
}
