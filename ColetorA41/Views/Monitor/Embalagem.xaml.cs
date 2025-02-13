using ColetorA41.ViewModel;

namespace ColetorA41.Views.Monitor;

public partial class Embalagem : ContentPage
{
	public Embalagem(MonitorViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}