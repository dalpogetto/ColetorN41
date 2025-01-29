using ColetorA41.Services;
using ColetorA41.ViewModel;

namespace ColetorA41
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell(new AppShellViewModel(new AuthService()));
        }
    }
}
