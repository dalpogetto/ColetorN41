using ColetorA41.ViewModel;
using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Maui.Views;

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

    protected override void OnAppearing()
    {
      //  base.OnAppearing();
      //  this.entry.Focus();
    }

    protected override bool OnBackButtonPressed()
    {
        var mensa = new Mensagem("info", "Navega��o", "Utilize a navega��o inclu�da no c�lculo");
        Shell.Current.CurrentPage.ShowPopup(mensa);
        return true;
    }


}