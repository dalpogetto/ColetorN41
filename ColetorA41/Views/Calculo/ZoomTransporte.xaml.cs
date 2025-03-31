namespace ColetorA41.Views.Calculo;

using ColetorA41.ViewModel;
using CommunityToolkit.Maui.Views;

public partial class ZoomTransporte : Popup
{
    private readonly CalculoViewModel viewModel;

    public ZoomTransporte(CalculoViewModel viewModel)
	{
		InitializeComponent();
        this.viewModel = viewModel;
    }
}