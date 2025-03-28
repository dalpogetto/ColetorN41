using ColetorA41.Models;
using ColetorA41.ViewModel;

namespace ColetorA41.Views.Monitor;

public partial class Processos : ContentPage
{
    private readonly MonitorViewModel _vm;
    public Processos(MonitorViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
        _vm = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.ObterEstabelecimentos();
        

    }

}