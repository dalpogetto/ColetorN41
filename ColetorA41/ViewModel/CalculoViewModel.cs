using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ColetorA41.Models;

using ColetorA41.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ColetorA41.Views.Calculo;
using Microsoft.Extensions.Configuration;

namespace ColetorA41.ViewModel
{
    public partial class CalculoViewModel : BaseViewModel
    {
        private readonly TotvsService _service;
        private readonly TotvsService46 _service46;
        private readonly IConfiguration _config;
       
      
        public CalculoViewModel(TotvsService totvsService, 
                                TotvsService46 totvsService46,
                                IConfiguration config)
        {
            _service = totvsService;
            _service46 = totvsService46;
            _config = config;
            this.ObterEstabelecimentos();
        }

        private Estabelecimento _estabSelecionado;
        public Estabelecimento EstabSelecionado
        {
            get => _estabSelecionado;
            set
            {
                if (_estabSelecionado != value)
                {
                    _estabSelecionado = value;
                     this.ObterTecnicosEstab();
                     this.ObterTransporte();
                }
            }
        }

        private Tecnico _tecnicoSelecionado;
        public Tecnico TecnicoSelecionado
        {
            get => _tecnicoSelecionado;
            set
            {
                if (_tecnicoSelecionado != value)
                {
                    _tecnicoSelecionado = value;
                    this.ObterEntrega();
                    
                }
            }
        }

        public async Task ObterEstabelecimentos()
        {
            this.IsBusy = true;
            var lista = await _service.ObterEstabelecimentos();

            this.listaEstab.Clear();
            foreach (var item in lista.OrderBy(x => x.identific)) 
            {
                this.listaEstab.Add(item);
            }
            this.IsBusy = false;
        }

        
        [ObservableProperty]
        int nrProcessSelecionado;

        public ObservableCollection<Estabelecimento> listaEstab { get; private set; } = new();

        public async Task ObterTecnicosEstab()
        {
            this.IsBusy = true;
            var lista = await _service.ObterTecEstab(this._estabSelecionado.codEstab);

            this.listaTecnico.Clear();
            foreach (var item in lista.OrderBy(x => x.identific))
            {
                this.listaTecnico.Add(item);
            }
            this.IsBusy = false;
        }

        public ObservableCollection<Tecnico> listaTecnico { get; private set; } = new();

        public ObservableCollection<Enc> listaEnc { get; private set; } = new();

        [ObservableProperty]
        string etiquetaEnc;

        //Dados da Nota
        [ObservableProperty]
        string serieEntra;

        [ObservableProperty]
        string serieSaida;

        [ObservableProperty]
        string rpw;

        [ObservableProperty]
        string entrega;

        [ObservableProperty]
        Transporte transpEntraSelecionado;

        [ObservableProperty]
        ParamEstabel parametroSelecionado;

        [ObservableProperty]
        Transporte transpSaidaSelecionado;

        [ObservableProperty]
        Entrega entregaSelecionada;

        [ObservableProperty]
        int usuarioAlmoxa;

        [ObservableProperty]
        string senhaAlmoxa;

        [ObservableProperty]
        int tipoCalculo;

        [ObservableProperty]
        string numEnc;

        
        [ObservableProperty]
        string lblAprovar;

        [ObservableProperty]
        string lblAprovarSemEntrada;



        public ObservableCollection<Transporte> listaTransporte { get; private set; } = new();

        public async void ObterTransporte()
        {
            this.IsBusy = true;
            var lista = await _service.ObterTransportes();

            this.listaTransporte.Clear();
            foreach (var item in lista.OrderBy(x => x.identific))
            {
                this.listaTransporte.Add(item);
            }
            this.IsBusy = false;
        }

        public ObservableCollection<Entrega> listaEntrega { get; private set; } = new();

        public async void ObterEntrega()
        {
            this.IsBusy = true;
            var lista = await _service.ObterEntrega(this.TecnicoSelecionado.codTec, this.EstabSelecionado.codEstab);

            this.listaEntrega.Clear();
            foreach (var item in lista.OrderBy(x=>x.identific))
            {
                this.listaEntrega.Add(item);
            }
            this.IsBusy = false;
        }

