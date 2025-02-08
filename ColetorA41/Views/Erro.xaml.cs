using ColetorA41.ViewModel;

namespace ColetorA41.Views;

public partial class Erro : ContentPage
{
	public Erro(CalculoViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}