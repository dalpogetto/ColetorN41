using ColetorA41.ViewModel;

namespace ColetorA41.Views.ParamEstab;

public partial class ParamEstabEdit : ContentPage
{
	public ParamEstabEdit(ParamEstabelViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}