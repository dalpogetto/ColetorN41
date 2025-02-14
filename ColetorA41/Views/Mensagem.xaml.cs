using ColetorA41.Models;
using CommunityToolkit.Maui.Views;

namespace ColetorA41.Views;

public partial class Mensagem:Popup 
{
    

	public Mensagem(string header, string mensagem)
	{
		InitializeComponent();
        this.header.Text = header.ToUpper();
        this.mensagem.Text = mensagem;
        
        
      
	}

    async void OnYesButtonClicked(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(true, cts.Token);
    }

    async void OnNoButtonClicked(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(false, cts.Token);
    }
}