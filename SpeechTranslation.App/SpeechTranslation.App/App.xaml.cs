using Xamarin.Forms;
using SpeechTranslation.App.Views;

namespace SpeechTranslation.App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            //Recording = false;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
