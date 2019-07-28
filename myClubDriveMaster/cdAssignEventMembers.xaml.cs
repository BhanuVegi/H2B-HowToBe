using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
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
            var getresp = await cdMapStdDrv(iEveSign.EventID);
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
                insertEventMembers.AllocationStatus = "UNALLOCATED";
                insertEventMembers.DriverCar = "NA";
                insertEventMembers.RiderCount = "0";
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

        async Task<JToken> cdMapStdDrv(String myCurrentEventID)
        {
            cdAllEventSignups curreventsignups = new cdAllEventSignups();
            cdAllEventSignups drveventsignups = new cdAllEventSignups();
            cdAllEventSignups stdeventsignups = new cdAllEventSignups();
            cdCallAPI mycallAPI = new cdCallAPI();
            cdUpdateEventMembers updem = new cdUpdateEventMembers();
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "EventIDIndex";
            qryAcct.ColName = "EventID";
            qryAcct.ColValue = myCurrentEventID;
            int esmaxarray = -1;
            int drvarray = -1;
            int stdarray = -1;

            var response = await mycallAPI.cdcallEventMembersGET(qryAcct);
            curreventsignups = JsonConvert.DeserializeObject<cdAllEventSignups>((string)response);

            System.Diagnostics.Debug.WriteLine(" Event Signup payload is " + response);

            try
            {
                foreach (var divsignup in curreventsignups.EventSignup)
                {
                    esmaxarray = esmaxarray + 1;
                    try
                    {
                        if (divsignup.MemberRole.Contains("D") == true)
                        {
                            drvarray = drvarray + 1;
                            drveventsignups.EventSignup[drvarray] = divsignup;
                        }
                        else
                        {
                            stdarray = stdarray + 1;
                            stdeventsignups.EventSignup[stdarray] = divsignup;

                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Out of loop " + ex);
                    }
                }

                System.Diagnostics.Debug.WriteLine(" Event signups devided. Max array is " + esmaxarray);
                int stdcounter = 0;

                foreach (var allsignup in drveventsignups.EventSignup)
                {
                    int rcount = Convert.ToInt32(allsignup.RiderCount);
                    try
                    {
                        for (stdcounter = 0; stdcounter <= stdarray && rcount > 0; stdcounter++)
                        {

                            rcount = rcount - 1;
                            //Do student driver allocation
                            DriverAllocation myDriverAlloc = new DriverAllocation();
                            myDriverAlloc.AllocationID = allsignup.MemberAccountID + stdeventsignups.EventSignup[stdcounter].MemberAccountID + myCurrentEventID;
                            myDriverAlloc.EventID = myCurrentEventID;
                            myDriverAlloc.ClubID = allsignup.ClubID;
                            myDriverAlloc.DriverID = allsignup.MemberAccountID;
                            myDriverAlloc.StudentID = stdeventsignups.EventSignup[stdcounter].MemberAccountID;
                            myDriverAlloc.EventName = allsignup.EventName;
                            myDriverAlloc.ClubName = allsignup.ClubName;
                            myDriverAlloc.Attr1 = "None";
                            myDriverAlloc.Attr2 = "None";
                            myDriverAlloc.Attr3 = "None";
                            myDriverAlloc.Attr4 = "None";
                            myDriverAlloc.Attr5 = "None";
                            myDriverAlloc.Attr6 = "None";
                            myDriverAlloc.Attr7 = "None";
                            myDriverAlloc.Attr8 = "None";
                            myDriverAlloc.Attr9 = "None";
                            myDriverAlloc.Attr10 = "None";
                            var daresponse = await mycallAPI.cdcallDriverAllocPUT(myDriverAlloc);
                            if (daresponse.ToString().Contains("ValidationException"))
                            {
                                System.Diagnostics.Debug.WriteLine(" Put API Call failed " + daresponse);
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine(" Put API Call Successful ");
                            }

                            //Set Student to Allocated
                            updem.EventMemberID = stdeventsignups.EventSignup[stdcounter].EventMemberID;
                            updem.ColumnName = "AllocationStatus";
                            updem.ColumnValue = "ALLOCATED";
                            updem.ColumnName1 = "RiderCount";
                            updem.ColumnValue1 = "0";
                            updem.ColumnName2 = "Attr1";
                            updem.ColumnValue2 = "None";
                            updem.ColumnName3 = "Attr2";
                            updem.ColumnValue3 = "None";
                            updem.ColumnName4 = "Attr3";
                            updem.ColumnValue4 = "None";

                            daresponse = await mycallAPI.cdEventMembersPOST(updem);
                            if (daresponse.ToString().Contains("ValidationException"))
                            {
                                System.Diagnostics.Debug.WriteLine(" Put API Call failed " + daresponse);
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine(" Put API Call Successful  ");
                            }
                        }
                        if (rcount <= 0)
                        {
                            //Set driver to allocated
                            updem.EventMemberID = allsignup.EventMemberID;
                            updem.ColumnName = "AllocationStatus";
                            updem.ColumnValue = "ALLOCATED";
                            updem.ColumnName1 = "RiderCount";
                            updem.ColumnValue1 = "0";
                            updem.ColumnName2 = "Attr1";
                            updem.ColumnValue2 = "None";
                            updem.ColumnName3 = "Attr2";
                            updem.ColumnValue3 = "None";
                            updem.ColumnName4 = "Attr3";
                            updem.ColumnValue4 = "None";

                            var dauresponse = await mycallAPI.cdEventMembersPOST(updem);
                            if (dauresponse.ToString().Contains("ValidationException"))
                            {
                                System.Diagnostics.Debug.WriteLine(" Put API Call failed " + dauresponse);
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine(" Put API Call Successful ");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Out of loop " + ex);
                    }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Loop " + ex);
            }

            return ("success");
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
