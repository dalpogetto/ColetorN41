using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
    public class ItemFicha
    {
        public bool soEntrada { get; set; }
        public string tipo { get; set; }
        public int qtPagar { get; set; }
        public string itPrincipal { get; set; }
        public bool temPagto { get; set; }
        public int qtRenovar { get; set; }
        public string itCodigo { get; set; }
        public int numOS { get; set; }
        public int qtMascara { get; set; }
        public string material { get; set; }
        public int seqOrdem { get; set; }
        public string cRowId { get; set; }
        public int qtRuim { get; set; }
        public int qtSaldo { get; set; }
        public string localiz { get; set; }
        public string kit { get; set; }
        public string codLocaliza { get; set; }
        public int qtTerc { get; set; }
        public int qtExtrakit { get; set; }
        public string descItem { get; set; }
        public int qtDAT { get; set; }
        public int id { get; set; }
        public string notaAnt { get; set; }
    }

    public class ItemFichaResponse
    {
        public int total { get; set; }
        public bool hasNext { get; set; }
        public List<ItemFicha> items { get; set; }
    }
}
