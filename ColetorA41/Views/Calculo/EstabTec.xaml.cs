using ColetorA41.ViewModel;

namespace ColetorA41.Views.Calculo;

public partial class EstabTec : ContentPage
{
	public EstabTec(CalculoViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}