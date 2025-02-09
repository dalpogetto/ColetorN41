﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ColetorA41.Models;

using ColetorA41.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ColetorA41.Views.Calculo;
using Microsoft.Extensions.Configuration;
using ColetorA41.Views;
using CommunityToolkit.Maui.Core.Platform;
using ColetorA41.Extensions;
using CommunityToolkit.Maui.Alerts;

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
          //  Task.Run(async () => await this.ObterEstabelecimentos());

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

                    Task.Run(async () => {
                        IsBusy = false;
                        this.listaTecnico.Clear();
                        await this.CarregarTecnicosEstabelecimento();
                        await this.ObterTransporte();
                        });

                    //this.ObterTecnicosEstab();
                    // this.ObterTransporte();
                }
            }
        }

        [ObservableProperty]
        string labelErro = "";

        private Tecnico _tecnicoSelecionado;
        public Tecnico TecnicoSelecionado
        {
            get => _tecnicoSelecionado;
            set
            {
                if (_tecnicoSelecionado != value)
                {
                    _tecnicoSelecionado = value;
                    Task.Run(async () => await this.ObterEntrega());
                    
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

        public async Task VerificarVersao()
        {
            this.IsBusy = true;
            IsError = await _service.VerificarVersaoMobile(AppInfo.Current.VersionString);
            this.IsBusy = false;
        }

        
        [ObservableProperty]
        int nrProcessSelecionado;

        public ObservableCollection<Estabelecimento> listaEstab { get; private set; } = new();

        public async Task ObterTecnicosEstab()
        {
            this.IsBusy = true;
            try
            {
                var lista = await _service.ObterTecEstabMobile(this._estabSelecionado.codEstab, CriterioBuscaTecnico,0, 20);
                if (lista != null)
                {
                    this.listaTecnico.Clear();
                    this.listaTecnico.AddRange(lista);
                    
                }
                this.IsBusy = false;
            }
            catch (Exception ex)
            {

                IsBusy = false;
                await Shell.Current.DisplayAlert("Atenção", ex.Message, "OK");
            }
            

            /*
            foreach (var item in lista.OrderBy(x => x.identific))
            {
                this.listaTecnico.Add(item);
            }
            */
            this.IsBusy = false;
        }

        public ObservableRangeCollection<Tecnico> listaTecnico { get; private set; } = new();

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
        int qtdeETSelecionadas;

        [ObservableProperty]
        int qtdeETNaoSelecionadas;

        [ObservableProperty]
        Transporte transpEntraSelecionado;

        [ObservableProperty]
        ParamEstabel parametroSelecionado;

        [ObservableProperty]
        Transporte transpSaidaSelecionado;

        [ObservableProperty]
        Entrega entregaSelecionada;

        [ObservableProperty]
        ItemFicha itemFichaSelecionada;

        [ObservableProperty]
        int usuarioAlmoxa_;

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

        public async Task ObterTransporte()
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

        public async Task ObterEntrega()
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
            this.listaExtrakitSelecionados.Clear();

            foreach (var item in this.listaExtrakit)
            {
                this.listaExtrakitSelecionados.Add(item);
            }
        }

        private string buscaTecnico;
        public string BuscaTecnico
        {
            get
            {
                return buscaTecnico;
            }
            set
            {
                buscaTecnico = value;
                if (buscaTecnico.Length > 2)
                {
                   // Task.Run(async () => { await BuscarTecnico(searchText); }).Wait();
                }
                if (buscaTecnico == "")
                {
                    Task.Run(async () => { await BuscarTecnico(""); }).Wait();
                }
            }
        }

        [ObservableProperty]
        string criterioBuscaTecnico = "";

        [RelayCommand]
        async Task BuscarTecnico(string criterio)
        {
            await Task.Run(async ()=> {
                IsBusy = true;
                CriterioBuscaTecnico = criterio;
                await ObterTecnicosEstab();
                IsBusy = false;
                });

        }

        private string buscaItemFicha;
        public string BuscaItemFicha
        {
            get
            {
                return buscaItemFicha;
            }
            set
            {
                buscaItemFicha = value;
                if (buscaItemFicha.Length > 2)
                {
                    // Task.Run(async () => { await BuscarTecnico(searchText); }).Wait();
                }
                if (buscaItemFicha == "")
                {
                    Task.Run(async () => { await BuscarTecnico(""); }).Wait();
                }
            }
        }

        [ObservableProperty]
        string criterioBuscaItemFicha = "";

        [RelayCommand]
        async Task BuscarItemFicha(string criterio)
        {
            await Task.Run(async () => {
                IsBusy = true;
                CriterioBuscaItemFicha = criterio;
                await CarregarFichas();
                IsBusy = false;
            });

        }


        [RelayCommand]
        public void ListaETSelecionada()
        {
            this.listaExtrakitNaoSelecionados.Clear();
            foreach(var item in this.listaExtrakit)
            {
                if (this.listaExtrakitSelecionados.OfType<Extrakit>().Where(x=>x.cRowId==item.cRowId).FirstOrDefault() == null)
                {
                    this.listaExtrakitNaoSelecionados.Add(item);
                }
            }
            QtdeETSelecionadas = listaExtrakitSelecionados.OfType<Extrakit>().Sum(s => s.qtSaldo);
            QtdeETNaoSelecionadas = listaExtrakitNaoSelecionados.Sum(s => s.qtSaldo);
        }

        public ObservableCollection<Extrakit> listaExtrakit { get; private set; } = new();
        public ObservableCollection<object> listaExtrakitSelecionados { get;  set; } = new ();
        public ObservableCollection<Extrakit> listaExtrakitNaoSelecionados { get; set; } = new();
        public async Task ObterExtrakit()
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
        [ObservableProperty]
        string rowIdOS = "";

        public async Task ObterDados()
        {
            this.IsBusy = true;
            //Gerar Numero de Processo se for preciso
            RowIdOS = await _service46.ObterDados(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec);
            //Obter Numero Gerado
            this.NrProcessSelecionado = await _service.ObterNrProcesso(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec);
            this.IsBusy = false;
        }

        //Calculo
        public ObservableCollection<Ficha> listaCalculo { get; private set; } = new();
        public ObservableCollection<Semsaldo> listaSemSaldo { get; private set; } = new();
        public ObservableCollection<Models.Resumo> listaResumo { get; private set; } = new();
        public ObservableRangeCollection<ItemFicha> listaItensFicha { get; private set; } = new();

        [RelayCommand]
        public async Task LoginAlmoxa()
        {
            this.IsBusy = true;
            var ok = await _service.LoginAlmoxa(this.EstabSelecionado.codEstab,
                                                this.UsuarioAlmoxa_,
                                                this.SenhaAlmoxa);
            if (ok.senhaValida)
            {
                //await ChamarResumo();
                await ChamarLoadingCalculo();
            }
            else
            {
                this.IsBusy = false;
                await Shell.Current.DisplayAlert("Erro Login", ok.mensagem, "ok");
                
            }

            //Esconder o teclado
            

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

        [ObservableProperty]
        string tipoFichaSelecionado;

        [RelayCommand]
        async Task ChamarResumoDetalhe(string tipoFicha)
        {
            if (tipoFichaSelecionado != tipoFicha)
            {
                listaItensFicha.Clear();
                tipoFichaSelecionado = tipoFicha;

            }
            await this.CarregarFichas();
            await Shell.Current.GoToAsync($"{nameof(ResumoDetalhe)}");
        }

        [RelayCommand]
        async Task CarregarFichas()
        {
           // if (IsBusy) return;
            IsBusy = true;

            listaItensFicha.Clear();
            var lista = await _service.ObterItensCalculoMobile(TipoCalculo, tipoFichaSelecionado, NrProcessSelecionado, listaItensFicha.Count(), 20, BuscaItemFicha);
            listaItensFicha.AddRange(lista.items);
            IsBusy = false;

        }

        [RelayCommand]
        async Task CarregarTecnicosEstabelecimento()
        {
            if (IsBusy) return;
            IsBusy = true;

            if (_estabSelecionado == null)
            {
                IsBusy = false;
                return;
            }

            // var lista = await _service.ObterItensCalculoMobile(TipoCalculo, tipoFichaSelecionado, NrProcessSelecionado, listaItensFicha.Count(), 20);
            try
            {
                var lista = await _service.ObterTecEstabMobile(this._estabSelecionado.codEstab, CriterioBuscaTecnico ,listaTecnico.Count(), 20);
                if (lista != null)
                    listaTecnico.AddRange(lista);
                IsBusy = false;

            }
            catch (Exception ex)
            {

                IsBusy = false;
                await Shell.Current.DisplayAlert("Atenção", ex.Message, "OK");
            }
            

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
            try
            {
                await this.ObterDados();
                await Shell.Current.GoToAsync($"{nameof(DadosNF)}");

            }
            catch (Exception ex)
            {
                IsBusy = false;
                await Shell.Current.DisplayAlert("Erro", ex.Message, "OK");
            }
           
            
        }

        [RelayCommand]
        async Task ChamarExtrakit()
        {
            IsBusy = true;
            QtdeETNaoSelecionadas = 0;
            qtdeETNaoSelecionadas = 0;
            await this.ObterExtrakit();
            this.ListaETSelecionada();
            await Shell.Current.GoToAsync($"{nameof(ExtrakitView)}");
            IsBusy = false;
        }

        [RelayCommand]
        async Task ChamarMainPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            
        }

        [RelayCommand]
        async Task ChamarLeituraENC()
        {
            await Shell.Current.GoToAsync($"{nameof(LeituraENC)}");
            await this.ObterEncs();
        }

        [RelayCommand]
        async Task ChamarLoginAlmoxa()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginAlmoxa)}");
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

            var item = await _service46.LeituraEnc(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec, numEnc, this.EntregaSelecionada.nrProcesso.ToString());
            item.numEnc = numEnc;
            if (item.flag.ToUpper() == "ERRO")
            {
                this.IsBusy = false;
                await Shell.Current.DisplayAlert("Erro ENC", item.mensagem, "OK");
                return;
            }

            //this.listaEnc.Add(item);
            this.listaEnc.Clear();
            await this.ObterEncs();
            this.NumEnc = string.Empty;
           
           
            
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
        async Task DetalheItemFicha(ItemFicha obj)
        {
            IsBusy = true;
            ItemFichaSelecionada = obj;
            await Shell.Current.GoToAsync($"{nameof(ResumoDetalheItem)}");
            IsBusy = false;

        }

        [RelayCommand]
        async Task ChamarLoadingCalculo()
        {
            this.IsBusy = true;
            //Apagar os calculos anteriores
            this.Fichas = new();
            LabelLoading = "Gerando Resumo Cálculo";
            await Shell.Current.GoToAsync($"{nameof(LoadingCalculo)}");
            await this.PrepararCalculo();

            this.IsBusy = false;
            await Shell.Current.GoToAsync($"{nameof(Views.Calculo.Resumo)}");

        }

        [RelayCommand]
        async Task ChamarResumo()
        {
            await Shell.Current.GoToAsync($"{nameof(Views.Calculo.Resumo)}");
        }
        [ObservableProperty]
        string labelLoading;

        [RelayCommand]
        async Task GerarInforme()
        {
            bool ok = await Shell.Current.DisplayAlert("GERAR INFORME ?", "DESEJA GERAR O INFORME DE OS", "Sim", "Não");
            if (ok)
            {
                IsBusy = true;
                LabelLoading = "Gerando Arquivo de Informe";
                await Shell.Current.GoToAsync($"{nameof(LoadingCalculo)}");

                var informe = await _service46.ImprimirOS(RowIdOS);
                if (informe != null)
                {
                    
                    IsBusy = false;
                    await Shell.Current.DisplayAlert("IMPRESSÃO OS", $"NumPedExec: {informe.NumPedExec} \nArquivo: {informe.Arquivo}", "OK");
                    await Shell.Current.GoToAsync($"{nameof(Views.Calculo.Resumo)}");
                }
            }
            IsBusy = false;
        }


        [RelayCommand]
        async Task ChamarDetalheResumo()
        {
            await Shell.Current.GoToAsync($"{nameof(ResumoDetalhe)}");
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

        ObservableCollection<Calculo> listaPagtos = new();


        public async Task PrepararCalculo()
        {
            this.IsBusy = true;
            this.IsCalculated = true;

            //Gerar Numero de Processo se for preciso
            var calculo = await _service.PrepararCalculoMobile(this.EstabSelecionado.codEstab,
                                                               this.TecnicoSelecionado.codTec,
                                                               this.NrProcessSelecionado,
                                                               this.listaExtrakitSelecionados.OfType<Extrakit>().ToList());

            //Montar Fichar
            this.listaCalculo.Clear();
            foreach (var item in calculo.items)
            {
                this.listaCalculo.Add(item);
            }

            //Montar SemSaldo
            this.listaSemSaldo.Clear();
            foreach (var item in calculo.semsaldo)
            {
                this.listaSemSaldo.Add(item);
            }

            //Montar Pagamentos
            this.listaPagtos.Clear();
            foreach (var item in calculo.pagto)
            {
                this.listaPagtos.Add(item);
            }

            //Chamar Tela
            await Task.Delay(100);
            await this.AtualizaLblBotoes(2);
            await this.AtualizarLabelsContadores(2);

            this.IsBusy = false;
            //await this.ChamarResumo();

        }

        public async Task AtualizarLabelsContadores(int tipoCalculo)
        {

            TipoCalculo = tipoCalculo;

            var item = this.listaCalculo.Where(x => x.tipo == tipoCalculo).FirstOrDefault();
            if (item == null) return;

            //Extrakits nao selecionados
            var totalET = 0;
            foreach (var x in listaExtrakitNaoSelecionados)
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

        }

        [RelayCommand]
        async Task AprovarCalculo()
        {
            bool ok = await Shell.Current.DisplayAlert("EXECUÇÃO CÁLCULO ?", "CONFIRMA EXECUÇÃO DO CÁLCULO", "Sim", "Não");
            if (ok)
            {
                /*
                IsBusy = true;
                LabelLoading = "Gerando Arquivo de Informe";
                await Shell.Current.GoToAsync($"{nameof(LoadingCalculo)}");

                var informe = await _service46.ImprimirOS(RowIdOS);
                if (informe != null)
                {

                    IsBusy = false;
                    await Shell.Current.DisplayAlert("IMPRESSÃO OS", $"NumPedExec: {informe.NumPedExec} \nArquivo: {informe.Arquivo}", "OK");
                    await Shell.Current.GoToAsync($"{nameof(Views.Calculo.Resumo)}");
                }
                */
            }
            IsBusy = false;
        }


    }
}
