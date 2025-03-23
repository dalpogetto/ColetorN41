using System.ComponentModel;
using System.Runtime.CompilerServices;
using ColetorA41.Services;
using ColetorA41.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ColetorA41.ViewModel
{
    public partial class LoginPageViewModel: BaseViewModel
    {
        private readonly TotvsService _srv;
        private readonly AppShellViewModel _shell;

        public LoginPageViewModel(TotvsService srv, AppShellViewModel shell)
        {
            _srv = srv;
            _shell = shell;
        }

        #region Variaveis Compartilhadas

        [ObservableProperty]
        public bool isSenha = true;

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
            await Task.Delay(1000);
            if (await _srv.Login(Usuario, Senha))
            {
                _shell.UsuarioLogado = Usuario;
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            this.IsBusy = false;
        }

    }
}
