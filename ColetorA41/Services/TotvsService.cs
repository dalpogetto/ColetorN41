using ColetorA41.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace ColetorA41.Services
{
    public partial class TotvsService:BaseService
    {

        private const string AuthStateKey = "AuthState";
        private const string InfoLogin = "UsuarioSenhaBase64";

        public TotvsService(IHttpClientFactory httpClientFactory, IConfiguration config) : base(httpClientFactory, config)
        {
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(2000);

            var authState = Preferences.Default.Get<bool>(AuthStateKey, false);
            var dadosLogin = Preferences.Default.Get(InfoLogin, String.Empty);
            //Setar Informacoes Usuario e Senha para Chamar APIs TOTVS
            Ambiente.UsuarioSenhaBase64 = dadosLogin;

            return authState;
        }
        public async Task<bool> Login(string usuario, string senha)
        {
            var byteArray = new UTF8Encoding().GetBytes($"{usuario}:{senha}");
            Ambiente.UsuarioSenhaBase64 = Convert.ToBase64String(byteArray);

            try
            {

                if (!await Login("101"))
                {
                    await Shell.Current.DisplayAlert("Erro!", "Usuário e senha inválidos", "OK");
                    return default;
                }


                Preferences.Default.Set<bool>(AuthStateKey, true);
                // var byteArray = new UTF8Encoding().GetBytes($"{usuario}:{senha}");
                // Ambiente.UsuarioSenhaBase64 = Convert.ToBase64String(byteArray);
                Preferences.Default.Set(InfoLogin, Ambiente.UsuarioSenhaBase64);
                return true;
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Erro!", "Usuário e senha inválidos", "OK");
                return false;
            }

        }
        public void Logout()
        {
            Preferences.Default.Clear();
        }

        public async Task DownloadVersao()
        {
            var response = await GetAsync<Versao>("apiesaa041/DownloadVersao");
            using var reader = new StreamReader(response.arquivo);


        }

        public async Task<ItemFichaResponse> ObterItensCalculoMobile(int tipoCalculo, string tipoFicha, int nrProcess, int skip, int pageSize, string criterioBuscaItemFicha)
        {
            var param = new NameValueCollection { { "TipoCalculo", tipoCalculo.ToString() }, 
                                                  { "TipoFicha", tipoFicha },
                                                  { "NrProcess", nrProcess.ToString() },
                                                  { "Skip", skip.ToString() },
                                                  { "PageSize", pageSize.ToString() },
                                                  { "criterioBusca", criterioBuscaItemFicha },

                                                };
            var response = await GetAsync<ItemFichaResponse>("apiesaa041/ObterItensCalculoMobile", param);
            return response;
        }

        public async Task<List<ParamEstabel>> ObterParamEstabelecimentos()
        {
            var response = await GetAsync<ParamEstabelecimentoResponse>("apiesaa041/ObterCalcEstab");
            return response.items;
        }

        public async Task<bool> VerificarVersaoMobile(string versaoAtual)
        {
            var param = new NameValueCollection { { "versao", versaoAtual } };
            var response = await GetAsync<Mobile>("apiesaa041/ObterVersaoMobile", param);
            return response.versaoValida;
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
            if (response.type == "error")
            {
                throw new Exception($"Code: {response.code}\nDetail: {response.detailedMessage}\nMessage: {response.message}");
            }
            return response.items;
        }

        public async Task<List<Tecnico>> ObterTecEstabMobile(string codEstabel, string criterioBusca, int skip, int pageSize)
        {
            var param = new NameValueCollection { { "codEstabel", codEstabel }, 
                                                  { "criterioBusca", criterioBusca }, 
                                                  { "Skip", skip.ToString()}, 
                                                  { "PageSize", pageSize.ToString() } 
                                                };
            var response = await GetAsync<TecnicoResponse>("apiesaa041/ObterTecEstabMobile", param);
            if (response.type == "error")
            {
                return null;
            }
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

        public async Task<ExecutarCalculoResponse> AprovarCalculo(ParamsTela req)
        {
            var request = new ExecutarCalculoRequest { paramTela = req};

            var response = await PostAsync<ExecutarCalculoRequest, ExecutarCalculoResponse>("apiesaa041/ObterExtrakit", request);
            return response;
        }

        public async Task<List<ProcessosEstab>> ObterProcessosEstab(string codEstabel)
        {
            var param = new NameValueCollection { { "codEstabel", codEstabel } };
            var response = await GetAsync<ProcessosEstabResponse>("apiesaa041/ObterProcessosEstab", param);
            return response.items;
        }

        public async Task<ArquivoResumo> ImprimirConfOS(string iExecucao, string nrProcess)
        {
            var param = new NameValueCollection { { "iExecucao", iExecucao }, { "nrProcess", nrProcess } };
            var response = await GetAsync<ArquivoResumo>("apiesaa041/ImprimirConfOS", param);
            return response;
        }

        public async Task EncerrarProcesso(string codEstabel, string nrProcess)
        {
            var param = new NameValueCollection { { "codEstabel", codEstabel }, { "nrProcess", nrProcess } };
            var response = await GetAsync<EncerrarResponse>("apiesaa041/EncerrarProcesso", param);
        }

        public async Task<List<ReparoItem>> ObterItensParaReparo(string codEmitente, string nrProcess)
        {
            var param = new NameValueCollection { { "codEmitente", codEmitente }, { "nrProcess", nrProcess } };
            var response = await GetAsync<ReparoItemResponse>("apiesaa041/ObterItensParaReparo", param);
            return response.items;
        }

        public async Task<bool> ValidarItensReparo(List<ReparoItem> lista)
        {
            var request = new ReparoItemRequest { itemsReparo = lista };
            var response = await PostAsync<ReparoItemRequest, ReparoItemResponse>("apiesaa041/ValidarItensReparo", request);
            return response.ok == "ok";
        }

        public async Task<ReparoItemResponse> AbrirReparos(List<ReparoItem> lista)
        {
            var request = new ReparoItemRequest { itemsReparo = lista };
            var response = await PostAsync<ReparoItemRequest, ReparoItemResponse>("apiesaa041/AbrirReparos", request);
            return response;
        }
    }
}
