using ColetorA41.ViewModel;
using CommunityToolkit.Maui.Views;

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
        //this.vm.ObterExtrakit();
    }

    protected override bool OnBackButtonPressed()
    {
        var mensa = new Mensagem("info", "Navega��o", "Utilize a navega��o inclu�da no c�lculo");
        Shell.Current.CurrentPage.ShowPopup(mensa);
        return true;
    }
}