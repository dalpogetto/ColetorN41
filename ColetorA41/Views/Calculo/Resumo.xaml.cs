using ColetorA41.ViewModel;

namespace ColetorA41.Views.Calculo;

public partial class Resumo : ContentPage
{
	public Resumo(CalculoViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}