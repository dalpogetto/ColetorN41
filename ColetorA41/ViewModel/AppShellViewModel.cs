using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ColetorA41.Models;

using ColetorA41.Services;
using ColetorA41.Views;

namespace ColetorA41.ViewModel
{

    public partial class AppShellViewModel: BaseViewModel
    {
        private readonly AuthService _authService;
        public AppShellViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        public void Logout()
        {
            _authService.Logout();
            Shell.Current.GoToAsync($"//{nameof(Login)}");
        }

        [ObservableProperty]
        Estabelecimento estabSelecionado;

        [ObservableProperty]
        Tecnico tecnicoSelecionado;

        [ObservableProperty]
        Processo processoSelecionado;


    }
}
