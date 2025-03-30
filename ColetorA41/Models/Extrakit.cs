using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
    
    public class ExtrakitRequest
    {
        public string? CodEstab { get; set; }
        public int CodTecnico { get; set; }
        public int NrProcess { get; set; }
    }

   
    public class ExtrakitResponse
    {
        public List<Extrakit> items { get; set; }
    }

    public class Extrakit
    {
        public string? tipo { get; set; }
        public string? itPrincipal { get; set; }
        public string? itCodigo { get; set; }
        public string? serieDocto { get; set; }
        public string? nroDocto { get; set; }
        public int numOS { get; set; }
        public int qtDisp { get; set; }
        public string? cRowId { get; set; }
        public int qtSaldo { get; set; }
        public int qtRuim { get; set; }
        public string? descItem { get; set; }
        public bool calculado { get; set; }
        public int qtKit { get; set; }
    }



}
