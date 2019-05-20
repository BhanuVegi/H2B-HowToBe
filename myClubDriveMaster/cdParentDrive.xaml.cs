using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdParentDrive : ContentPage
    {
        void cdNext(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Next Button");
        }

        void cdPervious(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Previous Button");
        }

        void cdSubmit(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Drive Button");
        }

        void cdTrack(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Track Button");
        }

        void cdHome(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Home Button");
        }

        void cdFind(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Find Button");
        }

        public cdParentDrive()
        {
            InitializeComponent();
        }
    }
}
