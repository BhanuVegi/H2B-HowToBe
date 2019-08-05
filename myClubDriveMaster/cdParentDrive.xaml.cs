using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace myClubDriveMaster
{
    public partial class cdParentDrive : ContentPage
    {
        Account loginAccount = new Account();
        getAccounts mystudAccounts = new getAccounts();
        int maxarray = -1;
        int counter = 0;
 
        void cdNext(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Next Button");
            counter = counter + 1;

            StudentName.Text = "Student Name: " + mystudAccounts.Account[counter].FirstName + " " + mystudAccounts.Account[counter].LastName;
            DestinationAddress1.Text = mystudAccounts.Account[counter].AddressLine1;
            DestinationAddress2.Text = mystudAccounts.Account[counter].AddressLine2;
            City.Text = mystudAccounts.Account[counter].City;
            State.Text = mystudAccounts.Account[counter].cdState;
            PostalCode.Text = mystudAccounts.Account[counter].PostalCode;

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

            StudentName.Text = "Student Name: " + mystudAccounts.Account[counter].FirstName + " " + mystudAccounts.Account[counter].LastName;
            DestinationAddress1.Text = mystudAccounts.Account[counter].AddressLine1;
            DestinationAddress2.Text = mystudAccounts.Account[counter].AddressLine2;
            City.Text = mystudAccounts.Account[counter].City;
            State.Text = mystudAccounts.Account[counter].cdState;
            PostalCode.Text = mystudAccounts.Account[counter].PostalCode;

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
            cdUpdateAccount updateAddress = new cdUpdateAccount();
            updateAddress.AccountID = mystudAccounts.Account[counter].AccountID;
            updateAddress.ColumnName = "AddressLine1";
            updateAddress.ColumnValue = DestinationAddress1.Text;
            updateAddress.ColumnName1 = "AddressLine2" ;
            updateAddress.ColumnValue1 = DestinationAddress2.Text;
            updateAddress.ColumnName2 = "City";
            updateAddress.ColumnValue2 = City.Text;
            updateAddress.ColumnName3 = "cdState";
            updateAddress.ColumnValue3 = State.Text;
            updateAddress.ColumnName4 = "PostalCode";
            updateAddress.ColumnValue4 = PostalCode.Text;

            System.Diagnostics.Debug.WriteLine(" Before calling Post API ");
            cdCallAPI mycallAPI = new cdCallAPI();
            var jsresponse = await mycallAPI.cdcallAccountsPOST(updateAddress);

            System.Diagnostics.Debug.WriteLine(" After calling Post API ");
            if (jsresponse.ToString().Contains("ValidationException"))
            {
                System.Diagnostics.Debug.WriteLine(" Post API Call failed " + jsresponse);
                myerror= JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                updateStatus.Text = "Update Failed. "+myerror.message;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Post API Call Successful");
                updateStatus.Text = "Update Successful";
            }

           // System.Diagnostics.Debug.WriteLine(" Update Response is "+jsresponse);

        }

        async void cdTrack(object sender, System.EventArgs e)
        {
            try
            { 
            System.Diagnostics.Debug.WriteLine(" Clicked Track Button");
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "StudentIDindex";
            qryAcct.ColName = "StudentID";
            qryAcct.ColValue = mystudAccounts.Account[counter].UserName;

            getDriver myDriverArray = new getDriver();
            DriverAllocation pubDriverInfo = new DriverAllocation();
            cdCallAPI mycallAPI = new cdCallAPI();

            var jsreponse = await mycallAPI.cdcallDriverAllocGET(qryAcct);
            myDriverArray = JsonConvert.DeserializeObject<getDriver>((string)jsreponse);

            pubDriverInfo = myDriverArray.DriverAllocation[0];

            String trackkey = pubDriverInfo.DriverID+ DateTime.Now.ToShortDateString();
            System.Diagnostics.Debug.WriteLine(" Tracking "+ mystudAccounts.Account[counter].UserName + "with the key "+ trackkey);

            var tpage = new cdTrackRiders(trackkey,loginAccount);
            await Navigation.PushModalAsync(tpage);
            }
            catch (Exception ex)
            {
                await DisplayAlert("No Tracking", "No Tracking information available for this student","OK");
            }

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

            StudentName.Text = "Student Name: "+mystudAccounts.Account[0].FirstName + " " + mystudAccounts.Account[0].LastName;
            DestinationAddress1.Text = mystudAccounts.Account[0].AddressLine1;
            DestinationAddress2.Text = mystudAccounts.Account[0].AddressLine2;
            City.Text = mystudAccounts.Account[0].City;
            State.Text = mystudAccounts.Account[0].cdState;
            PostalCode.Text = mystudAccounts.Account[0].PostalCode;

            System.Diagnostics.Debug.WriteLine(" Max Array is "+maxarray);

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

        public cdParentDrive(Account logAccount)
        {
            InitializeComponent();
            loginAccount = logAccount;

            getStudentInfo(logAccount);

        }
    }
}
