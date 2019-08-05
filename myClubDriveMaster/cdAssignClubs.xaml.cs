using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdAssignClubs : ContentPage
    {
        Account loginAccount = new Account();
        Account studentAccount = new Account();
        Club myClub = new Club();
        int clubAccociated = 0;

        async void cdHome(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }

        void cdFind(object sender, System.EventArgs e)
        {
            var response = validateClub();
            if (response.ToString() == "success")
            {
                System.Diagnostics.Debug.WriteLine(" Get Clubs Failed. Cannot proceed ");
            }
        }

        async void cdAssign(object sender, System.EventArgs e)
        {

            if (clubAccociated == 0)
            {
                    var crresponse = associateClub();

                System.Diagnostics.Debug.WriteLine(" Response is  "+ crresponse.ToString());

                if (crresponse.ToString() == "success")
                    {
                        await DisplayAlert("Action", "Club Association Successful", "Ok");
                        clubAccociated = 1;
                        var tpage = new cdAssignClubs(loginAccount, studentAccount);
                        await Navigation.PushModalAsync(tpage);
                    }
                    else
                    {
                        await DisplayAlert("Action", "Club Association Failed", "Ok");
                    }

            }
            else 
            {
                var tpage = new cdAssignClubs(loginAccount, studentAccount);
                await Navigation.PushModalAsync(tpage);
            }
        }

        async void cdSubmit(object sender, System.EventArgs e)
        {

                var crresponse = await associateClub();

                System.Diagnostics.Debug.WriteLine(" Response is  " + crresponse.ToString());
                
                if (crresponse.ToString() == "success")
                {
                   await DisplayAlert("Action", "Club Association Succesful", "Ok");
                   clubAccociated = 1;
                }
                else
                {
                   await DisplayAlert("Action", "Club Association Failed ", "Ok");
                }

        }

        private async Task<JToken> validateClub()
        {
            cdReadError myerror = new cdReadError();
            cdCallAPI mycallAPI = new cdCallAPI();
            cdQueryAttr qryAcct = new cdQueryAttr();
            if (CubID.Text != null)
            { 
                qryAcct = new cdQueryAttr();
                qryAcct.ColIndex = "None";
                qryAcct.IndexName = "None";
                qryAcct.ColName = "ClubID";
                qryAcct.ColValue = CubID.Text;
            }
            else if (getClubName.Text != null)
            {

                qryAcct = new cdQueryAttr();
                qryAcct.ColIndex = "IndexName";
                qryAcct.IndexName = "ClubNameIndex";
                qryAcct.ColName = "ClubName";
                qryAcct.ColValue = getClubName.Text;
            }
            else 
            {
                await DisplayAlert("Action", "Unable to fetch the club Information ", "Ok");
            }

            getClubs tempClubs = new getClubs();
            var jsclubs = await mycallAPI.cdcallClubsGET(qryAcct);
            tempClubs = JsonConvert.DeserializeObject<getClubs>((string)jsclubs);

            System.Diagnostics.Debug.WriteLine(" Club is retrived " + jsclubs);

            if (jsclubs.ToString().Contains("ValidationException") || !jsclubs.ToString().Contains("ClubName"))
            {
                System.Diagnostics.Debug.WriteLine(" Get Clubs Failed " + jsclubs);
                myerror = JsonConvert.DeserializeObject<cdReadError>(jsclubs.ToString());
                await DisplayAlert("Action", "Unable to fetch the club Information ", "Ok");

                return "failed";
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Get Club Information Successful");
                submitButton.IsEnabled = true;
                assignButton.IsEnabled = true;
                myClub = tempClubs.Club[0];
                ClubName.Text = "Club Name: "+tempClubs.Club[0].ClubName;
                CubAddress.Text = "Club Address: "+ tempClubs.Club[0].AddressLine1 + " " + tempClubs.Club[0].AddressLine2;
                City.Text = "City : "+ tempClubs.Club[0].City;
                myState.Text = "State "+ tempClubs.Club[0].cdState;
                PostalCode.Text = "Postal Code "+ tempClubs.Club[0].PostalCode;

                return "success";
            }

        }

        private async Task<JToken> associateClub()
        {
            if ( loginAccount.UserName == studentAccount.UserName)
            {
                System.Diagnostics.Debug.WriteLine(" Login and Parent account are same ");
                var getResponse = await createClubMembers(studentAccount, myClub, "DP", "Parent");
                System.Diagnostics.Debug.WriteLine(" Do we have an exception? "+ !getResponse.ToString().Contains("Exception"));
                if ( !getResponse.ToString().Contains("Exception"))
                {
                    System.Diagnostics.Debug.WriteLine(" Returning Success");
                    return "success";
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(" Returning Failure");
                    return "failed";
                }
            }
            else
            { 
                var getResponse = await createClubMembers(studentAccount, myClub, "R",studentAccount.ParentID);
                if ( !getResponse.ToString().Contains("Exception") )
                {
                    try 
                    { 
                        getResponse = await createClubMembers(loginAccount, myClub, "DP","Parent");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(" Response is " + getResponse + ex);
                    }
                    return "success";
                }
                else
                {
                    return "failed";
                }
            }
        }

        private async Task<JToken> createClubMembers(Account regAccount, Club regClub, String assignRole, string parentID)
        {
            cdReadError myerror = new cdReadError();
            cdCallAPI mycallAPI = new cdCallAPI();
            ClubMembers myclubmembership = new ClubMembers();
            myclubmembership.ClubMemberID = regAccount.UserName + regClub.ClubID;
            myclubmembership.ClubID = regClub.ClubID;
            myclubmembership.MemberAccountID = regAccount.UserName;
            myclubmembership.ClubName = regClub.ClubName;
            myclubmembership.MemberName = regAccount.FirstName + " " + regAccount.LastName;
            myclubmembership.MemberRole = assignRole;
            myclubmembership.Attr1 = parentID;
            myclubmembership.Attr2 = regAccount.EmailAddress;
            myclubmembership.Attr3 = "NA";
            myclubmembership.Attr4 = "NA";
            myclubmembership.Attr5 = "NA";
            myclubmembership.Attr6 = "NA";
            myclubmembership.Attr7 = "NA";
            myclubmembership.Attr8 = "NA";
            myclubmembership.Attr9 = "NotApproved";
            myclubmembership.Attr10 = "NA";

            var jsresponse = await mycallAPI.cdcallClubMemberPUT(myclubmembership);

            System.Diagnostics.Debug.WriteLine(" Response is " + jsresponse);

            if (jsresponse.ToString().Contains("ValidationException"))
            {
                System.Diagnostics.Debug.WriteLine(" Club Membership creation call failed " + jsresponse);
                var myError = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                return "failed";
            }
            else
            {
                return "success";
            }
        }

        public cdAssignClubs(Account laccount, Account saccount)
        {
            InitializeComponent();
            loginAccount = laccount;
            studentAccount = saccount;
            submitButton.IsEnabled = false;
            assignButton.IsEnabled = false;

        }
    }
}
