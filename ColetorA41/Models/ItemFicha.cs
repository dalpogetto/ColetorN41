using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
    public class ItemFicha:Calculo
    {
        
    }

    public class ItemFichaResponse
    {
        public int total { get; set; }
        public bool hasNext { get; set; }
        public List<ItemFicha> items { get; set; }
    }
}
