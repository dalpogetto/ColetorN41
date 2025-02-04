using ColetorA41.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Services
{
    public partial class TotvsService:BaseService
    {
        public TotvsService(IHttpClientFactory httpClientFactory, IConfiguration config) : base(httpClientFactory, config)
        {
        }

        public async Task<List<ParamEstabel>> ObterParamEstabelecimentos()
        {
            var response = await GetAsync<ParamEstabelecimentoResponse>("apiesaa041/ObterCalcEstab");
            return response.items;
        }

        public async Task<List<Estabelecimento>> ObterEstabelecimentos()
        {
            var response = await GetAsync<EstabResponse>("apiesaa041/ObterEstab");
            return response.items;
        }

        public async Task<List<Tecnico>> ObterTecEstab(string codEstabel)
        {
            var param = new NameValueCollection { { "codEstabel", codEstabel } };
            var response = await GetAsync<TecnicoResponse>("apiesaa041/ObterTecEstab", param);
            return response.items;
        }

        public async Task<List<Transporte>> ObterTransportes()
        {
            var response = await GetAsync<TransporteResponse>("apiesaa041/ObterTransp");
            return response.items;
        }

        public async Task<List<Entrega>> ObterEntrega(int codTecnico, string codEstab)
        {
            var param = new NameValueCollection { { "codTecnico", codTecnico.ToString() }, { "codEstabel", codEstab } };
            var response = await GetAsync<EntregaResponse>("apiesaa041/ObterEntrega", param);
            return response.listaEntrega;
        }

        public async Task<ParamEstabel> ObterParametrosEstab(string codEstabel)
        {
            var param = new NameValueCollection { { "codEstabel", codEstabel } };
            var response = await GetAsync<ParamEstabelecimentoResponse>("apiesaa041/ObterParamsEstab", param);
            return response.items.FirstOrDefault();
        }

        public async Task<List<Extrakit>> ObterExtrakit(string codEstabel, int codTecnico, int nrProcess )
        {
            var request = new ExtrakitRequest { CodEstab = codEstabel, CodTecnico = codTecnico, NrProcess = nrProcess };
            var response = await PostAsync<ExtrakitRequest, ExtrakitResponse>("apiesaa041/ObterExtrakit", request);
            return response.items;
        }

        public async Task<int> ObterNrProcesso(string codEstabel, int codTecnico)
        {
            var param = new NameValueCollection { {"codEstabel", codEstabel}, {"codTecnico", codTecnico.ToString()} };
            var response = await GetAsync<ProcessoResponse>("apiesaa041/ObterNrProcesso", param);
            return response.nrProcesso;
        }

        public async Task<AlmoxaResponse> LoginAlmoxa(string codEstabel, int usuarioAlmoxa=1888, string senhaAlmoxa = "guigui" )
        {
            var request = new AlmoxaRequest { CodEstabel = codEstabel, CodUsuario = usuarioAlmoxa, Senha = senhaAlmoxa };
            var response = await PostAsync<AlmoxaRequest, AlmoxaResponse>("apiesaa041/LoginAlmoxa", request);
            return response;
        }

        public async Task<bool> Login(string codEstabel, int usuarioAlmoxa = 1888, string senhaAlmoxa = "guigui")
        {
            var request = new AlmoxaRequest { CodEstabel = codEstabel, CodUsuario = usuarioAlmoxa, Senha = senhaAlmoxa };
            var response = await PostAsync<AlmoxaRequest, AlmoxaResponse>("apiesaa041/LoginAlmoxa", request);
            return response.senhaValida;
        }

        public async Task<PrepararCalculoResponse> PrepararCalculoMobile(string codEstabel, int codTecnico, int nrProcess, List<Extrakit> listaET)
        {
            var request = new PrepararCalculoRequest { CodEstab = codEstabel, CodTecnico = codTecnico, NrProcess = nrProcess, Extrakit=listaET };
            var response = await PostAsync<PrepararCalculoRequest, PrepararCalculoResponse>("apiesaa041/PrepararCalculoMobile", request);
            return response;
        }

        public async Task<bool> EliminarParametrosEstabel(string codEstabel)
        {
            var param = new NameValueCollection { { "codEstabel", codEstabel }};
            var response = await GetAsync<Object>("apiesaa041/DeletarCalcEstab", param);
            return true;
        }

        public async Task<bool> SalvarParametrosEstab(ParamEstabel param)
        {
            var request = new ParamEstabelecimentoRequest { paramsTela = param };
            var response = await PostAsync<ParamEstabelecimentoRequest, Object>("apiesaa041/SalvarCalcEstab", request);
            return true;
        }
    }
}
