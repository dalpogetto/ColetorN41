using ColetorA41.ViewModel;

namespace ColetorA41.Views.Monitor;

public partial class ReparoEdicaoItemReparo : ContentPage
{
	public ReparoEdicaoItemReparo(MonitorViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}