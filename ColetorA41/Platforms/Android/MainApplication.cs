using Android.App;
using Android.Runtime;
using ColetorA41.Platforms.Android.WebService;
using ColetorA41.Services;

namespace ColetorA41
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            DependencyService.Register<IHTTPClientHandlerCreationService, HTTPClientHandlerCreationService_Android>();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
