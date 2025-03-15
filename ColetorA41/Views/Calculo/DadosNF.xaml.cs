using ColetorA41.ViewModel;
using CommunityToolkit.Maui.Views;

namespace ColetorA41.Views.Calculo;

public partial class DadosNF : ContentPage
{
    private readonly CalculoViewModel vm;
    public DadosNF(CalculoViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
        this.vm = viewModel;
    }

    protected override async void OnAppearing()
    {

        base.OnAppearing();
        /*
        if ((vm.EstabSelecionado == null) || (vm.TecnicoSelecionado == null))
        {
            await Shell.Current.DisplayAlert("Erro!", "Dados do estabelecimento e t�cnicos n�o preenchidos corretamente", "OK");
            await Shell.Current.GoToAsync($"/{nameof(EstabTec)}");

            return;
        }
        this.vm.ObterParametrosEstab();
        */
    }

    protected override bool OnBackButtonPressed()
    {
        var mensa = new Mensagem("info", "Navega��o", "Utilize a navega��o inclu�da no c�lculo");
        Shell.Current.CurrentPage.ShowPopup(mensa);
        return true;
    }

}