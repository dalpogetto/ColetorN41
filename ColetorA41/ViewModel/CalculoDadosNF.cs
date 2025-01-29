using ColetorA41.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.ViewModel
{
    public class CalculoDadosNF
    {
        TotvsA41Service _srv;

        public CalculoDadosNF()
        {
        }

        public CalculoDadosNF(TotvsA41Service srv)
        {
            _srv = srv;
        }
    }
}
