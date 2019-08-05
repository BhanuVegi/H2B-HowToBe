using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdHome : ContentPage
    {
        Account loginAccount = new Account();

        async void cdParentPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Parent Page Button");
            var tpage = new cdParentDrive(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }
        async void cdDriverPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Driver Page Button");
            var tpage = new cdDriverDrive(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }
        async void cdAdminPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Admin Page Button");
            var tpage = new cdAdminDrive(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }
        void cdMerchPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Rider Page Button");
            Device.OpenUri(new Uri(App.cdShopMyURL));
        }
        async void cdRiderPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Rider Page Button");
            var tpage = new cdRiderDrive(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }
        async void cdmyClubs(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked My Clubs Button");
            var tpage = new cdClubs(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }
        async void cdmyAccount(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked My Account Button");
            var tpage = new cdMyAccount(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }
        async void cdmyRiders(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked My Riders Button");
            var tpage = new cdMyRiders(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }
        async void cdmyEvents(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked My Events Button");
            var tpage = new cdMyEvents(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }
        async  void cdCreateclub(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Create Club Button");
            var tpage = new cdCreateClub(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }
        async void cdCreateStudents(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Create Students Button");
            var tpage = new cdRiderAccount(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }
        async void cdCreateEvents(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Create Events Button");
            var tpage = new cdCreateEvents(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void cdLogout(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Logout Button");
            var tpage = new MainPage();
            await Navigation.PushModalAsync(tpage);
        }

        async void cdValToken()
        {
            try { 
                    cdCallAPI mycallAPI = new cdCallAPI();
                    var resp = await mycallAPI.cdvalidateToken();
                    String stresp = (String)resp;
                    if ( stresp.Contains("Success"))
                    {
                        System.Diagnostics.Debug.WriteLine(" Valid Login");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(" Login invalid. Navigating to main page "+stresp);
                        var tpage = new MainPage();
                        await Navigation.PushModalAsync(tpage);
                    }
                }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(" In error " + ex);
            }
        }

        public cdHome(Account logAccount)
        {
            InitializeComponent();
            loginAccount = logAccount;

            cdValToken();

            System.Diagnostics.Debug.WriteLine("Length of role is " +logAccount.Role+" "+ logAccount.Role.Length);

            for (int counter=0;counter< logAccount.Role.Length;counter++ )
            {
                System.Diagnostics.Debug.WriteLine("character "+counter+" is " + logAccount.Role.Substring(counter, 1));

            }

            if (logAccount.Role.Contains("D"))
            {
                System.Diagnostics.Debug.WriteLine("Enable Driver..");
                DriverPage.IsEnabled = true;
                MerchPage.IsEnabled = true;
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
                MerchPage.IsEnabled = true;
            }
            else
            {
                AdminPage.IsEnabled = false;
                CreateEvents.IsEnabled = false;
                CreateClub.IsEnabled = false;
            }
            if (logAccount.Role.Contains("P"))
            {
                System.Diagnostics.Debug.WriteLine("Enable Parent..");
                ParentPage.IsEnabled = true;
                RegisterStudents.IsEnabled = true;
                MerchPage.IsEnabled = true;
            }
            else
            {
                ParentPage.IsEnabled = false;
                RegisterStudents.IsEnabled = false;
            }
            if (logAccount.Role.Contains("R"))
            {
                System.Diagnostics.Debug.WriteLine("Enable Rider..");
                RiderPage.IsEnabled = true;
                RegisterStudents.IsEnabled = false;
                CreateClub.IsEnabled = false;
                CreateEvents.IsEnabled = false;
                ParentPage.IsEnabled = false;
                DriverPage.IsEnabled = false;
                AdminPage.IsEnabled = false;
                MerchPage.IsEnabled = false;
                MyRiders.IsEnabled = false;
            }
            else
            {
                RiderPage.IsEnabled = false;

            }

        }
    }
}
