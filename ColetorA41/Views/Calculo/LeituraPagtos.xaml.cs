using ColetorA41.ViewModel;

namespace ColetorA41.Views.Calculo;

public partial class LeituraPagtos : ContentPage
{
	public LeituraPagtos(CalculoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
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
}