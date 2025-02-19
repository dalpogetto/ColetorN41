using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ColetorA41.Models
{
    public class ReparoItem
    {
        
        [JsonProperty("datarec-lab")]
        public object datareclab { get; set; }
        public string NFSerRem { get; set; }
        public int lote { get; set; }

        [JsonProperty("fl-origem")]
        public string florigem { get; set; }
        public string LabAnt { get; set; }
        public double preco { get; set; }

        [JsonProperty("usuario-recebe-lab")]
        public string usuariorecebelab { get; set; }
        public string NomSirOrg { get; set; }
        public string nfretfe { get; set; }

        [JsonProperty("NumSerie-ant")]
        public string NumSerieant { get; set; }
        public object datarecdrf { get; set; }
        public string SerDevol { get; set; }
        public int NumSerie { get; set; }

        [JsonProperty("num-serie-it")]
        public string numserieit { get; set; }
        public string DataRecebe { get; set; }

        [JsonProperty("vl-orcado")]
        public double vlorcado { get; set; }
        public int CodCliDest { get; set; }
        public string Flag2 { get; set; }
        public object datainirep { get; set; }

        [JsonProperty("Cod-for-ext")]
        public int Codforext { get; set; }
        public object RRAnt { get; set; }
        public List<string> LaudoTecnico { get; set; }
        public int RequisSaida { get; set; }
        public string serretfe { get; set; }
        public string Abertura { get; set; }
        public string Laboratorio { get; set; }

        [JsonProperty("desc-item-equiv")]
        public string descitemequiv { get; set; }

        [JsonProperty("nat-operacao")]
        public string natoperacao { get; set; }

        [JsonProperty("nr-enc")]
        public int nrenc { get; set; }

        [JsonProperty("NumSerie-atu")]
        public string NumSerieatu { get; set; }
        public object dtretcon { get; set; }

        [JsonProperty("qt-equiv")]
        public double qtequiv { get; set; }
        public int NumOS { get; set; }

        [JsonProperty("nr-process")]
        public int nrprocess { get; set; }
        public object DataDevol { get; set; }
        public object FilAnt { get; set; }
        public bool impressa { get; set; }
        public int CodCliOrig { get; set; }
        public string NFDevol { get; set; }

        [JsonProperty("nat-operacao-rem")]
        public string natoperacaorem { get; set; }
        public int NTecnico { get; set; }

        [JsonProperty("nr-pedido")]
        public int nrpedido { get; set; }
        public int DefInd { get; set; }

        [JsonProperty("id-orcamento")]
        public string idorcamento { get; set; }

        [JsonProperty("e-cliente")]
        public bool ecliente { get; set; }

        [JsonProperty("qt-reparos")]
        public double qtreparos { get; set; }
        public string CidSirOrg { get; set; }

        [JsonProperty("it-codigo-equiv")]
        public string itcodigoequiv { get; set; }
        public string clisirog { get; set; }
        public string ConSirOrg { get; set; }
        public bool Aprovado { get; set; }

        [JsonProperty("dt-trans")]
        public object dttrans { get; set; }

        [JsonProperty("cod-estabel")]
        public string codestabel { get; set; }

        [JsonProperty("est-ntecnico")]
        public string estntecnico { get; set; }

        [JsonProperty("usuar-recebe-exp")]
        public string usuarrecebeexp { get; set; }

        [JsonProperty("fm-codigo")]
        public string fmcodigo { get; set; }
        public string FonSirOrg { get; set; }
        public string NatDevol { get; set; }
        public string CodLab { get; set; }

        [JsonProperty("nf-saida")]
        public string nfsaida { get; set; }
        public string NFRecebe { get; set; }
        public string NFrem { get; set; }
        public string HoraRecebe { get; set; }
        public string UsuarioC { get; set; }
        public string UsuarioI { get; set; }
        public int sequencia { get; set; }
        public double NumRR { get; set; }
        public object dtremcon { get; set; }
        public string Flag { get; set; }

        [JsonProperty("rr-bloq")]
        public bool rrbloq { get; set; }

        [JsonProperty("est-ntecnicosol")]
        public string estntecnicosol { get; set; }

        [JsonProperty("data-recebe-exp")]
        public object datarecebeexp { get; set; }

        [JsonProperty("it-codigo")]
        public string itcodigo { get; set; }

        [JsonProperty("est-ntecnicolab")]
        public string estntecnicolab { get; set; }

        [JsonProperty("num-plan")]
        public int numplan { get; set; }

        [JsonProperty("nr-pedido-dev")]
        public int nrpedidodev { get; set; }
        public string EndSirOrg { get; set; }
        public string Situacao { get; set; }
        public string CodFilial { get; set; }
        public string Atividade { get; set; }

        [JsonProperty("nr-laudo")]
        public int nrlaudo { get; set; }
        public string Observacao { get; set; }
        public string clisirde { get; set; }

        [JsonProperty("l-equivalente")]
        public bool lequivalente { get; set; }
        public double Chamado { get; set; }
        public string NFserie { get; set; }

        [JsonProperty("it-procomp")]
        public string itprocomp { get; set; }

        [JsonProperty("tp-encerra")]
        public string tpencerra { get; set; }
        public string localizacao { get; set; }
        public string EstSirOrg { get; set; }

        [JsonProperty("serie-enc")]
        public string serieenc { get; set; }
        public bool filsp { get; set; }
        public int NTecnicosol { get; set; }

        [JsonProperty("hora-recebe-exp")]
        public string horarecebeexp { get; set; }
        public string natretfe { get; set; }
        public int NTecnicolab { get; set; }
        public object DataRRAnt { get; set; }
    }

    public class ReparoItemResponse:BaseModel
    {
        public List<ReparoItem> items { get; set; }
        public string ok { get; set; }
        public int NumPedExec { get; set; }
    }

   
    public class ReparoItemRequest
    {
        public List<ReparoItem> reparos { get; set; }
        public List<ReparoItem> itemsReparo { get; set; }
    }

}
