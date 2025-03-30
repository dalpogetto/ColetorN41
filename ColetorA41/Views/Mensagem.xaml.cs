using ColetorA41.Models;
using CommunityToolkit.Maui.Views;

namespace ColetorA41.Views;

public partial class Mensagem:Popup 
{

    private string tipoMensagem; 

    public string? TipoMensagem
    { // property
        get { return tipoMensagem; } 
        set { 
            tipoMensagem = value;
            if (TipoMensagem.ToUpper() == "OK")
            {
                this.icone.Source = "success.png";
            }
            else if(TipoMensagem.ToUpper() == "INFO")
            {
                this.icone.Source = "info.png";
            }
            else
            {
                this.icone.Source = "erro.png";
            }
        } 
    }


    public Mensagem(string tipo, string header, string mensagem)
	{
		InitializeComponent();
        this.header.Text = header.ToUpper();
        this.mensagem.Text = mensagem;
        TipoMensagem = tipo;
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