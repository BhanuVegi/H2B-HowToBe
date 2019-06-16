using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdMyAccount : ContentPage
    {
        Account regAccount = new Account();

        async void cdMainPage(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Main Page button clicked");
            var bpage = new MainPage();
            await Navigation.PushModalAsync(bpage);
        }

        async void cdSubmit(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Submit button clicked");
            int firstUpdate = 0;
            int secondUpdate = 0;
            cdReadError myerror = new cdReadError();
            cdUpdateAccount updateAccount = new cdUpdateAccount();
            cdUpdateAccount updateAccount2 = new cdUpdateAccount();

            updateAccount.AccountID = regAccount.AccountID;

            if ( cdEmail.Text == regAccount.EmailAddress )
            {
                updateAccount.ColumnName = "EmailAddress";
                updateAccount.ColumnValue = cdEmail.Text;
                firstUpdate = 1;
            }
            if (cdAddress1.Text == regAccount.AddressLine1)
            {
                updateAccount.ColumnName3 = "AddressLine1";
                updateAccount.ColumnValue3 = cdAddress1.Text;
                firstUpdate = 1;
            }
            if (cdCity.Text == regAccount.City)
            {
                updateAccount.ColumnName4 = "City";
                updateAccount.ColumnValue4 = cdCity.Text;
                firstUpdate = 1;
            }
            if (cdState.Text == regAccount.cdState)
            {
                updateAccount.ColumnName1 = "cdState";
                updateAccount.ColumnValue1 = cdState.Text;
                firstUpdate = 1;
            }
            if (cdPostalCode.Text == regAccount.PostalCode)
            {
                updateAccount.ColumnName2 = "PostalCode";
                updateAccount.ColumnValue2 = cdPostalCode.Text;
                firstUpdate = 1;
            }
            if (cdFirstName.Text == regAccount.FirstName)
            {
                updateAccount2.ColumnName = "FirstName";
                updateAccount2.ColumnValue = cdFirstName.Text;
                secondUpdate = 1;
            }
            if (cdLastName.Text == regAccount.LastName)
            {
                updateAccount2.ColumnName1 = "LastName";
                updateAccount2.ColumnValue1 = cdFirstName.Text;
                secondUpdate = 1;
            }
            if (cdPhone.Text == regAccount.Phone)
            {
                updateAccount2.ColumnName2 = "Phone";
                updateAccount2.ColumnValue2 = cdPhone.Text;
                secondUpdate = 1;
            }
            if (cdCheckAdmin.IsChecked == true && regAccount.Role.Contains("A") == false )
            {
                updateAccount2.ColumnName3 = "AccountStatus";
                updateAccount2.ColumnValue3 = "NotApproved";
                updateAccount2.ColumnName3 = "Role";
                updateAccount2.ColumnValue3 = regAccount.Role+"A";
                secondUpdate = 1;
            }

            System.Diagnostics.Debug.WriteLine(" Before calling Post API ");
            cdCallAPI mycallAPI = new cdCallAPI();

            if ( firstUpdate == 1)
            { 
                var jsresponse = await mycallAPI.cdcallAccountsPOST(updateAccount);

                System.Diagnostics.Debug.WriteLine(" After calling Post API ");
                if (jsresponse.ToString().Contains("ValidationException"))
                {
                    System.Diagnostics.Debug.WriteLine(" Post API Call failed " + jsresponse);
                    myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                    updateStatus.Text = "Update Failed. " + myerror.message;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(" Post API Call Successful");
                    updateStatus.Text = "Update Successful";
                }
            }
            if (secondUpdate == 1)
            {

                var jsresponse = await mycallAPI.cdcallAccountsPOST(updateAccount2);

                System.Diagnostics.Debug.WriteLine(" After calling Post API ");
                if (jsresponse.ToString().Contains("ValidationException"))
                {
                    System.Diagnostics.Debug.WriteLine(" Post API Call failed " + jsresponse);
                    myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                    updateStatus.Text = "Update Failed. " + myerror.message;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(" Post API Call Successful");
                    updateStatus.Text = "Update Successful";
                }
            }

        }

        public cdMyAccount(Account loginAccount)
        {
            InitializeComponent();
            regAccount = loginAccount;
            cdUserName.Text = "User Name: "+ regAccount.UserName;
            cdEmail.Text = regAccount.EmailAddress;
            cdFirstName.Text = regAccount.FirstName;
            cdLastName.Text = regAccount.LastName;
            cdAddress1.Text = regAccount.AddressLine1;
            cdCity.Text = regAccount.City;
            cdState.Text = regAccount.cdState;
            cdPostalCode.Text = regAccount.PostalCode;
            cdPhone.Text = regAccount.Phone;
            if (regAccount.Role.Contains("D"))
            {
                System.Diagnostics.Debug.WriteLine("Populate Admin..");
                cdCheckAdmin.IsChecked = true;
            }

        }
    }
}
