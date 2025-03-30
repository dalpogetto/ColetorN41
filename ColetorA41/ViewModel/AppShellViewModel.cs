using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ColetorA41.Models;

using ColetorA41.Services;
using ColetorA41.Views;

namespace ColetorA41.ViewModel
{

    public partial class AppShellViewModel: BaseViewModel
    {
        private const string InfoLogin = "UsuarioSenhaBase64";
        private readonly TotvsService _service;
        public AppShellViewModel(TotvsService srv)
        {
            _service = srv;
        }

        public AppShellViewModel() { }

        [RelayCommand]
        public async void Logout()
        {
            //Limpar dados usuarios
            Preferences.Default.Clear();
            this.UsuarioLogado = string.Empty;

            //Fechar Janela e aguardar 
            Shell.Current.FlyoutIsPresented = false;
            await Task.Delay(500);

            //Chamar Tela de Login
            await Shell.Current.GoToAsync($"{nameof(Login)}");
        }

        [ObservableProperty]
        string usuarioLogado;

        [ObservableProperty]
        Estabelecimento estabSelecionado;

        [ObservableProperty]
        Tecnico tecnicoSelecionado;

        [ObservableProperty]
        Processo processoSelecionado;

        public string? ObterVersao { get; set; } = AppInfo.Current.VersionString;


    }
}
