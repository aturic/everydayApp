using System;
using UsefulApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsefulApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Points();
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
