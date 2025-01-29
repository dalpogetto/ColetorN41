using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
   
    public class TransporteResponse
    {
        public int total { get; set; }
        public bool hasNext { get; set; }
        public List<Transporte> items { get; set; }
    }

    public class Transporte
    {
        public int codTransp { get; set; }
        public string nomeAbrev { get; set; }
    }

}
