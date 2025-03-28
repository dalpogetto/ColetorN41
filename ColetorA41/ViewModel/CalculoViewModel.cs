﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ColetorA41.Models;

using ColetorA41.Services;
using System.Collections.ObjectModel;
using ColetorA41.Views.Calculo;
using Microsoft.Extensions.Configuration;
using ColetorA41.Views;
using ColetorA41.Extensions;
using CommunityToolkit.Maui.Views;
using ColetorA41.Views.Monitor;
using Microsoft.Maui.Controls.Platform.Compatibility;

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
        }


        #region Getter-Setter
        private Estabelecimento _estabSelecionado;
        public Estabelecimento EstabSelecionado
        {
            get =>  _estabSelecionado;
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
                    Task.Run(async () => await this.ObterEntrega());

                }
            }
        }

        #endregion

        #region Lista Compartilhadas
        public ObservableCollection<Estabelecimento> listaEstab { get; private set; } = new();
        public ObservableRangeCollection<Tecnico> listaTecnico { get; private set; } = new();
        public ObservableCollection<Enc> listaEnc { get; private set; } = new();
        public ObservableCollection<Transporte> listaTransporte { get; private set; } = new();
        public ObservableCollection<Entrega> listaEntrega { get; private set; } = new();
        public ObservableCollection<Extrakit> listaExtrakit { get; private set; } = new();
        public ObservableCollection<object> listaExtrakitSelecionados { get; set; } = new();
        public ObservableCollection<Extrakit> listaExtrakitNaoSelecionados { get; set; } = new();
        public ObservableCollection<Ficha> listaCalculo { get; private set; } = new();
        public ObservableCollection<Semsaldo> listaSemSaldo { get; private set; } = new();
        public ObservableCollection<Models.Resumo> listaResumo { get; private set; } = new ();
        public ObservableRangeCollection<ItemFicha> listaItensFicha { get; private set; } = new();

        public ObservableCollection<ItemFicha> listaPagtos { get; private set; }  = new();
        #endregion

        #region Listas Locais
        #endregion

        #region Variaveis Compartilhadas

        [ObservableProperty]
        string labelErro = "";

        [ObservableProperty]
        int nrProcessSelecionado;

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
        string tipoFichaSelecionado;

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

        [ObservableProperty]
        string rowIdOS = "";

        [ObservableProperty]
        string labelLoading;

        //Login Almoxarifado
        [ObservableProperty]
        LabelResumo fichas = new();

        [ObservableProperty]
        string criterioBuscaTecnico = "";

        [ObservableProperty]
        string criterioBuscaItemFicha = "";

        [ObservableProperty]
        bool isTotal = false;

        [ObservableProperty]
        bool isBtnAprovar = true;

        [ObservableProperty]
        bool isBtnAprovarSS = true;

        [ObservableProperty]
        bool isParcial = true;

        [ObservableProperty]
        bool isET = false;

        [ObservableProperty]
        int iRadio=1;

        #endregion

        #region Variaveis Locais


        #endregion

        #region Funcoes Compartilhadas

        [RelayCommand]
        void DownloadVersao()
        {
            var obj = _service.DownloadVersao();

        }
        [RelayCommand]
        void DetalheFicha(object obj)
        {

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

        [RelayCommand]
        void SelecionarNenhumExtrakit()
        {
            this.listaExtrakitSelecionados.Clear();
        }

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
        public async void ListaETSelecionada()
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

        [RelayCommand]
        public async Task LoginAlmoxa()
        {
            try
            {
                this.IsBusy = true;
                var ok = await _service.LoginAlmoxa(this.NrProcessSelecionado,
                                                    this.EstabSelecionado.codEstab,
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
                    var erro = new Mensagem("erro", "Erro Login", ok.mensagem);
                    await Shell.Current.CurrentPage.ShowPopupAsync(erro);
                    return;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
           

        }

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
        async Task ChamarResumoDetalhe(string tipoFicha)
        {
            if (tipoFichaSelecionado != tipoFicha)
            {
                listaItensFicha.Clear();
                tipoFichaSelecionado = tipoFicha;

            }
            listaItensFicha.Clear();
            await this.CarregarFichas();
            await Shell.Current.GoToAsync($"{nameof(ResumoDetalhe)}");
        }

        [RelayCommand]
        async Task ChamarResumoDetalhePagto(string tipoFicha)
        {
            if (tipoFichaSelecionado != tipoFicha)
            {
                listaItensFicha.Clear();
                tipoFichaSelecionado = tipoFicha;

            }
            await this.CarregarFichasPagto();
            await Shell.Current.GoToAsync($"{nameof(ResumoDetalhePago)}");
        }

        [RelayCommand]
        async Task CarregarFichas()
        {
            //Adicionar Extrakit fora do processo na selecao do Geral
            if (this.TipoFichaSelecionado == "Geral" && this.listaItensFicha.Count <= 0)
            {
                foreach (var item in this.listaExtrakitNaoSelecionados)
                {
                    listaItensFicha.Add(new ItemFicha
                    {
                        itCodigo = item.itCodigo,
                        itPrincipal = item.itCodigo,
                        tipo = item.tipo,
                        descItem = item.descItem,
                        qtPagar = 0,
                        qtRenovar = 0,
                        qtRuim = item.qtRuim,
                        qtSaldo = item.qtSaldo,
                        qtExtrakit = item.qtSaldo,
                        notaAnt = item.nroDocto
                    });
                }
            }

            //Itens sem saldo nao sera paginado
            if (this.TipoFichaSelecionado == "SemSaldo" && this.listaItensFicha.Count > 0)
                return;


            // if (IsBusy) return;
            IsBusy = true;

            var lista = await _service.ObterItensCalculoMobile(TipoCalculo, TipoFichaSelecionado, NrProcessSelecionado, listaItensFicha.Count(), 20, BuscaItemFicha);
            listaItensFicha.AddRange(lista.items);
            IsBusy = false;
        }

        async Task CarregarFichasPagto()
        {
           
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
                var msg = new Mensagem("erro", "Erro", ex.Message);
                await Shell.Current.ShowPopupAsync(msg);
               
            }
            

        }

        [RelayCommand]
        async Task ChamarDadosNF(string botaoVoltar="false")
        {
            if ((this.EstabSelecionado == null) || (this.TecnicoSelecionado == null))
            {
                var erro = new Mensagem("erro", "Erro Validação", "Dados do estabelecimento e técnicos não preenchidos corretamente!");
                await Shell.Current.CurrentPage.ShowPopupAsync(erro);
                return;
            }
            await this.ObterParametrosEstab();
            try
            {
                if(botaoVoltar=="false")
                  await this.ObterDados();

                await Shell.Current.GoToAsync($"{nameof(DadosNF)}");

            }
            catch (Exception ex)
            {
                IsBusy = false;
                var msg = new Mensagem("erro", "Erro", ex.Message);
                await Shell.Current.ShowPopupAsync(msg);
              
            }
           
            
        }

        [RelayCommand]
        async Task ChamarExtrakit(string botaoVoltar="false")
        {
            IsBusy = true;
            if (botaoVoltar == "false")
            {
                QtdeETNaoSelecionadas = 0;
                QtdeETNaoSelecionadas = 0;
                await this.ObterExtrakit();
                this.ListaETSelecionada();
            }
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
            //Verificar se existe alguma etiqueta sem leitura

            if (listaEnc != null)
            {
                var lpendente = listaEnc.Where(o => o.flag != "OK").FirstOrDefault();
                if (lpendente != null)
                {

                    var erro = new Mensagem("erro", "Fichas Pendentes", "Verifique a leitura das etiquetas antes de continuar!");
                    await Shell.Current.CurrentPage.ShowPopupAsync(erro);
                    return;
                }
            }

            this.UsuarioAlmoxa_ = 0;
            this.SenhaAlmoxa = string.Empty;

            await Shell.Current.GoToAsync($"{nameof(LoginAlmoxa)}");
        }

       

        [RelayCommand]
        async Task LeituraENC(string numEnc)
        {

            await Task.Run(() =>
            {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                this.IsBusy = true;
                if (string.IsNullOrEmpty(numEnc))
                {
                    this.IsBusy = false;
                    return;
                }

                var item = await _service46.LeituraEnc(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec, numEnc, this.NrProcessSelecionado.ToString());
                item.numEnc = numEnc;
                if (item.flag.ToUpper() == "ERRO")
                {
                    this.IsBusy = false;
                    var erro = new Mensagem("erro", "Erro Enc", item.mensagem);
                    await Shell.Current.CurrentPage.ShowPopupAsync(erro);
                   // this.listaEnc.Add(new Enc { flag = "Erro Leitura", numEnc = numEnc, itCodigo = item.mensagem });
                    this.NumEnc = string.Empty;

                    return;
                }

                //this.listaEnc.Add(item);
                this.listaEnc.Clear();
                await this.ObterEncs();
                this.NumEnc = string.Empty;

            });

            });


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
        async Task EliminarPagto(ItemFicha obj)
        {
            IsBusy = true;
            var item = this.listaPagtos.Where(o=>o.cRowId==obj.cRowId).First();

            await this._service.EliminarPorId(item.id, this._estabSelecionado.codEstab, this._tecnicoSelecionado.codTec);

            this.listaPagtos.Remove(item);
            Fichas.Geral = Fichas.Geral - item.qtPagar;

            await AtualizarLabelsContadores(TipoCalculo);
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
           
            IsTotal = TipoCalculo == 1;
            IsParcial = TipoCalculo == 2 ;
            IsET = TipoCalculo == 3;

            await this.AtualizaLblBotoes(TipoCalculo);
            await Shell.Current.GoToAsync($"{nameof(Views.Calculo.Resumo)}");
        }
       
        [RelayCommand]
        async Task GerarInforme()
        {
            var mensa = new MensagemSimNao("Gerar Informe", "Deseja gerar o Informe de OS ?");
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(mensa);
            if (result is bool ok)
            {
                if (ok)
                {

                    IsBusy = true;
                    LabelLoading = "Gerando Arquivo de Informe";
                    await Shell.Current.GoToAsync($"{nameof(LoadingCalculo)}");

                    var informe = await _service46.ImprimirOS(RowIdOS);
                    if (informe != null)
                    {

                        IsBusy = false;
                        var erro = new Mensagem("ok", "Impressão OS", $"NumPedExec: {informe.NumPedExec} \nArquivo: {informe.Arquivo}");
                        await Shell.Current.CurrentPage.ShowPopupAsync(erro);
                        await Shell.Current.GoToAsync($"{nameof(Views.Calculo.Resumo)}");
                    }
                }
            }
            IsBusy = false;
        }

        [RelayCommand]
        async Task ChamarDetalheResumo()
        {
            //await Shell.Current.GoToAsync($"{nameof(ResumoDetalhe)}");
            await Shell.Current.GoToAsync("..");
        }

        

        [RelayCommand]
        async Task RadioTipoCalculo(object opcao)
        {
            IsBusy = true;
            var tipo = TipoCalculo;
            await this.AtualizaLblBotoes(tipo);
            await this.AtualizarLabelsContadores(tipo);
            IsBusy = false;
        }

        [RelayCommand]
        async Task AprovarCalculo(string tipoAprovacao)
        {
            var mensa = new MensagemSimNao("Execução Cálculo", "Confirma a execução do cálculo ?");
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(mensa);
            if (result is bool ok)
            {
                if (ok)
                {
                    var paramTela = new ParamCalculo
                    {
                         opcao = TipoCalculo,
                         tipoAprovacao= Convert.ToInt32(tipoAprovacao),
                         nrProcess= NrProcessSelecionado,
                         codEstab=EstabSelecionado.codEstab,
                         codEmitente= TecnicoSelecionado.codTec,
                         codEntrega=EntregaSelecionada.codEntrega,
                         serieEntra=SerieEntra,
                         serieSai=SerieSaida,
                         codTranspEntra=TranspEntraSelecionado.codTransp,
                         codTranspSai=TranspSaidaSelecionado.codTransp,
                    };

                    //Executar o calculo
                    var resp = await this._service.AprovarCalculo(paramTela);
                    if(resp.type != null && resp.type == "error")
                    {
                        var mensaCalc = new Mensagem("error", "Cálculo Execução", "Erro execução do cálculo");
                        await Shell.Current.CurrentPage.ShowPopupAsync(mensaCalc);
                        return;

                    }
                    else
                    {
                        var mensaCalc = new Mensagem("ok", "Cálculo Execução", "Execução do cálculo realizada com sucesso! Processo RPW: " + resp.rpw);
                        await Shell.Current.CurrentPage.ShowPopupAsync(mensaCalc);
                        await ChamarEstabTec();
                    }

                }
            }
            IsBusy = false;
        }
        #endregion

        #region Funcoes Locais

        

        public void resetGeral()
        {
            EstabSelecionado = null;
            TecnicoSelecionado = null;
            listaExtrakitSelecionados.Clear();
            listaTecnico.Clear();
            listaExtrakit.Clear();

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

        public async Task ObterTecnicosEstab()
        {
            this.IsBusy = true;
            try
            {
                var lista = await _service.ObterTecEstabMobile(this._estabSelecionado.codEstab, CriterioBuscaTecnico, 0, 20);
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
                var erro = new Mensagem("erro", "Erro", ex.Message);
                await Shell.Current.CurrentPage.ShowPopupAsync(erro);
            }

            this.IsBusy = false;
        }

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

        public async Task ObterEntrega()
        {
            this.IsBusy = true;
            var lista = await _service.ObterEntrega(this.TecnicoSelecionado.codTec, this.EstabSelecionado.codEstab);

            this.listaEntrega.Clear();
            foreach (var item in lista.OrderBy(x => x.identific))
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
        public async Task ObterExtrakit()
        {
            this.IsBusy = true;
            this.listaExtrakit.Clear();
            var lista = await _service.ObterExtrakit(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec, this.NrProcessSelecionado);
            if (lista != null) {
                foreach (var item in lista)
                {
                    this.listaExtrakit.Add(item);
                }
            }
            this.IsBusy = false;
        }

        public async Task ObterDados()
        {
            this.IsBusy = true;
            //Gerar Numero de Processo se for preciso
            RowIdOS = await _service46.ObterDados(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec);
            //Obter Numero Gerado
            this.NrProcessSelecionado = await _service.ObterNrProcesso(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec);
            this.IsBusy = false;
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
                if (!item.soEntrada)
                this.listaPagtos.Add(item);
            }

            //Chamar Tela
            await Task.Delay(100);
            TipoCalculo = 2;
            IsTotal = false;
            IsParcial = true;
            IsET = false;

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
            Fichas.Pagto = listaPagtos.Sum(o=>o.qtPagar);

            //Renovacoes
            Fichas.Renovacao = item.qtReno;

            //Somente Entrada
            Fichas.SomenteEntrada = item.qtSoEntra;

            //Extrakit
            Fichas.Extrakit = item.qtExtrakit;

            //Sem Saldo
            Fichas.SemSaldo = item.qtSemSaldo;

            IsBtnAprovar = true;
            IsBtnAprovarSS = true;

            if (TipoCalculo >= 2)
            {

                var resp = await this._service.AlteracaoTipoCalculoMobile(TipoCalculo.ToString(), this.NrProcessSelecionado.ToString());
                IsBtnAprovar = resp.btnAprovar;
                IsBtnAprovarSS = resp.btnAprovarSS;

                if (!IsBtnAprovar && !IsBtnAprovarSS)
                {
                    var mensa = new Mensagem("error", "Tipo de Cálculo", "Existe OS Informada para Nota Fiscal de Kit. Usar as opções Renovação Total ou Renovação Parcial");
                    await Shell.Current.CurrentPage.ShowPopupAsync(mensa);
                }
            }


            this.IsBusy = false;

        }

        public async Task ObterEncs()
        {
            this.IsBusy = true;
            var lista = await _service46.ObterEncs(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec);
            this.listaEnc.Clear();
            if (lista != null)
            {
                foreach (var item in lista)
                {
                    this.listaEnc.Add(item);
                }
            }
            this.IsBusy = false;
           

        }



        #endregion
    }
}
