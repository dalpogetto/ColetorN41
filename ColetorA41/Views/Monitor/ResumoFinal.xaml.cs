using ColetorA41.ViewModel;

namespace ColetorA41.Views.Monitor;

public partial class ResumoFinal : ContentPage
{
	public ResumoFinal(MonitorViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}