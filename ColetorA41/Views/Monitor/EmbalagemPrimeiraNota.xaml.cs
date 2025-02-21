using ColetorA41.ViewModel;

namespace ColetorA41.Views.Monitor;

public partial class EmbalagemPrimeiraNota : ContentPage
{
	public EmbalagemPrimeiraNota(MonitorViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}