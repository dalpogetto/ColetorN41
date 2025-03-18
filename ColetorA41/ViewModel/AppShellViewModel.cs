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
        public void Logout()
        {
            Preferences.Default.Clear();
            Shell.Current.GoToAsync($"{nameof(Login)}");
        }

        [ObservableProperty]
        string usuarioLogado;

        [ObservableProperty]
        Estabelecimento estabSelecionado;

        [ObservableProperty]
        Tecnico tecnicoSelecionado;

        [ObservableProperty]
        Processo processoSelecionado;

        public string ObterVersao { get; set; } = AppInfo.Current.VersionString;


    }
}
