using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdAdminDrive : ContentPage
    {
        Account loginAccount = new Account();
        getAccounts mystudArray = new getAccounts();
        int maxarray = -1;
        int counter = 0;

        void cdNext(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Next Button");
            String myrole = " ";
            counter = counter + 1;
            ApplicantName.Text = "Applicant Name: " + mystudArray.Account[counter].FirstName + " " + mystudArray.Account[counter].LastName;

            if (mystudArray.Account[counter].Role.Contains("D"))
            {
                myrole = myrole+" Driver ";
            }
            if (mystudArray.Account[counter].Role.Contains("P"))
            {
                myrole = myrole+" Parent ";
            }
            if (mystudArray.Account[counter].Role.Contains("A"))
            {
                myrole = myrole+" Admin ";
            }

            ApplicantType.Text = "Applicant Role: "+myrole;

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

        void cdPrevious(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Track Button");
            counter = counter - 1;
            String myrole = " ";
            ApplicantName.Text = "Applicant Name: " + mystudArray.Account[counter].FirstName + " " + mystudArray.Account[counter].LastName;

            if (mystudArray.Account[counter].Role.Contains("D"))
            {
                myrole = myrole + " Driver ";
            }
            if (mystudArray.Account[counter].Role.Contains("P"))
            {
                myrole = myrole + " Parent ";
            }
            if (mystudArray.Account[counter].Role.Contains("A"))
            {
                myrole = myrole + " Admin ";
            }

            ApplicantType.Text = "Applicant Role: "+myrole;

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
            System.Diagnostics.Debug.WriteLine(" Clicked Submit Button");
            cdReadError myerror = new cdReadError();
            cdUpdateAccount updatemyAccount = new cdUpdateAccount();
            updatemyAccount.AccountID = mystudArray.Account[counter].AccountID;
            updatemyAccount.ColumnName = "AccountStatus";
            updatemyAccount.ColumnValue = picker.SelectedItem.ToString();
            updatemyAccount.ColumnName1 = "Attr6";
            updatemyAccount.ColumnValue1 = mystudArray.Account[counter].Attr6;
            updatemyAccount.ColumnName2 = "Attr7";
            updatemyAccount.ColumnValue2 = mystudArray.Account[counter].Attr7;
            updatemyAccount.ColumnName3 = "Attr8";
            updatemyAccount.ColumnValue3 = mystudArray.Account[counter].Attr8;
            updatemyAccount.ColumnName4 = "Attr9";
            updatemyAccount.ColumnValue4 = mystudArray.Account[counter].Attr9;

            System.Diagnostics.Debug.WriteLine(" Before calling Post API ");
            cdCallAPI mycallAPI = new cdCallAPI();
            var jsresponse = await mycallAPI.cdcallAccountsPOST(updatemyAccount);

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

        async void cdHome(object sender, System.EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine(" Clicked Home Button");
            var tpage = new cdHome(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void cdqueryAll()
        {
            String myrole = " ";
            cdCallAPI mycallAPI = new cdCallAPI();
            Account myaccount = new Account();
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "AccountStatusindex";
            qryAcct.ColName = "AccountStatus";
            qryAcct.ColValue = "NotApproved";

            getAccounts myAccountsArray = new getAccounts();

            var jsreponse = await mycallAPI.cdcallAccountsGET(qryAcct);
            myAccountsArray = JsonConvert.DeserializeObject<getAccounts>((string)jsreponse);
            mystudArray = myAccountsArray;
            ApplicantName.Text = "Applicant Name: " + mystudArray.Account[counter].FirstName + " " + mystudArray.Account[counter].LastName;

            if (mystudArray.Account[0].Role.Contains("D"))
            {
                myrole = myrole + " Driver ";
            }
            if (mystudArray.Account[0].Role.Contains("P"))
            {
                myrole = myrole + " Parent ";
            }
            if (mystudArray.Account[0].Role.Contains("A"))
            {
                myrole = myrole + " Admin ";
            }

            ApplicantType.Text = "Applicant Role: "+ myrole;

            try
            {
                foreach ( var myacc in myAccountsArray.Account)
                {
                    maxarray = maxarray + 1;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
            }

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

        async void cdFind(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Find Button");
            String myrole = " ";
            cdCallAPI mycallAPI = new cdCallAPI();
            Account myaccount = new Account();
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "EmailAddressIndex";
            qryAcct.ColName = "EmailAddress";
            qryAcct.ColValue = EmailAddress.Text;

            getAccounts myAccountsArray = new getAccounts();

            var jsreponse = await mycallAPI.cdcallAccountsGET(qryAcct);
            myAccountsArray = JsonConvert.DeserializeObject<getAccounts>((string)jsreponse);
            mystudArray = myAccountsArray;
            ApplicantName.Text = "Applicant Name: " + mystudArray.Account[counter].FirstName + " " + mystudArray.Account[counter].LastName;

            if (mystudArray.Account[0].Role.Contains("D"))
            {
                myrole = myrole + " Driver ";
            }
            if (mystudArray.Account[0].Role.Contains("P"))
            {
                myrole = myrole + " Parent ";
            }
            if (mystudArray.Account[0].Role.Contains("A"))
            {
                myrole = myrole + " Admin ";
            }

            ApplicantType.Text = "Applicant Role: " + myrole;

            PreviousButton.IsEnabled = false;
            NextButton.IsEnabled = false;

        }

        public cdAdminDrive(Account logAccount)
        {
            InitializeComponent();
            loginAccount = logAccount;
            cdqueryAll();
        }

    }
}
