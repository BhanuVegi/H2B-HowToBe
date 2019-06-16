using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdMyRiders : ContentPage
    {
        Account loginAccount = new Account();
        getAccounts mystudAccounts = new getAccounts();
        int maxarray = -1;
        int counter = 0;

        void cdNext(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Next Button");
            counter = counter + 1;

            UserName.Text = "User Name: " + mystudAccounts.Account[counter].UserName;
            cdEmail.Text = mystudAccounts.Account[counter].EmailAddress;
            cdFirstName.Text = mystudAccounts.Account[counter].FirstName;
            cdMiddleName.Text = mystudAccounts.Account[counter].MiddleName;
            cdLastName.Text = mystudAccounts.Account[counter].LastName;
            cdAddress1.Text = mystudAccounts.Account[counter].AddressLine1;
            cdAddress2.Text = mystudAccounts.Account[counter].AddressLine2;
            cdCity.Text = mystudAccounts.Account[counter].City;
            cdState.Text = mystudAccounts.Account[counter].cdState;
            cdPostalCode.Text = mystudAccounts.Account[counter].PostalCode;
            cdPhone.Text = mystudAccounts.Account[counter].Phone;

            if (counter >= maxarray)
            {
                NextButton.IsEnabled = false;
                if (counter != 0)
                {
                    PreviousButton.IsEnabled = true;
                }
                else
                {
                    PreviousButton.IsEnabled = false;
                }
            }
            else
            {
                NextButton.IsEnabled = true;
                if (counter != 0)
                {
                    PreviousButton.IsEnabled = true;
                }
                else
                {
                    PreviousButton.IsEnabled = false;
                }
            }
        }

        void cdPervious(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Previous Button");
            counter = counter - 1;

            UserName.Text = "User Name: " + mystudAccounts.Account[counter].UserName;
            cdEmail.Text = mystudAccounts.Account[counter].EmailAddress;
            cdFirstName.Text = mystudAccounts.Account[counter].FirstName;
            cdMiddleName.Text = mystudAccounts.Account[counter].MiddleName;
            cdLastName.Text = mystudAccounts.Account[counter].LastName;
            cdAddress1.Text = mystudAccounts.Account[counter].AddressLine1;
            cdAddress2.Text = mystudAccounts.Account[counter].AddressLine2;
            cdCity.Text = mystudAccounts.Account[counter].City;
            cdState.Text = mystudAccounts.Account[counter].cdState;
            cdPostalCode.Text = mystudAccounts.Account[counter].PostalCode;
            cdPhone.Text = mystudAccounts.Account[counter].Phone;

            if (counter == 0)
            {
                PreviousButton.IsEnabled = false;
                if (counter < maxarray)
                {
                    NextButton.IsEnabled = true;
                }
                else
                {
                    NextButton.IsEnabled = false;
                }
            }
            else
            {
                PreviousButton.IsEnabled = true;
                if (counter < maxarray)
                {
                    NextButton.IsEnabled = true;
                }
                else
                {
                    NextButton.IsEnabled = false;
                }
            }

        }

        async void cdSubmit(object sender, System.EventArgs e)
        {
            cdReadError myerror = new cdReadError();
            cdCallAPI mycallAPI = new cdCallAPI();

            cdUpdateAccount updateAddress = new cdUpdateAccount();
            updateAddress.AccountID = mystudAccounts.Account[counter].AccountID;
            updateAddress.ColumnName = "FirstName";
            updateAddress.ColumnValue = cdFirstName.Text;
            updateAddress.ColumnName1 = "LastName";
            updateAddress.ColumnValue1 = cdLastName.Text;
            updateAddress.ColumnName2 = "MiddleName";
            updateAddress.ColumnValue2 = cdMiddleName.Text;
            updateAddress.ColumnName3 = "cdPhone";
            updateAddress.ColumnValue3 = cdPhone.Text;
            updateAddress.ColumnName4 = "EmailAddress";
            updateAddress.ColumnValue4 = cdEmail.Text;

            var jsresponse = await mycallAPI.cdcallAccountsPOST(updateAddress);

            System.Diagnostics.Debug.WriteLine(" After calling Post API 1 ");
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

            updateAddress.ColumnName = "AddressLine1";
            updateAddress.ColumnValue = cdAddress1.Text;
            updateAddress.ColumnName1 = "AddressLine2";
            updateAddress.ColumnValue1 = cdAddress2.Text;
            updateAddress.ColumnName2 = "City";
            updateAddress.ColumnValue2 = cdCity.Text;
            updateAddress.ColumnName3 = "cdState";
            updateAddress.ColumnValue3 = cdState.Text;
            updateAddress.ColumnName4 = "PostalCode";
            updateAddress.ColumnValue4 = cdPostalCode.Text;

            System.Diagnostics.Debug.WriteLine(" Before calling Post API ");

            jsresponse = await mycallAPI.cdcallAccountsPOST(updateAddress);

            System.Diagnostics.Debug.WriteLine(" After calling Post API 2 ");
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

            // System.Diagnostics.Debug.WriteLine(" Update Response is "+jsresponse);

        }

        async void cdHome(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Home Button");
            var tpage = new cdHome(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void getStudentInfo(Account logAccount)
        {
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "ParentIDindex";
            qryAcct.ColName = "ParentID";
            qryAcct.ColValue = logAccount.UserName;

            getAccounts myStudentArray = new getAccounts();
            cdCallAPI mycallAPI = new cdCallAPI();

            var jsreponse = await mycallAPI.cdcallAccountsGET(qryAcct);
            myStudentArray = JsonConvert.DeserializeObject<getAccounts>((string)jsreponse);
            mystudAccounts = myStudentArray;

            try
            {
                foreach (var stacc in myStudentArray.Account)
                {
                    maxarray = maxarray + 1;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
            }

            UserName.Text = "User Name: " + mystudAccounts.Account[0].UserName;
            cdEmail.Text = mystudAccounts.Account[0].EmailAddress;
            cdFirstName.Text = mystudAccounts.Account[0].FirstName;
            cdMiddleName.Text = mystudAccounts.Account[0].MiddleName;
            cdLastName.Text = mystudAccounts.Account[0].LastName;
            cdAddress1.Text = mystudAccounts.Account[0].AddressLine1;
            cdAddress2.Text = mystudAccounts.Account[0].AddressLine2;
            cdCity.Text = mystudAccounts.Account[0].City;
            cdState.Text = mystudAccounts.Account[0].cdState;
            cdPostalCode.Text = mystudAccounts.Account[0].PostalCode;
            cdPhone.Text = mystudAccounts.Account[0].Phone;

            System.Diagnostics.Debug.WriteLine(" Max Array is " + maxarray);

            if (counter == maxarray)
            {
                PreviousButton.IsEnabled = false;
                NextButton.IsEnabled = false;
            }
            else
            {
                PreviousButton.IsEnabled = false;
                NextButton.IsEnabled = true;
            }

        }
        public cdMyRiders(Account myAccount)
        {
            InitializeComponent();
            loginAccount = myAccount;
            getStudentInfo(loginAccount);
        }
    }
}
