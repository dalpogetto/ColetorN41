using ColetorA41.Services;
using ColetorA41.ViewModel;
using ColetorA41.Views;
using ColetorA41.Views.Calculo;
using ColetorA41.Views.ParamEstab;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
//using ColetorA41.Pages;

namespace ColetorA41
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            

#if DEBUG
            builder.Logging.AddDebug();
#endif

            //Services
            builder.Services.AddTransient<TotvsService>();
            builder.Services.AddTransient<TotvsService46>();
            builder.Services.AddTransient<AuthService>();

            //Views
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<Loading>();
            builder.Services.AddTransient<ParamEstabList>();
            builder.Services.AddTransient<ParamEstabEdit>();
            builder.Services.AddTransient<EstabTec>();
            builder.Services.AddTransient<DadosNF>();
            builder.Services.AddTransient<ExtrakitView>();
            builder.Services.AddTransient<Resumo>();
            builder.Services.AddTransient<ResumoDetalhe>();

            //ViewModels
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddSingleton<CalculoViewModel>();
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddSingleton<ParamEstabelViewModel>();

            return builder.Build();
        }
    }
}
