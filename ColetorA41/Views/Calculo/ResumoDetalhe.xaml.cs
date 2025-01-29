using ColetorA41.ViewModel;

namespace ColetorA41.Views.Calculo;

public partial class ResumoDetalhe : ContentPage
{
    private readonly CalculoViewModel vm;
    public ResumoDetalhe(CalculoViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
        this.vm = viewModel;
    }
    protected override void OnAppearing()
    {

        base.OnAppearing();
        this.vm.ObterExtrakit();
    }
}