using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdDriverDrive : ContentPage
    {
        void cdNext(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Next Button");
        }

        void cdPervious(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Previous Button");
        }

        void cdDrive(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Drive Button");
        }


        void cdHome(object sender, System.EventArgs e)
        {
           System.Diagnostics.Debug.WriteLine(" Clicked Home Button");
        }

        public cdDriverDrive()
        {
            InitializeComponent();
        }
    }
}
