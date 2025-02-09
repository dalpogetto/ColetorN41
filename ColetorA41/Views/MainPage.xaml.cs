using ColetorA41.ViewModel;

namespace ColetorA41.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel _vm)
	{
		InitializeComponent();
        this.BindingContext = _vm;
		
    }
}