        public async Task ObterParametrosEstab()
        {
            this.IsBusy = true;
            var parametro = await _service.ObterParametrosEstab(this._estabSelecionado.codEstab);
            if (parametro != null)
            {
                this.TranspEntraSelecionado = this.listaTransporte.Where(item => item.codTransp == parametro.codTranspEntra).FirstOrDefault();
                this.TranspSaidaSelecionado = this.listaTransporte.Where(item => item.codTransp == parametro.codTranspSai).FirstOrDefault();
                this.EntregaSelecionada = this.listaEntrega.Where(item => item.codEntrega == parametro.codEntrega).FirstOrDefault();
                this.SerieEntra = parametro.serieEntra;
                this.SerieSaida = parametro.serieSai;
                this.Entrega = parametro.codEntrega;
                this.Rpw = parametro.rpw;
            }
            this.IsBusy = false;
        }

        //Extrakit
        [RelayCommand]
        void SelecionarTodosExtrakit()
        {
            foreach (var item in this.listaExtrakit)
            {
                this.listaExtrakitSelecionados.Add(item);
            }
        }

        

        [RelayCommand]
        public void ListaETSelecionada(object obj)
        {
            QtdeETSelecionadas = listaExtrakitSelecionados.OfType<Extrakit>().Sum(s => s.qtSaldo);
            QtdeETNaoSelecionadas = 0;
        }

        public ObservableCollection<Extrakit> listaExtrakit { get; private set; } = new();
        public ObservableCollection<object> listaExtrakitSelecionados { get;  set; } = new ();
        public async void ObterExtrakit()
        {
            this.IsBusy = true;
            this.listaExtrakit.Clear();
            var lista = await _service.ObterExtrakit(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec, this.NrProcessSelecionado);
            foreach (var item in lista)
            {
                this.listaExtrakit.Add(item);
            }
            this.IsBusy = false;
        }

        public async Task ObterDados()
        {
            this.IsBusy = true;
            //Gerar Numero de Processo se for preciso
            var lista = await _service46.ObterDados(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec);
            //Obter Numero Gerado
            this.NrProcessSelecionado = await _service.ObterNrProcesso(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec);
            this.IsBusy = false;
        }

        //Calculo
        public ObservableCollection<Ficha> listaCalculo { get; private set; } = new();
        public ObservableCollection<Semsaldo> listaSemSaldo { get; private set; } = new();
        public ObservableCollection<Models.Resumo> listaResumo { get; private set; } = new();

        public async Task<bool> LoginAlmoxa()
        {
            this.IsBusy = true;
            var ok = await _service.LoginAlmoxa(this.EstabSelecionado.codEstab,
                                                this.UsuarioAlmoxa,
                                                this.SenhaAlmoxa);
            this.IsBusy = false;
            return ok;
        }

        public Command SelectedChangedCommand
        {
            get
            {
                return new Command((sender) =>
                {
                   // Extrakit et = sender as Extrakit;

                    //Console.WriteLine("Select item in Colletionview" + et.itCodigo);
                });
            }
        }

        public IConfiguration Config { get; }

        [RelayCommand]
        private void SelectionChanged()
        {
            foreach (var p in listaExtrakitSelecionados)
            {
                if (p is Extrakit person)
                {
                    Console.WriteLine($"{person.itCodigo} is selected");
                }
            }
        }

        [RelayCommand]
        async Task ChamarEstabTec()
        {
            //await Shell.Current.DisplayAlert("Aqui", "Entrou", "OK");
            await Shell.Current.GoToAsync($"{nameof(EstabTec)}");
        }

        [RelayCommand]
        async Task ChamarResumoDetalhe()
        {
            await Shell.Current.GoToAsync($"{nameof(ResumoDetalhe)}");
        }

        [RelayCommand]
        async Task ChamarDadosNF()
        {
            if ((this.EstabSelecionado == null) || (this.TecnicoSelecionado == null))
            {
                await Shell.Current.DisplayAlert("Erro!", "Dados do estabelecimento e técnicos não preenchidos corretamente", "OK");
                //await Shell.Current.GoToAsync($"/{nameof(EstabTec)}");
                return;
            }
            await this.ObterParametrosEstab();
            await this.ObterDados();
            await Shell.Current.GoToAsync($"{nameof(DadosNF)}");
        }

        [RelayCommand]
        async Task ChamarExtrakit()
        {
            //await Shell.Current.DisplayAlert("Aqui", "Entrou", "OK");
            this.ObterExtrakit();
            await Shell.Current.GoToAsync($"{nameof(ExtrakitView)}");
        }

        

        [RelayCommand]
        async Task ChamarLeituraENC()
        {
            await Shell.Current.GoToAsync($"{nameof(LeituraENC)}");
            await this.ObterEncs();
        }

        public async Task ObterEncs()
        {
            this.IsBusy = true;
            var lista = await _service46.ObterEncs(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec);
            this.listaEnc.Clear();
            foreach (var item in lista)
            {
                this.listaEnc.Add(item);
            }
            this.IsBusy = false;

        }

