using ColetorA41.ViewModel;

namespace ColetorA41.Views.Calculo;

public partial class ResumoDetalhePago : ContentPage
{
    private readonly CalculoViewModel vm;
    public ResumoDetalhePago(CalculoViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
        this.vm = viewModel;
    }
    
}