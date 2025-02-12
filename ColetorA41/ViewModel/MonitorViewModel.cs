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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

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
                var lista = await _service.ObterProcessosEstab(this._estabSelecionado.codEstab);
                if (lista != null)
                    listaProcessosEstab.AddRange(lista);
                IsBusy = false;

            }
            catch (Exception ex)
            {

                IsBusy = false;
                await Shell.Current.DisplayAlert("Atenção", ex.Message, "OK");
            }


        }
    }
}
