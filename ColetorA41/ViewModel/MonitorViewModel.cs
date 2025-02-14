using System;
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
        private readonly IConfiguration _config;


        public MonitorViewModel(TotvsService totvsService,
                                IConfiguration config)
        {
            _service = totvsService;
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

        [RelayCommand]
        async Task ChamarPopup()
        {
           
        }

        public ObservableCollection<Estabelecimento> listaEstab { get; private set; } = new();
        public ObservableRangeCollection<ProcessosEstab> listaProcessosEstab { get; private set; } = new();

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
                await Shell.Current.GoToAsync($"{nameof(Reparo)}");

            else if (obj.situacao == "B")
                await Shell.Current.GoToAsync($"{nameof(Embalagem)}");

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

    }
}
