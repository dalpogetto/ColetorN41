using ColetorA41.ViewModel;
using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Maui.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ColetorA41.Views.Calculo;

public partial class EstabTec : ContentPage
{
    private readonly CalculoViewModel _vm;
    public EstabTec(CalculoViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.ObterEstabelecimentos();
    }

    protected override bool OnBackButtonPressed()
    {
        var mensa = new Mensagem("info", "Navega��o", "Utilize a navega��o inclu�da no c�lculo");
        Shell.Current.CurrentPage.ShowPopup(mensa);
        return true;
    }
   
}