using ColetorA41.ViewModel;

namespace ColetorA41.Views.Calculo;

public partial class LoadingCalculo : ContentPage
{
	public LoadingCalculo(CalculoViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}