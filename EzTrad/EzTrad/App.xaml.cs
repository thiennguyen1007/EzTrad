using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace EzTrad
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();

            MainPage = new MainPage();
            //MainPage = new NavigationPage(new Views.DatLenhPage.DatLenhPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
