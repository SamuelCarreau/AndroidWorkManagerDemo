using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Work;
using Prism;
using Prism.Ioc;
using System;
using WorkManagerDemo.Droid.Services;
using WorkManagerDemo.Interfaces;
using WorkManagerDemo.Messages;
using Xamarin.Forms;

namespace WorkManagerDemo.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            //WorkManager workManager = WorkManager.GetInstance(Xamarin.Essentials.Platform.AppContext);
            //workManager.CancelAllWork();
            InitMessagingCenterSubcription();
            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }

        private void InitMessagingCenterSubcription()
        {
            MessagingCenter.Subscribe<CalculatorWorkerRequested>(this, nameof(CalculatorWorkerRequested), message =>
            {
                WorkManager workManager = WorkManager.GetInstance(Xamarin.Essentials.Platform.AppContext);
                PeriodicWorkRequest taxWorkRequest = PeriodicWorkRequest.Builder.From<CalculatorWorker>(TimeSpan.FromMinutes(15)).Build();
                workManager.EnqueueUniquePeriodicWork(nameof(CalculatorWorker), ExistingPeriodicWorkPolicy.Replace, taxWorkRequest);
            });
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.RegisterSingleton<INotificationService, NotificationService>();
        }
    }
}

