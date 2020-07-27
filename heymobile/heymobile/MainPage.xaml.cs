using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace heymobile
{
	public partial class MainPage : ContentPage
	{

        public MainPage()
        {

            InitializeComponent();
		}

        async void ShowLoginDialog()
        {
            var page = new MainPage();

            await Navigation.PushModalAsync(page);
        }

        private async void Start_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;

            await Navigation.PushModalAsync(new Instruction());

            {
                /*
                if (button.Text != "Start")
                {
                    button.Text = "Start";
                }
                else
                {
                    button.Text = "Started";
                }
                */
            }

        }

        private async void Skip_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            for (int i = 0; i < Navigation.NavigationStack.Count; i++)
            {
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count]);
            }
            await Navigation.PushModalAsync(new MainInterface());
            {
                /*
                if (button.Text != "Skip")
                {
                    button.Text = "Skip";
                }
                else
                {
                    button.Text = "Skipped";
                }
                */
            }

        }
    }
}
