using ColetorA41.Services;

namespace ColetorA41.Views;

public partial class Loading : ContentPage
{
    private readonly AuthService _authService;
    public Loading(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (await _authService.IsAuthenticatedAsync())
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
}