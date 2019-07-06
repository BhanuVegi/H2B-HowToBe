using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using Plugin.InputKit;

namespace myClubDriveMaster
{
    public partial class cdAssignEventMembers : ContentPage
    {
        Account myAccount = new Account();
        String pClubName = "";
        String pEventID = "";
        String pEventName = "";
        String pClubID = "";
        String pEventAddress = "";
        int maxarray = -1;
        int counter = 0;
        getAccounts myStudentArray = new getAccounts();

        async void cdHome(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(myAccount);
            await Navigation.PushModalAsync(tpage);
        }
        void cdNext(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Next Button");
            counter = counter + 1;
            cdSignupRider.IsChecked = true;
            UserName.Text = "User Name: " + myStudentArray.Account[counter].FirstName + " " + myStudentArray.Account[counter].LastName;
            cdEmail.Text = "Email Address: " + myStudentArray.Account[counter].EmailAddress;
            cdFirstName.Text = "First Name: " + myStudentArray.Account[counter].FirstName;
            cdMiddleName.Text = "Middle Name: " + myStudentArray.Account[counter].MiddleName;
            cdLastName.Text = "Last Name: " + myStudentArray.Account[counter].LastName;

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
            cdSignupRider.IsChecked = true;
            UserName.Text = "User Name: " + myStudentArray.Account[counter].FirstName + " " + myStudentArray.Account[counter].LastName;
            cdEmail.Text = "Email Address: " + myStudentArray.Account[counter].EmailAddress;
            cdFirstName.Text = "First Name: " + myStudentArray.Account[counter].FirstName;
            cdMiddleName.Text = "Middle Name: " + myStudentArray.Account[counter].MiddleName;
            cdLastName.Text = "Last Name: " + myStudentArray.Account[counter].LastName;

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
        async void ieventmem(cdEventSignups iEveSign)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            cdReadError myerror = new cdReadError();
            var jsresponse = await mycallAPI.cdcallEventsMemberPUT(iEveSign);
            if (jsresponse.ToString().Contains("ValidationException"))
            {
                System.Diagnostics.Debug.WriteLine(" Put API Call failed " + jsresponse);
                myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                await DisplayAlert("Event Signup Failed", jsresponse.ToString(), "ok");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Put API Call Successful");
                await DisplayAlert("Event Signup Successful", "Event Signup Successful", "ok");
            }
        }

        void cdSubmit(object sender, System.EventArgs e)
        {
        
            if (cdSignupRider.IsChecked == true) 
            { 
                cdEventSignups insertEventMembers = new cdEventSignups();
                insertEventMembers.EventID = pEventID;
                insertEventMembers.EventName = pEventName;
                insertEventMembers.ClubName = pClubName;
                insertEventMembers.ClubID = pClubID;
                insertEventMembers.PickupLocation = pEventAddress;
                insertEventMembers.Attr1 = "None";
                insertEventMembers.Attr2 = "None";
                insertEventMembers.Attr3 = "None";
                insertEventMembers.Attr4 = "None";
                insertEventMembers.Attr5 = "None";
                insertEventMembers.Attr6 = "None";
                insertEventMembers.Attr7 = "None";
                insertEventMembers.Attr8 = "None";
                insertEventMembers.Attr9 = "None";
                insertEventMembers.Attr10 = "None";
                insertEventMembers.EventMemberID = myStudentArray.Account[counter].UserName+ pClubName.Substring(0, 3)+ pEventName.Substring(0,3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                insertEventMembers.MemberName = myStudentArray.Account[counter].FirstName + " " + myStudentArray.Account[counter].LastName;
                insertEventMembers.MemberAccountID = myStudentArray.Account[counter].UserName;
                insertEventMembers.MemberRole = "R";
                ieventmem(insertEventMembers);
            }

        }

        async void getStudentInfo(Account logAccount)
        {
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "ParentIDindex";
            qryAcct.ColName = "ParentID";
            qryAcct.ColValue = logAccount.UserName;

            cdCallAPI mycallAPI = new cdCallAPI();

            var jsreponse = await mycallAPI.cdcallAccountsGET(qryAcct);
            myStudentArray = JsonConvert.DeserializeObject<getAccounts>((string)jsreponse);

            try
            {
                foreach (var stacc in myStudentArray.Account)
                {
                    maxarray = maxarray + 1;
                }
                UserName.Text = "User Name: " + myStudentArray.Account[0].FirstName + " " + myStudentArray.Account[0].LastName;
                cdEmail.Text = "Email Address: " + myStudentArray.Account[0].EmailAddress;
                cdFirstName.Text = "First Name: " + myStudentArray.Account[0].FirstName;
                cdMiddleName.Text = "Middle Name: " + myStudentArray.Account[0].MiddleName;
                cdLastName.Text = "Last Name: " + myStudentArray.Account[0].LastName;
                cdSignupRider.IsChecked = true;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
            }

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

        public cdAssignEventMembers(Account loginAccount, String ClubID,String ClubName, String EventID, String EventName, String EventAddress)
        {
            InitializeComponent();
            myAccount = loginAccount;
            pClubName = ClubName;
            pEventID = EventID;
            pEventName = EventName;
            pEventAddress = EventAddress;
            pClubID = ClubID;

            if ( loginAccount.Role.Contains("P") == true )
            {
                getStudentInfo(loginAccount);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Just use the student key ");
                cdSignupRider.IsChecked = true;
                UserName.Text = "User Name: "+loginAccount.FirstName + " " + loginAccount.LastName;
                cdEmail.Text = "Email Address: "+loginAccount.EmailAddress;
                cdFirstName.Text = "First Name: "+loginAccount.FirstName;
                cdMiddleName.Text = "Middle Name: "+loginAccount.MiddleName;
                cdLastName.Text = "Last Name: "+loginAccount.LastName;
            }

        }
    }
}
