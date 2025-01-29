﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
{
    
    public class TecnicoResponse
    {
        public int total { get; set; }
        public bool hasNext { get; set; }
        public List<Tecnico> items { get; set; }
    }

    public class Tecnico
    {
        public int codTec { get; set; }
        public string nomeAbrev { get; set; }

        public string identific => $"{codTec} - {nomeAbrev}";
    }

}
