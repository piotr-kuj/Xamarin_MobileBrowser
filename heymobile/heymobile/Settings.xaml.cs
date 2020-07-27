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
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent ();


		}

        private void Notification_onChanged(object sender, ToggledEventArgs e)
        {
            if (notificationCell.On == true)
            {
                //notificationCell.Text = "Yes";
            }
            else
            {
               // notificationCell.Text = "No";
            }
        }



    }
}