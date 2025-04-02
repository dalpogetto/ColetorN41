using ColetorA41.ViewModel;

namespace ColetorA41.Views.Calculo;

public partial class LeituraPagtos : ContentPage
{
	public LeituraPagtos(CalculoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}