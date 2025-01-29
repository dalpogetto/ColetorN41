namespace ColetorA41.Models
{


    public class EstabResponse
    {
        public int total { get; set; }
        public bool hasNext { get; set; }
        public List<Estabelecimento> items { get; set; }
    }

    public class Estabelecimento
    {
        public string codEstab { get; set; }
        public string codFilial { get; set; }
        public string nome { get; set; }

        public string identific => $"{codEstab} - {nome}";
    }

}
