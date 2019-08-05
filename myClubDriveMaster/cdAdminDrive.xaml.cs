using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdAdminDrive : ContentPage
    {
        Account loginAccount = new Account();
        List<ClubMembers> unClubMembers = new List<ClubMembers>();
        int maxarray = -1;
        int counter = 0;

        void cdNext(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Next Button");
            String myrole = " ";
            counter = counter + 1;
            ApplicantName.Text = "Applicant Name: " + unClubMembers[counter].MemberName;

            if (unClubMembers[counter].MemberRole.Contains("D"))
            {
                myrole = myrole + " Driver ";
            }
            if (unClubMembers[counter].MemberRole.Contains("P"))
            {
                myrole = myrole + " Parent ";
            }
            if (unClubMembers[counter].MemberRole.Contains("A"))
            {
                myrole = myrole + " Admin ";
            }
            if (unClubMembers[counter].MemberRole.Contains("R"))
            {
                myrole = myrole + " Rider ";
            }

            ApplicantType.Text = "Applicant Role: " + myrole;

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
            ApplicantName.Text = "Applicant Name: " + unClubMembers[counter].MemberName;

            if (unClubMembers[counter].MemberRole.Contains("D"))
            {
                myrole = myrole + " Driver ";
            }
            if (unClubMembers[counter].MemberRole.Contains("P"))
            {
                myrole = myrole + " Parent ";
            }
            if (unClubMembers[counter].MemberRole.Contains("A"))
            {
                myrole = myrole + " Admin ";
            }
            if (unClubMembers[counter].MemberRole.Contains("R"))
            {
                myrole = myrole + " Rider ";
            }

            ApplicantType.Text = "Applicant Role: " + myrole;

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
            cdUpdateClubMembers updatemyAccount = new cdUpdateClubMembers();
            updatemyAccount.ClubMemberID = unClubMembers[counter].ClubMemberID;
            updatemyAccount.ColumnName = "Attr9";
            updatemyAccount.ColumnValue = picker.SelectedItem.ToString();
            updatemyAccount.ColumnName1 = "Attr6";
            updatemyAccount.ColumnValue1 = unClubMembers[counter].Attr6;
            updatemyAccount.ColumnName2 = "Attr7";
            updatemyAccount.ColumnValue2 = unClubMembers[counter].Attr7;
            updatemyAccount.ColumnName3 = "Attr8";
            updatemyAccount.ColumnValue3 = unClubMembers[counter].Attr8;
            updatemyAccount.ColumnName4 = "Attr5";
            updatemyAccount.ColumnValue4 = unClubMembers[counter].Attr9;

            System.Diagnostics.Debug.WriteLine(" Before calling Post API ");
            cdCallAPI mycallAPI = new cdCallAPI();
            var jsresponse = await mycallAPI.cdcallClubMembersPOST(updatemyAccount);

            System.Diagnostics.Debug.WriteLine(" After calling Post API ");
            if (jsresponse.ToString().Contains("ValidationException"))
            {
                System.Diagnostics.Debug.WriteLine(" Post API Call failed " + jsresponse);
                myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                await DisplayAlert("Update Failed", "Update Failed. " + myerror.message,"OK");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Post API Call Successful");
                await DisplayAlert("Update Successful", "Update Successful","OK");
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
            int clubUACM = 0;
            cdCallAPI mycallAPI = new cdCallAPI();
            Account myaccount = new Account();
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "MemberAccountIDIndex";
            qryAcct.ColName = "MemberAccountID";
            qryAcct.ColValue = loginAccount.AccountID;

            getClubMembers myClubs = new getClubMembers();
            getClubMembers myUnApprovedMembers = new getClubMembers();

            var jsreponse = await mycallAPI.cdcallClubMembersGET(qryAcct);
            myClubs = JsonConvert.DeserializeObject<getClubMembers>((string)jsreponse);

            System.Diagnostics.Debug.WriteLine("Getting Clubs. Response received is " + jsreponse);

            try
            {
                foreach ( var myc in myClubs.ClubMember)
                {

                    if (myc.MemberRole.Contains("A"))
                    {
                        cdQueryAttr qryAcctUACM = new cdQueryAttr();
                        qryAcctUACM.ColIndex = "IndexName";
                        qryAcctUACM.IndexName = "AccountStatusIndex";
                        qryAcctUACM.ColName = "ClubID";
                        qryAcctUACM.ColValue = myc.ClubID;

                        System.Diagnostics.Debug.WriteLine("Getting unapproved members for Club ID " + myc.ClubID);

                        var jsreponseUACM = await mycallAPI.cdcallClubMembersUAGET(qryAcctUACM);
                        myUnApprovedMembers = JsonConvert.DeserializeObject<getClubMembers>((string)jsreponseUACM);

                        System.Diagnostics.Debug.WriteLine("Getting Unapproved club members. Response received is " + jsreponseUACM);

                        foreach (var mycuam in myUnApprovedMembers.ClubMember)
                        {
                            System.Diagnostics.Debug.WriteLine("Adding Club Member to the list "+ mycuam.MemberAccountID);
                            unClubMembers.Add(mycuam);
                            maxarray = maxarray + 1;
                            clubUACM = clubUACM + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
            }

            System.Diagnostics.Debug.WriteLine("Max Array " + maxarray + " club ua array " + clubUACM + " Counter "+counter);

            if ( clubUACM >0 )
            {
                ApplicantName.Text = "Applicant Name: " + unClubMembers[counter].MemberName;

                if (unClubMembers[counter].MemberRole.Contains("D"))
                {
                    myrole = myrole + " Driver ";
                }
                if (unClubMembers[counter].MemberRole.Contains("P"))
                {
                    myrole = myrole + " Parent ";
                }
                if (unClubMembers[counter].MemberRole.Contains("A"))
                {
                    myrole = myrole + " Admin ";
                }
                if (unClubMembers[counter].MemberRole.Contains("R"))
                {
                    myrole = myrole + " Rider ";
                }
                ApplicantType.Text = "Applicant Role: " + myrole;
            }


            if (counter == maxarray)
            {
                PreviousButton.IsEnabled = false;
                NextButton.IsEnabled = false;
            }
            else if (counter < maxarray)
            {
                PreviousButton.IsEnabled = false;
                NextButton.IsEnabled = true;
            }
            else
            {
                PreviousButton.IsEnabled = false;
                NextButton.IsEnabled = false;
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
            qryAcct.IndexName = "MemberAccountIDIndex";
            qryAcct.ColName = "MemberAccountID";
            qryAcct.ColValue = EmailAddress.Text;
            counter = 0;

            getClubMembers myAccountsArray = new getClubMembers();

            var jsreponse = await mycallAPI.cdcallClubMembersGET(qryAcct);
            myAccountsArray = JsonConvert.DeserializeObject<getClubMembers>((string)jsreponse);
            ApplicantName.Text = "Applicant Name: " + myAccountsArray.ClubMember[counter].MemberName;

            if (myAccountsArray.ClubMember[counter].MemberRole.Contains("D"))
            {
                myrole = myrole + " Driver ";
            }
            if (myAccountsArray.ClubMember[counter].MemberRole.Contains("P"))
            {
                myrole = myrole + " Parent ";
            }
            if (myAccountsArray.ClubMember[counter].MemberRole.Contains("A"))
            {
                myrole = myrole + " Admin ";
            }
            if (unClubMembers[counter].MemberRole.Contains("R"))
            {
                myrole = myrole + " Rider ";
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
