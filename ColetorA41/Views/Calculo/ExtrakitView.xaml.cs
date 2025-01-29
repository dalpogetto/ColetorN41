using ColetorA41.ViewModel;

namespace ColetorA41.Views.Calculo;

public partial class ExtrakitView : ContentPage
{
    private readonly CalculoViewModel vm;
    public ExtrakitView(CalculoViewModel viewModel)
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