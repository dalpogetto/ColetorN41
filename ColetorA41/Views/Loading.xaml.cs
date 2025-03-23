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

        //Verificar Usuario Logado
        try
        {
            _vm.IsBusyLoading = true;
           
            //Verificar se ja existe usuario autenticado
            if (await _srv.AutenticacaoUsuario())
            {
                //Testar a versao utilizada
                if (!await _srv.VerificarVersaoMobile(AppInfo.Current.VersionString))
                {
                    _vm.IsError = true;
                    _vm.IsBusyLoading = false;
                    throw new Exception("Versão Inválida");
                }

                //Chamar Menu
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            else
            {
                //Chamar tela login
                await Shell.Current.GoToAsync($"{nameof(Login)}");
            }

            
        }
        catch (Exception ex)
        {
            //Caso aconteca algum erro ir para tela de erro e mostrar mensagem
            _vm.LabelErro = ex.Message;
            await Shell.Current.GoToAsync($"{nameof(Erro)}");
            _vm.IsBusyLoading = false;
            _srv.Logout();
           
        }
        finally
        {
            IsBusy = false;
        }

        _vm.IsBusy = true;

        
        
        
    }
}