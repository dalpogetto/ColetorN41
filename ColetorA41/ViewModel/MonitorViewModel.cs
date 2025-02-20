﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColetorA41.Extensions;
using ColetorA41.Models;
using ColetorA41.Services;
using ColetorA41.Views;
using ColetorA41.Views.Monitor;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Maui.Views;

namespace ColetorA41.ViewModel
{
    public partial class MonitorViewModel:BaseViewModel
    {
        private readonly TotvsService _service;
        private readonly TotvsService46 _service46;
        private readonly IConfiguration _config;


        public MonitorViewModel(TotvsService totvsService,
                                TotvsService46 totvsService46,
                                IConfiguration config)
        {
            _service = totvsService;
            _service46 = totvsService46;
            _config = config;
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
                        this.listaProcessosEstab.Clear();
                        await this.CarregarProcessosEstabelecimento();
                    });
                }
                
            }
        }

        [ObservableProperty]
        ProcessosEstab processoSelecionado;

        [ObservableProperty]
        ArquivoResumo arquivoResumo;

        [ObservableProperty]
        ReparoItem reparoItemDados;

        [ObservableProperty]
        ReparoItem reparoOriginal;

        [ObservableProperty]
        string justificativa;

        [ObservableProperty]
        notafiscal dadosNotaFiscal;


        [RelayCommand]
        async Task ChamarPopup()
        {
           
        }

        public ObservableCollection<Estabelecimento> listaEstab { get; private set; } = new();
        public ObservableRangeCollection<ProcessosEstab> listaProcessosEstab { get; private set; } = new();
        public ObservableRangeCollection<ReparoItem> listaReparos { get; private set; } = new();
        public ObservableRangeCollection<NotaFiscalPagto> listaNotaPagto { get; private set; } = new();


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
        ProcessosEstab processoEstabSelecionado;


        [RelayCommand]
        async Task ChamarProcesso()
        {
            await Shell.Current.GoToAsync($"{nameof(Processos)}");

        }

        [RelayCommand]
        async Task ChamarMainPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

        }

        [RelayCommand]
        async Task CarregarProcessosEstabelecimento()
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
                listaProcessosEstab.Clear();
                var lista = await _service.ObterProcessosEstab(this._estabSelecionado.codEstab);
                if (lista != null)
                    listaProcessosEstab.AddRange(lista);
                IsBusy = false;
                IsRefreshing = false;

            }
            catch (Exception ex)
            {

                IsBusy = false;
                IsRefreshing = false;
                await Shell.Current.DisplayAlert("Atenção", ex.Message, "OK");
            }


        }
        [RelayCommand]
        async Task AtualizarListaProcessos()
        {
            await CarregarProcessosEstabelecimento();
        }

        [RelayCommand]
        async Task ChamarTarefa(ProcessosEstab obj)
        {
            ProcessoEstabSelecionado = obj;
            IsBack = true;

            if (obj.situacao == "L")
            {
                ArquivoResumo = null;
                await Shell.Current.GoToAsync($"{nameof(ResumoFinal)}");
            }

            else if (obj.situacao == "R")
            {
                await Shell.Current.GoToAsync($"{nameof(Reparo)}");
                await this.ObterItensParaReparo();
            }

            else if (obj.situacao == "B")
            {
                await Shell.Current.GoToAsync($"{nameof(Embalagem)}");
                //await this.ObterDadosPrimeiraNota();
                await this.ObterNotasPagto();
            }

        }

        [RelayCommand]
        async Task ImprimirConferencia()
        {
            var popup = new MensagemSimNao("Arquivo Conferência", "Confirma Geração Arquivo Conferência ?");
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
            if (result is bool ok)
            {
                if (ok)
                {
                    IsBusy = true;
                    ArquivoResumo = await _service.ImprimirConfOS("2", ProcessoEstabSelecionado.nrprocess.ToString());
                    IsBusy = false;
                }
            }
        }

        [RelayCommand]
        async Task EncerrarProcesso()
        {

            if (ArquivoResumo == null)
            {
                var erro = new Mensagem("erro", "Erro Validação", "Arquivo de Conferência ainda não foi gerado!");
                await Shell.Current.CurrentPage.ShowPopupAsync(erro);
                return;
            }

            var popup = new MensagemSimNao("Encerramento Processo", "Confirma Encerramento do Processo ?");
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
            if (result is bool ok)
            {
                if (ok)
                {
                    IsBusy = true;
                    await _service.EncerrarProcesso(ProcessoEstabSelecionado.codestabel, ProcessoEstabSelecionado.nrprocess.ToString());
                    IsBusy = false;
                    await CarregarProcessosEstabelecimento();
                    await Shell.Current.GoToAsync($"{nameof(Processos)}");
                   
                }
            }
        }

        [RelayCommand]
        async Task ObterItensParaReparo()
        {
            IsBusy = true;
            this.listaReparos.Clear();
            listaReparos.AddRange(await _service.ObterItensParaReparo(ProcessoEstabSelecionado.codemitente.ToString(), ProcessoEstabSelecionado.nrprocess.ToString()));
            IsBusy = false;
        }

        [RelayCommand]
        async Task ObterDadosPrimeiraNota()
        {
            IsBusy = true;
            var resp = await _service.ObterPrimeiraNota(new DadosEmbalagem
            {
                CodEstab = ProcessoEstabSelecionado.codestabel,
                CodTecnico = ProcessoEstabSelecionado.codemitente,
                NrProcess = ProcessoEstabSelecionado.nrprocess
            });

            DadosNotaFiscal = resp.nfs[0];
            IsBusy = false;
        }

        [RelayCommand]
        async Task ObterNotasPagto()
        {
            IsBusy = true;
            listaNotaPagto.Clear();
            var resp = await _service.ObterNotasPagto(ProcessoEstabSelecionado.nrprocess.ToString());
            listaNotaPagto.AddRange(resp.items);
            IsBusy = false;
        }

        [RelayCommand]
        async Task EditarReparoItem(ReparoItem item)
        {
            ReparoItemDados = item;

            //Guardar valor original para retornar aos campos qdo houver erro
            ReparoOriginal = new ReparoItem { itcodigoequiv = item.itcodigoequiv, qtequiv = item.qtequiv, numserieit = item.numserieit };
            await Shell.Current.GoToAsync($"{nameof(ReparoEdicaoItemReparo)}");
        }

        [RelayCommand]
        async Task SalvarItemReparo()
        {
            //Testar numero de serie
            IsBusy=true;
            var ok = await _service46.ValidarSerie(ReparoItemDados.itcodigo, ReparoItemDados.numserieit);
            if (ok.type == "error")
            {
                
                var erro = new Mensagem("error", ok.detailedMessage, ok.message);
                await Shell.Current.CurrentPage.ShowPopupAsync(erro);
                //Num serie original
                ReparoItemDados.numserieit = "000000000000";
                //return;
            }

            IsBusy = false;
            ReparoItemDados.lequivalente = ReparoItemDados.itcodigoequiv != string.Empty;
            await this.ChamarReparo();
           
        }

        [RelayCommand]
        async Task EliminarReparoItem(ReparoItem item)
        {
            var dialog = new MensagemSimNao("Exclusão Reparo", "Confirma Exclusão ?");
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(dialog);
            if (result is bool ok)
            {
                if (ok)
                {
                    listaReparos.Remove(item);
                }
            }
        }

        [RelayCommand]
        async Task EliminarTodosReparoItem()
        {
            var dialog = new MensagemSimNao("Eliminar todos", "Confirma Eliminação de Todos ?");
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(dialog);
            if (result is bool ok)
            {
                if (ok)
                {
                    listaReparos.Clear();
                }
            }
        }

        [RelayCommand]
        async Task AbrirReparos()
        {
            var lexcecao = true;
            var dialog = new MensagemSimNao("Abertura Reparos", "Confirma Abertura de Reparos ?");
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(dialog);
            if (result is bool ok)
            {
                if (ok)
                {
                    //justificativa
                    foreach (var item in listaReparos)
                    {
                        item.descitemequiv = Justificativa;
                        if (!item.lequivalente)
                            lexcecao = false;
                        else if (item.lequivalente && (item.itcodigo.Substring(0, 6) == item.itcodigoequiv.Substring(0, 6)) && (item.itcodigo.Substring(0, 2) == "98"))
                            lexcecao = false;
                    }

                    if (lexcecao && listaReparos.Count > 0)
                    {

                        var dialog2 = new MensagemSimNao("Equivalência por Exceção", "Confirma Equivalência por Exceção ?");
                        var result2 = await Shell.Current.CurrentPage.ShowPopupAsync(dialog2);
                        if (result2 is bool ok2)
                        {
                            if (ok2)
                            {
                                var res = await _service.ValidarItensReparo(listaReparos.ToList());
                                if (res.type == "error")
                                {
                                    var mensa = new Mensagem("error", res.detailedMessage, res.message);
                                    await Shell.Current.CurrentPage.ShowPopupAsync(mensa);
                                    return;
                                }
                                if (res.ok == "ok")
                                {
                                   await ChamarCriacaoReparo();
                                }
                            }
                        }
                    }
                    else
                    {
                        var res = await _service.ValidarItensReparo(listaReparos.ToList());
                        if (res.type == "error")
                        {
                            var mensa = new Mensagem("error", res.detailedMessage, res.message);
                            await Shell.Current.CurrentPage.ShowPopupAsync(mensa);
                            return;
                        }
                        if (res.ok == "ok")
                        {
                            await ChamarCriacaoReparo();
                        }

                    }
                }
            }
        }

        async Task ChamarCriacaoReparo()
        {
            var dialog = new MensagemSimNao("Geração e Impressao Reparos", "Deseja Gerar e Imprimir os Reparos ?");
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(dialog);
            if (result is bool ok)
            {
                if (ok)
                {
                    //Caso nao exista nenhum registro no grid retornar valores padrao
                    if (listaReparos.Count == 0)
                    {
                        listaReparos.Add(new ReparoItem { itcodigo = "", codestabel = ProcessoEstabSelecionado.codestabel, nrprocess = ProcessoEstabSelecionado.nrprocess });
                    }
                    var res = await _service.AbrirReparos(listaReparos.ToList());
                    if (res.type == "error")
                    {
                        var mensa = new Mensagem("error", res.detailedMessage, res.message);
                        await Shell.Current.CurrentPage.ShowPopupAsync(mensa);
                        return;
                    }

                    if (res.NumPedExec > 0)
                    {
                        var mensa = new Mensagem("ok", "Gerado Pedido Execução", string.Format("Pedido de Execução:{0}", res.NumPedExec));
                        await Shell.Current.CurrentPage.ShowPopupAsync(mensa);

                        //Atualizar grid e chamar lista de processos
                        await CarregarProcessosEstabelecimento();
                        await Shell.Current.GoToAsync($"{nameof(Processos)}");
                    }
                }
            }
        }

       

        [RelayCommand]
        async Task ChamarReparo()
        {
            //Voltar os campos originais

            await Shell.Current.GoToAsync($"{nameof(Reparo)}");
        }

    }
}