        [RelayCommand]
        async Task LeituraENC(string numEnc)
        {
            this.IsBusy = true;
            if (string.IsNullOrEmpty(numEnc))
            {
                this.IsBusy = false;
                return;
            }

            var item = await _service46.LeituraEnc(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec, numEnc, this.entregaSelecionada.nrProcesso.ToString());
            item.numEnc = numEnc;
            if (item.flag.ToUpper() == "ERRO")
            {
                this.IsBusy = false;
                await Shell.Current.DisplayAlert("Erro ENC", item.mensagem, "OK");
                return;
            }
            this.listaEnc.Add(item);
            this.NumEnc = string.Empty;
            this.IsBusy = false;
           
            
        }

        public async void Mock()
        {
            this.IsBusy = true;
            this.listaEnc.Add(new Enc {numEnc="12345", chamado = "11111", flag = "OK", itCodigo = "85.111.00024-2b", numOS=8540, mensagem="Mensagem: OK" });
            this.listaEnc.Add(new Enc {numEnc = "6789", chamado = "", flag = "NOK", itCodigo = "86.555.00024-2b", itDescricao = "", numOS = 0, mensagem = "" });
            this.IsBusy = false;
        }

        [RelayCommand]
        async Task EliminarEnc(Enc objEnc)
        {
            IsBusy = true;
            await _service46.Desmarcar(objEnc.cRowId, objEnc.cItemRowId);
            var item = this.listaEnc.Where(x => x.numEnc == objEnc.numEnc).First();
            this.listaEnc.Remove(item);
            IsBusy = false;

        }

        [RelayCommand]
        async Task ChamarResumo()
        {
            this.IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(Views.Calculo.Resumo)}");
            await this.PrepararCalculo();
            
        }

        [RelayCommand]
        async Task RadioTipoCalculo(string tipoCalculo)
        {
            var tipo = Convert.ToInt32(tipoCalculo);
            await this.AtualizaLblBotoes(tipo);
            await this.AtualizarLabelsContadores(tipo);
        }

        async Task AtualizaLblBotoes(int tipo)
        {
            switch (tipo)
            {
                case 1:
                    {
                        LblAprovar = "Renovação Total";
                        LblAprovarSemEntrada = "Renovação Total";
                    }
                    break;

                case 2:
                    {
                        LblAprovar = "Renovação Parcial";
                        LblAprovarSemEntrada = "Renovação Parcial";
                    }
                    break;
                case 3:
                    {
                        LblAprovar = "ExtraKit";
                        LblAprovarSemEntrada = "Extrakit";
                    }
                    break;
            }
        }

        //Login Almoxarifado
        [ObservableProperty]
        LabelResumo fichas = new ();


        public async Task PrepararCalculo()
        {
            this.IsBusy = true;
            //Gerar Numero de Processo se for preciso
            var calculo = await _service.PrepararCalculoMobile(this.EstabSelecionado.codEstab,
                                                         this.TecnicoSelecionado.codTec,
                                                         this.NrProcessSelecionado,
                                                         this.listaExtrakit.ToList());

            //Montar 
            this.listaCalculo.Clear();
            foreach (var item in calculo.items)
            {
                this.listaCalculo.Add(item);
            }

            //Chamar Tela
            await Task.Delay(100);
            await this.AtualizaLblBotoes(2);
            await this.AtualizarLabelsContadores(2);

        }

        public async Task AtualizarLabelsContadores(int tipoCalculo)
        {

            TipoCalculo = tipoCalculo;

            var item = this.listaCalculo.Where(x => x.tipo == tipoCalculo).FirstOrDefault();
            if (item == null) return;

            //Extrakits nao selecionados
            var totalET = 0;
            foreach (var x in listaExtrakit)
            {
                totalET += x.qtDisp;
            }

            //Geral
            Fichas.Geral = item.qtGeral + totalET;

            //Gera Extrakit - Labels
            Fichas.GeralExtrakit = totalET;

            //Pagamento
            Fichas.Pagto = item.qtPagto;

            //Renovacoes
            Fichas.Renovacao = item.qtReno;

            //Somente Entrada
            Fichas.SomenteEntrada = item.qtSoEntra;

            //Extrakit
            Fichas.Extrakit = item.qtExtrakit;

            //Sem Saldo
            Fichas.SemSaldo = item.qtSemSaldo;

            this.IsBusy = false;

            //Atualizar tela
            

        }
    }
}
