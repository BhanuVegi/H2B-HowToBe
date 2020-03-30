using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdAccounts : ContentPage
    {

        Account regAccount = new Account();
        Club regClub = new Club();     

        async void cdMainPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Account Reg button clicked");
            var bpage = new MainPage();
            await Navigation.PushModalAsync(bpage);
        }

        async void cdSecond(object sender, System.EventArgs e)
        {
            if (cdUserName.Text is null || cdEmail.Text is null || cdPassword.Text is null )
            {
                await DisplayAlert("Enter Required Values ", " User Name, Email Address, First Name, Last Name, Password are required fields. Please enter the same.", "OK");
            }
            else 
            {
                System.Diagnostics.Debug.WriteLine(" Account Reg button clicked");
                regAccount.UserName = cdUserName.Text;
                regAccount.EmailAddress = cdEmail.Text;
                var cpage = new cdAccounts2(regAccount, regClub, cdPassword.Text);
                await Navigation.PushModalAsync(cpage);
            }
        }

        public cdAccounts(Account pAccount, Club pClub, String pPassword)
        {
            InitializeComponent();
            regAccount = pAccount;
            regClub = pClub;
            cdUserName.Text = regAccount.UserName;
            cdEmail.Text = regAccount.EmailAddress;
   
            cdPassword.Text = pPassword;
            cdConfPass.Text = pPassword;
        }
    }
}
