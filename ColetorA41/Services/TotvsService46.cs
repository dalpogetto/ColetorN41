using ColetorA41.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Services
{
    public class TotvsService46:BaseService
    {
        public async Task<List<OrdemServico>> ObterDados(string codEstabel, int codTecnico, string senha="moto", string origem="calculo")
        {
            var param = new NameValueCollection { 
                {"codEstabel", codEstabel },
                {"codUsuario", codTecnico.ToString() },
                {"senha", senha },
                {"origem", origem }
            };
            var response = await GetAsync<OrdemServicoResponse>("apiesaa046/ObterDados", param);
            return response.ordens;
        }
    }
}
