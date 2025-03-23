using ColetorA41.ViewModel;
using CommunityToolkit.Maui.Views;

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

    protected override bool OnBackButtonPressed()
    {
        var mensa = new Mensagem("info", "Navega��o", "Utilize a navega��o inclu�da no c�lculo");
        Shell.Current.CurrentPage.ShowPopup(mensa);
        return true;
    }

}