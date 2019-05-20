using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdHome : ContentPage
    {
        void cdParentPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Parent Page Button");
        }
        void cdDriverPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Driver Page Button");
        }
        void cdAdminPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Admin Page Button");
        }
        void cdRiderPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Rider Page Button");
        }
        void cdmyClubs(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked My Clubs Button");
        }
        void cdmyAccount(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked My Account Button");
        }
        void cdmyRiders(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked My Riders Button");
        }
        void cdCreateclub(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Create Club Button");
        }
        void cdCreateStudents(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Create Students Button");
        }
        void cdCreateEvents(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Create Events Button");
        }

        async void cdLogout(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Logout Button");
            var tpage = new MainPage();
            await Navigation.PushModalAsync(tpage);
        }

        public cdHome(Account logAccount)
        {
            InitializeComponent();

            System.Diagnostics.Debug.WriteLine("Length of role is " +logAccount.Role+" "+ logAccount.Role.Length);

            for (int counter=0;counter< logAccount.Role.Length;counter++ )
            {
                System.Diagnostics.Debug.WriteLine("character "+counter+" is " + logAccount.Role.Substring(counter, 1));

            }

            if (logAccount.Role.Contains("D"))
            {
                System.Diagnostics.Debug.WriteLine("Enable Driver..");
                DriverPage.IsEnabled = true;
            }
            else
            {
                DriverPage.IsEnabled = false;
            }
            if (logAccount.Role.Contains("A"))
            {
                System.Diagnostics.Debug.WriteLine("Enable Admin..");
                AdminPage.IsEnabled = true;
                CreateEvents.IsEnabled = true;
                CreateClub.IsEnabled = true;
            }
            else
            {
                AdminPage.IsEnabled = false;
                CreateEvents.IsEnabled = false;
                CreateClub.IsEnabled = false;
            }
            if (logAccount.Role.Contains("P"))
            {
                System.Diagnostics.Debug.WriteLine("Enable Driver..");
                ParentPage.IsEnabled = true;
                RegisterStudents.IsEnabled = true;
            }
            else
            {
                ParentPage.IsEnabled = false;
                RegisterStudents.IsEnabled = false;
            }
            if (logAccount.Role.Contains("R"))
            {
                System.Diagnostics.Debug.WriteLine("Enable Driver..");
                RiderPage.IsEnabled = true;
            }
            else
            {
                RiderPage.IsEnabled = false;
            }

        }
    }
}
