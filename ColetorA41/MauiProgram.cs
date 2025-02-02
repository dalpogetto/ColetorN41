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
                httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BASE_URL"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", builder.Configuration.GetValue<string>("USUARIO_SENHA_BASE64"));
                httpClient.DefaultRequestHeaders.Add("x-totvs-server-alias", builder.Configuration.GetValue<string>("ALIAS_APPSERVER"));
                httpClient.DefaultRequestHeaders.Add("CompanyId", builder.Configuration.GetValue<string>("EMPRESA_PADRAO"));
                httpClient.Timeout = Timeout.InfiniteTimeSpan;
              
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
            });

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
