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

        try
        {
            if (await _srv.VerificarVersaoMobile(AppInfo.Current.VersionString))
            {

                if (await _srv.IsAuthenticatedAsync())
                {
                    // User is logged in
                    // redirect to main page
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                }
                else
                {
                    // User is not logged in 
                    // Redirect to LoginPage
                    await Shell.Current.GoToAsync($"{nameof(Login)}");
                }
            }
            else
            {
                _vm.IsError = true;
                _vm.IsBusy = false;
            }
        }
        catch (Exception ex)
        {

            //await Shell.Current.DisplayAlert("Aten��o", ex.Message, "OK");
            _vm.LabelErro = ex.Message;
            await Shell.Current.GoToAsync($"{nameof(Erro)}");
        }
        finally
        {
            IsBusy = false;
        }

        _vm.IsBusy = true;

        
        
        
    }
}