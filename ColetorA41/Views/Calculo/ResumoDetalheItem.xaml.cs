using ColetorA41.ViewModel;

namespace ColetorA41.Views.Calculo;

public partial class ResumoDetalheItem : ContentPage
{
	public ResumoDetalheItem(CalculoViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}