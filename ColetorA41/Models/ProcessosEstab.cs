using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ColetorA41.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ProcessosEstab
    {
        [JsonProperty("nome-almoxa")]
        public string? nomealmoxa { get; set; }

        [JsonProperty("nome-abrev")]
        public string? nomeabrev { get; set; }
        public string? situacao { get; set; }
        public string? fase { get; set; }

        [JsonProperty("cod-emitente")]
        public int codemitente { get; set; }

        [JsonProperty("cod-estabel")]
        public string? codestabel { get; set; }

        [JsonProperty("num-ped-exec")]
        public string? numpedexec { get; set; }

        [JsonProperty("nr-process")]
        public int nrprocess { get; set; }

        [JsonProperty("desc-ped-exec")]
        public string? descpedexec { get; set; }
    }

    public class ProcessosEstabResponse
    {
        public List<ProcessosEstab> items { get; set; }
    }


}
