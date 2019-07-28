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
            if (regAccount.UserName != null || regAccount.EmailAddress != null || regAccount.FirstName != null ||
                regAccount.LastName != null || cdPassword.Text != null )
            { 
                System.Diagnostics.Debug.WriteLine(" Account Reg button clicked");
                regAccount.UserName = cdUserName.Text;
                regAccount.EmailAddress = cdEmail.Text;
                regAccount.FirstName = cdFirstName.Text;
                regAccount.MiddleName = cdMiddleName.Text;
                regAccount.LastName = cdLastName.Text;
                regAccount.AddressLine1 = cdAddress1.Text;
                regAccount.AddressLine2 = cdAddress2.Text;
                regAccount.City = cdCity.Text;
                regAccount.cdState = cdState.Text;
                regAccount.PostalCode = cdPostalCode.Text;
                regAccount.Phone = cdPhone.Text;
                var cpage = new cdAccounts2(regAccount,regClub, cdPassword.Text);
                await Navigation.PushModalAsync(cpage);
            }
            else 
            {
                await DisplayAlert("Enter Required Values ", " User Name, Email Address, First Name, Last Name, Password are required fields. Please enter the same.", "OK");
            }
        }

        public cdAccounts(Account pAccount, Club pClub, String pPassword)
        {
            InitializeComponent();
            regAccount = pAccount;
            regClub = pClub;
            cdUserName.Text = regAccount.UserName;
            cdEmail.Text = regAccount.EmailAddress;
            cdFirstName.Text = regAccount.FirstName;
            cdMiddleName.Text = regAccount.MiddleName;
            cdLastName.Text = regAccount.LastName;
            cdAddress1.Text = regAccount.AddressLine1;
            cdAddress2.Text = regAccount.AddressLine2;
            cdCity.Text = regAccount.City;
            cdState.Text = regAccount.cdState;
            cdPostalCode.Text = regAccount.PostalCode;
            cdPhone.Text = regAccount.Phone;
            cdPassword.Text = pPassword;
            cdConfPass.Text = pPassword;
        }
    }
}
