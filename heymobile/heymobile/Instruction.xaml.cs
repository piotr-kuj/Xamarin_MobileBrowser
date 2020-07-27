using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace heymobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Instruction : ContentPage
	{
		public Instruction ()
		{
			InitializeComponent ();
		}
        private async void Skip_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;

            //await Navigation.PopToRootAsync();

            await Navigation.PushAsync(new MainInterface());
        }

        // NEW INSTUCTION( PAGE number) 

        private async void Next_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;


            await Navigation.PushModalAsync(new Instruction());
            //this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 1]);
        }

        private async void Previous_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;


            await Navigation.PushModalAsync(new Instruction());
            //this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 1]);
        }
    }
}