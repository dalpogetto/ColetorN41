using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ColetorA41.Models
{
   
    public class notafiscal
    {
        [JsonProperty("cod-estabel")]
        public string codestabel { get; set; }
        public string serie { get; set; }

        [JsonProperty("nome-ab-cli")]
        public string nomeabcli { get; set; }

        [JsonProperty("nr-nota-fis")]
        public string nrnotafis { get; set; }

        [JsonProperty("nat-operacao")]
        public string natoperacao { get; set; }

        [JsonProperty("idi-sit")]
        public int idisit { get; set; }

        [JsonProperty("peso-liq")]
        public string pesoliq { get; set; }

        [JsonProperty("peso-bru")]
        public string pesobru { get; set; }

        [JsonProperty("qt-volume")]
        public string volume { get; set; }

        [JsonProperty("nr-process")]
        public string nrprocess { get; set; }
    }

    public class NotaFiscalEmbalagemResponse: BaseModel
    {
        public List<notafiscal> nfs { get; set; }
    }

    public class DadosEmbalagem
    {
        public string CodEstab { get; set; }
        public int CodTecnico { get; set; }
        public int NrProcess { get; set; }
    }
    public class NotaFiscalPagto
    {
        public string situacao { get; set; }

        [JsonProperty("nr-seq-fat")]
        public int nrseqfat { get; set; }

        [JsonProperty("cod-estabel")]
        public string codestabel { get; set; }

        [JsonProperty("it-codigo")]
        public string itcodigo { get; set; }
        public string serie { get; set; }

        [JsonProperty("qt-faturada")]
        public int qtfaturada { get; set; }

        [JsonProperty("nr-nota-fis")]
        public string nrnotafis { get; set; }

        [JsonProperty("nat-operacao")]
        public string natoperacao { get; set; }

       

        [JsonProperty("peso-liq")]
        public string pesoliq { get; set; }

        [JsonProperty("peso-bru")]
        public string pesobru { get; set; }

        public string volume { get; set; }
    }

    public class NotaFiscalPagtoResponse:BaseModel
    {
        public List<NotaFiscalPagto> items { get; set; }
        public string ok { get; set; }
    }

    public class NotaFiscalPagtoRequest 
    {
        public string nrProcess { get; set; }
        public string itCodigo { get; set; }
    }
}
