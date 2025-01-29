using ColetorA41.Services;
using ColetorA41.ViewModel;

namespace ColetorA41.Views;

public partial class Login : ContentPage
{
    private readonly AuthService _authService;
    public Login(LoginPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
    
}