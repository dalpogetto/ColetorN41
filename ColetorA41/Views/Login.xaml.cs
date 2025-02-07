using ColetorA41.Services;
using ColetorA41.ViewModel;

namespace ColetorA41.Views;

public partial class Login : ContentPage
{
    public Login(LoginPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
    
}