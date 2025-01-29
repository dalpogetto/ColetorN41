using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ColetorA41.Models;
//using ColetorA41.Pages;
using ColetorA41.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ColetorA41.Views.ParamEstab;

namespace ColetorA41.ViewModel
{
    public partial class ParamEstabelViewModel : BaseViewModel
    {
        private readonly TotvsService _service;
        private readonly TotvsService46 _service46;


        public ParamEstabelViewModel(TotvsService totvsService,
                                     TotvsService46 totvsService46)
        {
            _service = totvsService;
            _service46 = totvsService46;
            this.ObterParamEstabelecimentos();
            this.ObterEstabelecimentos();
            this.ObterTransporte();

        }

        [ObservableProperty]
        Transporte transpEntraSelecionado;

        [ObservableProperty]
        Transporte transpSaidaSelecionado;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(listaParamEstab))]
        ParamEstabel paramEstab;

        [ObservableProperty]
        Estabelecimento estabSelecionado;

        public ObservableCollection<ParamEstabel> listaParamEstab { get; private set; } = new();
        public ObservableCollection<Estabelecimento> listaEstab { get; private set; } = new();

        public async void ObterParamEstabelecimentos()
        {
            this.IsBusy = true;
            var lista = await _service.ObterParamEstabelecimentos();

            this.listaParamEstab.Clear();
            foreach (var item in lista)
            {
                this.listaParamEstab.Add(item);
            }
            this.IsBusy = false;
        }

        public async void ObterEstabelecimentos()
        {
            this.IsBusy = true;
            var lista = await _service.ObterEstabelecimentos();

            this.listaEstab.Clear();
            foreach (var item in lista)
            {
                this.listaEstab.Add(item);
            }
            this.IsBusy = false;
        }


        public ObservableCollection<Transporte> listaTransporte { get; private set; } = new();
        public async void ObterTransporte()
        {
            this.IsBusy = true;
            var lista = await _service.ObterTransportes();

            this.listaTransporte.Clear();
            foreach (var item in lista)
            {
                this.listaTransporte.Add(item);
            }
            this.IsBusy = false;
        }

        public async Task EliminarRegistro(object obj)
        {
            bool lok = await Application.Current.MainPage.DisplayAlert("CONFIRMAÇÃO", "Deseja deletar o registro ?", "Yes", "No");
            if (!lok) return;

            if (obj == null) return;

            this.IsBusy = true;
            await _service.EliminarParametrosEstabel((obj as ParamEstabel).codEstabel);

            //Eliminar da lista 
            this.ObterParamEstabelecimentos();

            this.ParamEstab = null;
            await Shell.Current.GoToAsync("..", true);
        }

        public async Task SalvarRegistro()
        {
            this.IsBusy = true;
            this.ParamEstab.codEstabel = this.EstabSelecionado != null ? this.EstabSelecionado.codEstab: string.Empty;
            this.ParamEstab.codTranspEntra = this.TranspEntraSelecionado != null ? this.TranspEntraSelecionado.codTransp: 1;
            this.ParamEstab.codTranspSai = this.TranspSaidaSelecionado != null ?  this.TranspSaidaSelecionado.codTransp: 1;
            await _service.SalvarParametrosEstab(this.ParamEstab);

            //Atualizar o grid
            this.ObterParamEstabelecimentos();
        }

        [RelayCommand]
        public async Task Salvar()
        {
            await this.SalvarRegistro();
            await Shell.Current.GoToAsync("/ParamEstabList", true);
        }

        [RelayCommand]
        public async Task Eliminar(object obj) => await EliminarRegistro(obj);


        [RelayCommand]
        public void Editar(object obj) => AbrirJanela(obj);

        [RelayCommand]
        public async Task Cancelar()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        public async void AbrirJanela(object obj)
        {
            //Edicao
            if (obj != null)
            {
                this.ParamEstab = obj as ParamEstabel;
                //Posicionar Estabelecimento
                this.EstabSelecionado = this.listaEstab.Where(item => item.codEstab == ParamEstab.codEstabel).FirstOrDefault();

                //Posicionar Transporte Entrada
                this.TranspEntraSelecionado = this.listaTransporte.Where(item => item.codTransp == ParamEstab.codTranspEntra).FirstOrDefault();

                //Posicionar Transporte Saida
                this.TranspSaidaSelecionado = this.listaTransporte.Where(item => item.codTransp == ParamEstab.codTranspSai).FirstOrDefault();

            }
            //Novo
            else
            {
                this.ParamEstab = null;
                this.EstabSelecionado = this.listaEstab.FirstOrDefault(item=>item.codEstab=="101");
                this.TranspEntraSelecionado = this.listaTransporte.FirstOrDefault(item=>item.codTransp==1);
                this.TranspSaidaSelecionado = this.listaTransporte.FirstOrDefault(item => item.codTransp == 1);
                this.ParamEstab = new ParamEstabel();
            }
            await Shell.Current.GoToAsync($"{nameof(ParamEstabEdit)}");
        }


        //public ICommand Editar { get; set; } = new RelayCommand<object>(async (obj) => {
        //    await Shell.Current.GoToAsync($"{nameof(CadEstabelEdit)}", true);
        //});


    }
}
