using ColetorA41.ViewModel;

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
            await Shell.Current.DisplayAlert("Erro!", "Dados do estabelecimento e técnicos não preenchidos corretamente", "OK");
            await Shell.Current.GoToAsync($"/{nameof(EstabTec)}");

            return;
        }
        this.vm.ObterParametrosEstab();
        */
    }

}