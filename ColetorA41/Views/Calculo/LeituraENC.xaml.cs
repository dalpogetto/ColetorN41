using ColetorA41.ViewModel;
using CommunityToolkit.Maui.Core.Platform;

namespace ColetorA41.Views.Calculo;

public partial class LeituraENC : ContentPage
{

    private readonly CalculoViewModel vm;
    public LeituraENC(CalculoViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
        this.vm = viewModel;
    }

    
}