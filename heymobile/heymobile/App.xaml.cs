using heymobile;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace heymobile
{
	public partial class App : Application
	{
		public App ()
		{
            InitializeComponent();

            //MainPage = new MainInterface();
            MainPage = new MainInterface();
            
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
            // Handle when your app resumes
            //MainPage = new MainInterface();
		}
	}
}
