using System.Net.Http.Headers;
using System.Net.Http;
using ColetorA41.Services;
using ColetorA41.ViewModel;
using ColetorA41.Views;
using ColetorA41.Views.Calculo;
using ColetorA41.Views.ParamEstab;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ColetorA41.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;
//using ColetorA41.Pages;

namespace ColetorA41
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            NullabilityInfoContext kk = new();
           
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Registrar Arquivos de configuracao
            builder.AddAppSettings();

            


#if DEBUG
            builder.Logging.AddDebug();
#endif

            //Services
            builder.Services.AddTransient<TotvsService>();
            builder.Services.AddTransient<TotvsService46>();
            builder.Services.AddTransient<AuthService>();
            
            //HttpClient
            builder.Services.AddHttpClient("coletor", httpClient =>
            {

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
            });

            //Views
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<LoginAlmoxa>();
            builder.Services.AddTransient<Loading>();
            builder.Services.AddTransient<ParamEstabList>();
            builder.Services.AddTransient<ParamEstabEdit>();
            builder.Services.AddTransient<EstabTec>();
            builder.Services.AddTransient<DadosNF>();
            builder.Services.AddTransient<ExtrakitView>();
            builder.Services.AddTransient<LeituraENC>();
            builder.Services.AddTransient<ColetorA41.Views.Calculo.Resumo>();
            builder.Services.AddTransient<ResumoDetalhe>();

            //ViewModels
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddSingleton<CalculoViewModel>();
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddSingleton<ParamEstabelViewModel>();

            return builder.Build();
        }

        private static void AddAppSettings(this MauiAppBuilder builder)
        {
            

#if DEBUG
            builder.AddJsonSettings("appsettings.desenv.json");
#endif

#if !DEBUG
            builder.AddJsonSettings("appsettings.prod.json");
#endif
        }

        private static void AddJsonSettings(this MauiAppBuilder builder, string fileName)
        {
            using Stream stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"ColetorA41.{fileName}");

            if (stream != null)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
                builder.Configuration.AddConfiguration(config);
            }
        }
    }
}
