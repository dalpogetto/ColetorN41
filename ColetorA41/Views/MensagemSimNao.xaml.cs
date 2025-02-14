using CommunityToolkit.Maui.Views;
namespace ColetorA41.Views;

public partial class MensagemSimNao : Popup
{
	public MensagemSimNao(string header, string mensagem)
	{
		InitializeComponent();
        this.header.Text = header;
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

    private void Button_Clicked(object sender, EventArgs e)
    {
        
    }
}