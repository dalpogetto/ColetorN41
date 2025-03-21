using ColetorA41.ViewModel;

namespace ColetorA41.Views.Monitor;

public partial class ReparoEdicaoItemReparo : ContentPage
{
	public ReparoEdicaoItemReparo(MonitorViewModel vm)
	{
		InitializeComponent();
		vm.ReparoItemDados = new Models.ReparoItem { itcodigo = "89.000.99908-4b", nrenc = 45454522, codestabel="131", CodFilial="08" };
		this.BindingContext = vm;
	}
}