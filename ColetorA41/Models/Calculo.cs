using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
    public class Resumo
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }

        public int quantidade { get; set; }

        public List<Resumo> resumos { get; set; }

    }

    public class Ficha
    {
        public int tipo { get; set; }
        public int qtSemSaldo { get; set; }
        public int qtGeral { get; set; }
        public int qtSoEntra { get; set; }
        public int qtPagto { get; set; }
        public int qtExtrakit { get; set; }
        public int qtReno { get; set; }


    }

    public class PrepararCalculoRequest
    {
        public string CodEstab { get; set; }
        public int CodTecnico { get; set; }
        public int NrProcess { get; set; }
        public List<Extrakit> Extrakit { get; set; }
    }

    public class PrepararCalculoResponse
    {
        public List<Ficha> items { get; set; }
    }

    public class Semsaldo
    {
        public int qtPagar { get; set; }
        public int qtSaldo { get; set; }
        public string kit { get; set; }
        public string itCodigo { get; set; }
        public string descItem { get; set; }
        public int qtMascara { get; set; }
    }

    public class Calculo
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


    public class AprovarCalculoRequest
    {
        public string CodEstab { get; set; }
        public int CodTecnico { get; set; }
        public int NrProcess { get; set; }
    }



}
