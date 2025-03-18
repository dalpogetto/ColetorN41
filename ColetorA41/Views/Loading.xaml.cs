using ColetorA41.Services;
using ColetorA41.ViewModel;

namespace ColetorA41.Views;

public partial class Loading : ContentPage
{
    private readonly TotvsService _srv;
    private readonly CalculoViewModel _vm;
    public Loading(TotvsService srv, CalculoViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
        _srv = srv;
        _vm = vm;
    }
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        //Na inicializacao a rotina verifica a versao utilizada para direcionar para 
        //MainPage ou Login e caso aconteca qualquer erro interrompe a navegacao e 
        //mostra a tela de Erro
        try
        {
            _vm.IsBusyLoading = true;
           
            if (await _srv.IsAuthenticatedAsync())
            {
                if (!await _srv.VerificarVersaoMobile(AppInfo.Current.VersionString))
                {
                    _vm.IsError = true;
                    _vm.IsBusyLoading = false;
                    throw new Exception("Versão Inválida");
                }
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            else
            {
                // User is not logged in 
                // Redirect to LoginPage
                await Shell.Current.GoToAsync($"{nameof(Login)}");
            }

            
        }
        catch (Exception ex)
        {
            _vm.LabelErro = ex.Message;
            await Shell.Current.GoToAsync($"{nameof(Erro)}");
            _vm.IsBusyLoading = false;
           
        }
        finally
        {
            IsBusy = false;
        }

        _vm.IsBusy = true;

        
        
        
    }
}