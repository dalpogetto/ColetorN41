
using ColetorA41.ViewModel;
using ColetorA41.Views;
using ColetorA41.Views.ParamEstab;
using ColetorA41.Views.Calculo;
using ColetorA41.Services;


namespace ColetorA41
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();
            

            Routing.RegisterRoute(nameof(Loading)       , typeof(Loading));
            Routing.RegisterRoute(nameof(Login)         , typeof(Login));
           // Routing.RegisterRoute(nameof(MainPage)      , typeof(MainPage));
            Routing.RegisterRoute(nameof(ParamEstabList), typeof(ParamEstabList));
            Routing.RegisterRoute(nameof(ParamEstabEdit), typeof(ParamEstabEdit));
            Routing.RegisterRoute(nameof(EstabTec)      , typeof(EstabTec));
            Routing.RegisterRoute(nameof(DadosNF)       , typeof(DadosNF));
            Routing.RegisterRoute(nameof(ExtrakitView)  , typeof(ExtrakitView));
            Routing.RegisterRoute(nameof(Resumo)        , typeof(Resumo));
            Routing.RegisterRoute(nameof(ResumoDetalhe) , typeof(ResumoDetalhe));
            Routing.RegisterRoute(nameof(ResumoDetalheItem), typeof(ResumoDetalheItem));
            Routing.RegisterRoute(nameof(LeituraENC)    , typeof(LeituraENC));
            Routing.RegisterRoute(nameof(LoginAlmoxa)   , typeof(LoginAlmoxa));
            Routing.RegisterRoute(nameof(LoadingCalculo), typeof(LoadingCalculo));
            Routing.RegisterRoute(nameof(Erro), typeof(Erro));

            //

        }
    }
}
