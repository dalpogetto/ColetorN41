using ColetorA41.ViewModel;

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
}