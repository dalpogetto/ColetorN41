using ColetorA41.ViewModel;

namespace ColetorA41.Views.Monitor;

public partial class Reparo : ContentPage
{
	public Reparo(MonitorViewModel vm)
	{
		InitializeComponent();
		
        this.BindingContext = vm;
	}
}