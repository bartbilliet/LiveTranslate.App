using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SpeechTranslation.App.Services;
using SpeechTranslation.App.Droid.Services;

namespace SpeechTranslation.App.Droid
{
    [Activity(Label = "SpeechTranslation.App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int RECORD_AUDIO = 1;
        private IMicrophoneService micService;
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Xamarin.Forms.DependencyService.Register<IMicrophoneService, MicrophoneService>();
            micService = Xamarin.Forms.DependencyService.Get<IMicrophoneService>();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            switch (requestCode)
            {
                case RECORD_AUDIO:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            micService.OnRequestPermissionsResult(true);
                        }
                        else
                        {
                            micService.OnRequestPermissionsResult(false);
                        }
                    }
                    break;
            }
        }
    }
}