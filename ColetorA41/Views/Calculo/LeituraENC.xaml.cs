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
        base.OnAppearing();
        Task.Run(() =>
        {
            while (!entry.IsLoaded)
            {
                Task.Delay(500).Wait();
            }
            Shell.Current.Dispatcher.Dispatch(() =>
            {
                entry.Focus();
            });
        });
    }

    protected override bool OnBackButtonPressed()
    {
        var mensa = new Mensagem("info", "Navegação", "Utilize a navegação incluída no cálculo");
        Shell.Current.CurrentPage.ShowPopup(mensa);
        return true;
    }


}