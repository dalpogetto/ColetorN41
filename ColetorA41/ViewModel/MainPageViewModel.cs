using CommunityToolkit.Mvvm.Input;
using ColetorA41.Models;
using ColetorA41.Views.ParamEstab;
using ColetorA41.Views.Calculo;
using ColetorA41.Views;
using ColetorA41.Views.Monitor;


namespace ColetorA41.ViewModel
{
    public partial class MainPageViewModel:BaseViewModel
    {

        #region Funcoes Compartilhadas

        [RelayCommand]
        async Task ChamarCalculo()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(EstabTec)}");
            IsBusy = false;
        }

        [RelayCommand]
        async Task ChamarInfoOS()
        {
            //await Shell.Current.GoToAsync($"{nameof(InformeOS)}");
        }

        [RelayCommand]
        async Task ChamarMonitor()
        {
            await Shell.Current.GoToAsync($"{nameof(Processos)}");
        }

        [RelayCommand]
        async Task ChamarCadEstabel()
        {
            this.IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(ParamEstabList)}");
            this.IsBusy = false;
        }

        [RelayCommand]
        async Task ChamarLeituraENC()
        {
            await Shell.Current.GoToAsync($"/{nameof(LeituraENC)}");
        }

        #endregion

    }
}
