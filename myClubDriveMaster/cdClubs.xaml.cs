using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdClubs : ContentPage
    {

        Account myAccount = new Account();
        getClubs myClubs = new getClubs();

        List<Club> assignedClubs = new List<Club>();

        int maxarray = -1;
        int counter = 0;

        async void cdHome(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(myAccount);
            await Navigation.PushModalAsync(tpage);
        }
        async void cdAssign(object sender, System.EventArgs e)
        {
            try 
            { 
                if (myAccount.Role.Contains("R") & myAccount.ParentID != "None")
                {
                    Account paccount = new Account();
                    cdCallAPI mycallAPI = new cdCallAPI();
                    cdQueryAttr qryAcct = new cdQueryAttr();
                    qryAcct.ColIndex = "IndexName";
                    qryAcct.IndexName = "UserNameindex";
                    qryAcct.ColName = "UserName";
                    qryAcct.ColValue = myAccount.ParentID;

                    getAccounts myAccountsArray = new getAccounts();

                    var jsreponse = await mycallAPI.cdcallAccountsGET(qryAcct);
                    myAccountsArray = JsonConvert.DeserializeObject<getAccounts>((string)jsreponse);
                    paccount = myAccountsArray.Account[0]; 
                    var tpage = new cdAssignClubs(paccount,myAccount);
                    await Navigation.PushModalAsync(tpage);
                }
                else
                {
                    var tpage = new cdAssignClubs(myAccount, myAccount);
                    await Navigation.PushModalAsync(tpage);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
                await DisplayAlert("Action", "Unable to fetch data. Please try later.","OK");

            }
        }
        async void cdSubmit(object sender, System.EventArgs e)
        {
            try
            {
                cdReadError myerror = new cdReadError();
                cdUpdateClub updateAddress = new cdUpdateClub();
                updateAddress.ClubID = assignedClubs[counter].ClubID;
                updateAddress.ColumnName = "AddressLine1";
                updateAddress.ColumnValue = CubAddress.Text;
                updateAddress.ColumnName2 = "City";
                updateAddress.ColumnValue2 = City.Text;
                updateAddress.ColumnName3 = "cdState";
                updateAddress.ColumnValue3 = myState.Text;
                updateAddress.ColumnName4 = "PostalCode";
                updateAddress.ColumnValue4 = PostalCode.Text;

                System.Diagnostics.Debug.WriteLine(" Before calling Post API ");
                cdCallAPI mycallAPI = new cdCallAPI();
                var jsresponse = await mycallAPI.cdcallClubsPOST(updateAddress);

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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
                await DisplayAlert("Action", "Unable to update data. Please try later.", "OK");

            }

        }

        void cdPervious(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Previous Button");
            counter = counter - 1;

            ClubName.Text = "Club Name: " + assignedClubs[counter].ClubName;
            ClubID.Text = "Clud ID: " + assignedClubs[counter].ClubID;
            CubAddress.Text = assignedClubs[counter].AddressLine1 + " " + assignedClubs[counter].AddressLine2;
            City.Text = assignedClubs[counter].City;
            myState.Text = assignedClubs[counter].cdState;
            PostalCode.Text = assignedClubs[counter].PostalCode;

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

        void cdNext(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Next Button");
            counter = counter + 1;

            ClubName.Text = "Club Name: " + assignedClubs[counter].ClubName;
            ClubID.Text = "Clud ID: " + assignedClubs[counter].ClubID;
            CubAddress.Text = assignedClubs[counter].AddressLine1 + " " + assignedClubs[counter].AddressLine2;
            City.Text = assignedClubs[counter].City;
            myState.Text = assignedClubs[counter].cdState;
            PostalCode.Text = assignedClubs[counter].PostalCode;

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

        async void getClubs()
        {
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "MemberAccountIDIndex";
            qryAcct.ColName = "MemberAccountID";
            qryAcct.ColValue = myAccount.UserName;
            String clubRetrived = "";

            System.Diagnostics.Debug.WriteLine(" Getting clubs from club members");

            getClubMembers myClubMembers = new getClubMembers();
            cdCallAPI mycallAPI = new cdCallAPI();

            var jsreponse = await mycallAPI.cdcallClubMembersGET(qryAcct);
            myClubMembers = JsonConvert.DeserializeObject<getClubMembers>((string)jsreponse);

            System.Diagnostics.Debug.WriteLine(" Club Member payload is " + jsreponse);

            try
            {
                foreach (var stacc in myClubMembers.ClubMember)
                {
                    maxarray = maxarray + 1;
                    qryAcct = new cdQueryAttr();
                    qryAcct.ColIndex = "None";
                    qryAcct.IndexName = "None";
                    qryAcct.ColName = "ClubID";
                    qryAcct.ColValue = myClubMembers.ClubMember[maxarray].ClubID;

                    System.Diagnostics.Debug.WriteLine(" Populating Club for " + myClubMembers.ClubMember[maxarray].ClubID);

                    getClubs tempClubs = new getClubs();
                    var jsclubs = await mycallAPI.cdcallClubsGET(qryAcct);
                    tempClubs = JsonConvert.DeserializeObject<getClubs>((string)jsclubs);
                    clubRetrived = (string)jsclubs;

                    System.Diagnostics.Debug.WriteLine(" Club is retrived " + jsclubs);

                    System.Diagnostics.Debug.WriteLine(" Before Initializing club "+ tempClubs.Club[0].ClubID +"  "+ tempClubs.Club[0].ClubName);
                    assignedClubs.Add(tempClubs.Club[0]);
                    System.Diagnostics.Debug.WriteLine(" After Initializing club ");

                    System.Diagnostics.Debug.WriteLine(" Added to assigned clubs ");

                }

            if (clubRetrived.Contains("ClubName"))
            { 
                ClubName.Text = "Club Name: " + assignedClubs[0].ClubName;
                ClubID.Text = "Clud ID: " + assignedClubs[0].ClubID;
                CubAddress.Text = assignedClubs[0].AddressLine1+" "+ assignedClubs[0].AddressLine2;
                City.Text = assignedClubs[0].City;
                myState.Text = assignedClubs[0].cdState;
                PostalCode.Text = assignedClubs[0].PostalCode;
            }
            else
            {
                PreviousButton.IsEnabled = false;
                NextButton.IsEnabled = false;
                SubmitButton.IsEnabled = false;
            }

            System.Diagnostics.Debug.WriteLine(" Max Array is " + maxarray);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
                await DisplayAlert("Action", "Unable to fetch data. Please try later.","OK");

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

        public cdClubs(Account loginAccount)
        {

            InitializeComponent();
            myAccount = loginAccount;
            getClubs();

        }
    }
}
