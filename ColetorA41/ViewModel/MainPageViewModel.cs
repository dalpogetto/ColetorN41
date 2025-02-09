using CommunityToolkit.Mvvm.Input;
using ColetorA41.Models;
using ColetorA41.Views.ParamEstab;
using ColetorA41.Views.Calculo;


namespace ColetorA41.ViewModel
{
    public partial class MainPageViewModel:BaseViewModel
    {
        [RelayCommand]
        async Task ChamarCalculo()
        {
            IsBusy = false;
            await Shell.Current.GoToAsync($"{nameof(EstabTec)}");

        }

        [RelayCommand]
        async Task ChamarInfoOS()
        {
            //await Shell.Current.GoToAsync($"{nameof(InformeOS)}");
        }

        [RelayCommand]
        async Task ChamarMOnitor()
        {
           // await Shell.Current.GoToAsync($"{nameof(MonitorNotas)}");
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



    }
}
