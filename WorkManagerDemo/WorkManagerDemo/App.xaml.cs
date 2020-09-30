using Prism;
using Prism.Ioc;
using WorkManagerDemo.ViewModels;
using WorkManagerDemo.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using WorkManagerDemo.Interfaces;
using WorkManagerDemo.Services;

namespace WorkManagerDemo
{
    public partial class App
    {

        public TaxWorkerListener TaxWorkerListener { get; set; }
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            TaxWorkerListener = Container.Resolve(typeof(TaxWorkerListener)) as TaxWorkerListener;
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<ITaxCalculator, TaxCalculator>();
            containerRegistry.RegisterSingleton<TaxWorkerListener>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
