using ColetorA41.ViewModel;

namespace ColetorA41.Views.ParamEstab;

public partial class ParamEstabList : ContentPage
{
	public ParamEstabList(ParamEstabelViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}