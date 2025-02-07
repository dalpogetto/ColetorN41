using ColetorA41.Services;
using ColetorA41.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ColetorA41.ViewModel
{
    public partial class LoginPageViewModel: BaseViewModel
    {
        private readonly AuthService _authService;

        public LoginPageViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [ObservableProperty]
        string usuario;

        [ObservableProperty]
        string senha;

        [ObservableProperty]
        string versao = AppInfo.Current.VersionString;

        [RelayCommand]
        public async void Login()
        {
            this.IsBusy = true;
            await Task.Delay(2000);
            if (await _authService.Login(Usuario, Senha))
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            this.IsBusy = false;
        }

    }
}
