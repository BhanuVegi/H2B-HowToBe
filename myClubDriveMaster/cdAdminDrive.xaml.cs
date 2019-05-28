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

        void cdSubmit(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Drive Button");
        }

        void cdHome(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Home Button");
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
