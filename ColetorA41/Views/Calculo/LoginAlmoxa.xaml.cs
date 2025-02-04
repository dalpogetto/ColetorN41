using ColetorA41.ViewModel;

namespace ColetorA41.Views.Calculo;

public partial class LoginAlmoxa : ContentPage
{
	public LoginAlmoxa(CalculoViewModel viewModel)
	{
        InitializeComponent();
        this.BindingContext = viewModel;
	}
}