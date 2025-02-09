using ColetorA41.Services;
using ColetorA41.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ColetorA41.ViewModel
{
    public partial class LoginPageViewModel: BaseViewModel
    {
        private readonly TotvsService _srv;

        public LoginPageViewModel(TotvsService srv)
        {
            _srv = srv;
        }

        #region Variaveis Compartilhadas

        [ObservableProperty]
        string usuario;

        [ObservableProperty]
        string senha;

        [ObservableProperty]
        string versao = AppInfo.Current.VersionString;

        #endregion


        [RelayCommand]
        public async void Login()
        {
            this.IsBusy = true;
            await Task.Delay(2000);
            if (await _srv.Login(Usuario, Senha))
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            this.IsBusy = false;
        }

    }
}
