using ColetorA41.Services;
using ColetorA41.ViewModel;

namespace ColetorA41
{
    public partial class App : Application
    {
        public App(AppShellViewModel vm)
        {
            InitializeComponent();

            MainPage = new AppShell(vm);
        }
    }
}
