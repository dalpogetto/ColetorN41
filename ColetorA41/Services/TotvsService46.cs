using ColetorA41.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Services
{
    public class TotvsService46 : BaseService
    {
        

        public async Task<List<OrdemServico>> ObterDados(string codEstabel, int codTecnico, string senha = "moto", string origem = "calculo")
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

        public async Task<Enc> LeituraEnc(string codEstabel, int codTecnico, string numEnc, string nrProcesso)
        {
            var param = new NameValueCollection {
                {"codEstabel", codEstabel },
                {"codTecnico", codTecnico.ToString() },
                {"numEnc", numEnc },
                {"nrProcesso", nrProcesso }
            };
            var response = await GetAsync<EncResponse>("apiesaa046/LeituraEnc", param);
            return response.items.FirstOrDefault();
        }

        public async Task<List<Enc>> ObterEncs(string codEstabel, int codTecnico)
        {
            var param = new NameValueCollection {
                {"codEstabel", codEstabel },
                {"codTecnico", codTecnico.ToString() },
            };
            var response = await GetAsync<EncResponse>("apiesaa046/ObterEncs", param);
            return response.items;
        }

        public async Task<bool> Desmarcar(string cRowId, string cItemRowId)
        {
            var param = new NameValueCollection {
                {"cRowId", cRowId },
                {"cItemRowId", cItemRowId }
            };
            var response = await GetAsync<EncResponse>("apiesaa046/Desmarcar", param);
            return true;
        }

    }
}
