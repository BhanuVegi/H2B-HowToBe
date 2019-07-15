using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace myClubDriveMaster
{
    public partial class cdMyEvents : ContentPage
    {
        Account myAccount = new Account();
        getEvents myEvents = new getEvents();
        List<cdEvents> assignedEvents = new List<cdEvents>();
        String returnError = "NA";
        String myClubName = " ";
        String myEventID = " ";
        String myEventName = " ";
        String myEventAddress = " ";
        String myClubID = "";
        int maxarray = -1;
        int counter = 0;
        int eventcounter = 0;
        String prevclub = "";
        String myEventDate = "";

        async void cdHome(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(myAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void cdRiders(object sender, System.EventArgs e)
        {
            if (cdCheckRider.IsChecked == true)
            {
                var resp = await createEventSignups("Self");
            }
            var tpage = new cdAssignEventMembers(myAccount,myClubID ,myClubName, myEventID, myEventName,myEventAddress);
            await Navigation.PushModalAsync(tpage);
        }

        void OnDateSelected(object sender, System.EventArgs e)
        {
            myEventDate = eventDatePicker.Date.ToString();
            System.Diagnostics.Debug.WriteLine(" Event date is " + myEventDate);

        }

        async Task<JToken> createEventSignups(String fwho)
        {
            if ( fwho == "Self")
            { 
                cdCallAPI mycallAPI = new cdCallAPI();
                cdReadError myerror = new cdReadError();
                cdEventSignups insertEventMembers = new cdEventSignups();
                insertEventMembers.EventID = assignedEvents[counter].EventID;
                insertEventMembers.EventName = assignedEvents[counter].EventName;
                insertEventMembers.ClubName = assignedEvents[counter].ClubName;
                insertEventMembers.ClubID = assignedEvents[counter].ClubID;
                insertEventMembers.AllocationStatus = "UNALLOCATED";
                insertEventMembers.DriverCar = CarType.Text;
                insertEventMembers.RiderCount = CarAllowance.Text;
                insertEventMembers.PickupLocation = assignedEvents[counter].AddressLine1+ " "+assignedEvents[counter].City + " " + assignedEvents[counter].cdState + " " + assignedEvents[counter].PostalCode;
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
                insertEventMembers.EventMemberID = myAccount.UserName + assignedEvents[counter].ClubName.Substring(0, 3) + assignedEvents[counter].EventName.Substring(0, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                insertEventMembers.MemberName = myAccount.FirstName + " " + myAccount.LastName;
                insertEventMembers.MemberAccountID = myAccount.UserName;
                if (cdCheckRider.IsChecked == true)
                { 
                    insertEventMembers.MemberRole = "D";
                }
                else
                {
                    insertEventMembers.MemberRole = "R";
                }
                var jsresponse = await mycallAPI.cdcallEventsMemberPUT(insertEventMembers);
                if (jsresponse.ToString().Contains("ValidationException"))
                {
                    System.Diagnostics.Debug.WriteLine(" Put API Call failed " + jsresponse);
                    myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                    await DisplayAlert("Event Signup Failed", jsresponse.ToString(), "ok");
                    return "failed";
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(" Put API Call Successful");
                    await DisplayAlert("Event Signup Successful", "Event Signup Successful", "ok");
                    return "success";
                }
            }
            else
            {
                cdQueryAttr qryAcct = new cdQueryAttr();
                qryAcct.ColIndex = "IndexName";
                qryAcct.IndexName = "ParentIDIndex";
                qryAcct.ColName = "Attr1";
                qryAcct.ColValue = myAccount.UserName;

                System.Diagnostics.Debug.WriteLine(" Getting Students from login user");

                getClubMembers myClubMembers = new getClubMembers();
                cdCallAPI mycallAPI = new cdCallAPI();

                var jsreponse = await mycallAPI.cdcallClubMembersGET(qryAcct);
                myClubMembers = JsonConvert.DeserializeObject<getClubMembers>((string)jsreponse);

                System.Diagnostics.Debug.WriteLine(" Club Member payload is " + jsreponse);

                try
                {
                    foreach (var stacc in myClubMembers.ClubMember)
                    {
                        cdCallAPI myscallAPI = new cdCallAPI();
                        cdReadError myerror = new cdReadError();
                        cdEventSignups insertEventMembers = new cdEventSignups();
                        insertEventMembers.EventID = assignedEvents[counter].EventID;
                        insertEventMembers.EventName = assignedEvents[counter].EventName;
                        insertEventMembers.ClubName = assignedEvents[counter].ClubName;
                        insertEventMembers.ClubID = assignedEvents[counter].ClubID;
                        insertEventMembers.AllocationStatus = "UNALLOCATED";
                        insertEventMembers.DriverCar = "NA";
                        insertEventMembers.RiderCount = "0";
                        insertEventMembers.PickupLocation = assignedEvents[counter].AddressLine1 + " " + assignedEvents[counter].City + " " + assignedEvents[counter].cdState + " " + assignedEvents[counter].PostalCode;
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
                        insertEventMembers.EventMemberID = stacc.MemberAccountID +stacc.ClubName.Substring(0, 3) + assignedEvents[counter].EventName.Substring(0, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                        insertEventMembers.MemberName = stacc.MemberName;
                        insertEventMembers.MemberRole = "R";
                        var jsresponse = await myscallAPI.cdcallEventsMemberPUT(insertEventMembers);
                        if (jsresponse.ToString().Contains("ValidationException"))
                        {
                            System.Diagnostics.Debug.WriteLine(" Put API Call failed " + jsresponse);
                            myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                            returnError = returnError+" "+ stacc.MemberName;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(" Put API Call Successful for "+ stacc.MemberName);
                        }
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("End of Clubs Loop " + ex);
                }

                System.Diagnostics.Debug.WriteLine(" Looped through all students assigned to the club ");
                if (returnError == "NA")
                {
                    await DisplayAlert("Event Signup Successful", "Event Signup Successful for all members", "ok");
                    return "success"; 
                }
                else
                {
                    await DisplayAlert("Event Signup Failed", "Event Signup failed for "+ returnError+". Sign up individual members" , "ok");
                    return "failed for "+ returnError;
                }

            }


        }

        async void cdSubmit(object sender, System.EventArgs e)
        {

            if (assignedEvents[counter].Attr1.Contains("A") == true)
            {
                cdReadError myerror = new cdReadError();
                cdUpdateEvent updateAddress = new cdUpdateEvent();
                updateAddress.EventID = assignedEvents[counter].EventID;
                updateAddress.ColumnName = "AddressLine1";
                updateAddress.ColumnValue = EventAddress.Text;
                updateAddress.ColumnName2 = "City";
                updateAddress.ColumnValue2 = City.Text;
                updateAddress.ColumnName3 = "cdState";
                updateAddress.ColumnValue3 = myState.Text;
                updateAddress.ColumnName4 = "PostalCode";
                updateAddress.ColumnValue4 = PostalCode.Text;

                System.Diagnostics.Debug.WriteLine(" Before calling Post API ");
                cdCallAPI mycallAPI = new cdCallAPI();
                var jsresponse = await mycallAPI.cdcallEventsPOST(updateAddress);

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
                if (cdAddAll.IsChecked == true)
                {
                    var resp = await createEventSignups("Everyone");
                }
                if (cdCheckRider.IsChecked == true )
                {
                    var resp = await createEventSignups("Self");
                }

            }

        }

        void cdPervious(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Previous Button");
            counter = counter - 1;
            myClubName = assignedEvents[counter].Attr3;
            myClubID = assignedEvents[counter].ClubID;
            myEventID = assignedEvents[counter].EventID;
            myEventName = assignedEvents[counter].EventName;
            myEventAddress = assignedEvents[counter].AddressLine1 + " " + assignedEvents[counter].AddressLine2 + " ," + assignedEvents[counter].City + " ," + assignedEvents[counter].cdState + " " + assignedEvents[counter].PostalCode;
            EventName.Text = "Event Name: " + assignedEvents[counter].EventName;
            EventID.Text = "Clud ID: " + assignedEvents[counter].EventID;
            EventAddress.Text = assignedEvents[counter].AddressLine1;
            if (assignedEvents[0].AddressLine2 == "None" || assignedEvents[0].AddressLine2 == "NA")
            {
                EventAddress2.Text = "";
            }
            else
            {
                EventAddress2.Text = assignedEvents[0].AddressLine2;
            }
            City.Text = assignedEvents[counter].City;
            myState.Text = assignedEvents[counter].cdState;
            PostalCode.Text = assignedEvents[counter].PostalCode;
            ClubName.Text = assignedEvents[counter].ClubName;
            cdNotes.Text = assignedEvents[counter].Notes;

            if (assignedEvents[counter].Attr1.Contains("A") == false)
            {
                EventName.IsEnabled = false;
                EventID.IsEnabled = false;
                EventAddress.IsEnabled = false;
                EventAddress2.IsEnabled = false;
                City.IsEnabled = false;
                myState.IsEnabled = false;
                PostalCode.IsEnabled = false;
                cdNotes.IsEnabled = false;
                eventDatePicker.IsEnabled = false;
            }
            else
            {
                EventName.IsEnabled = true;
                EventID.IsEnabled = true;
                EventAddress.IsEnabled = true;
                EventAddress2.IsEnabled = true;
                City.IsEnabled = true;
                myState.IsEnabled = true;
                PostalCode.IsEnabled = true;
                cdNotes.IsEnabled = true;
                eventDatePicker.IsEnabled = true;
            }

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
            myClubName = assignedEvents[counter].Attr3;
            myClubID = assignedEvents[counter].ClubID;
            myEventID = assignedEvents[counter].EventID;
            myEventName = assignedEvents[counter].EventName;
            myEventAddress = assignedEvents[counter].AddressLine1 + " " + assignedEvents[counter].AddressLine2 + " ," + assignedEvents[counter].City + " ," + assignedEvents[counter].cdState + " " + assignedEvents[counter].PostalCode;
            EventName.Text = "Event Name: " + assignedEvents[counter].EventName;
            EventID.Text = "Clud ID: " + assignedEvents[counter].EventID;
            EventAddress.Text = assignedEvents[counter].AddressLine1;
            if (assignedEvents[0].AddressLine2 == "None" || assignedEvents[0].AddressLine2 == "NA")
            {
                EventAddress2.Text = "";
            }
            else
            {
                EventAddress2.Text = assignedEvents[0].AddressLine2;
            }
            City.Text = assignedEvents[counter].City;
            myState.Text = assignedEvents[counter].cdState;
            PostalCode.Text = assignedEvents[counter].PostalCode;
            ClubName.Text = assignedEvents[counter].ClubName;
            cdNotes.Text = assignedEvents[counter].Notes;

            if (assignedEvents[counter].Attr1.Contains("A") == false)
            {
                EventName.IsEnabled = false;
                EventID.IsEnabled = false;
                EventAddress.IsEnabled = false;
                EventAddress2.IsEnabled = false;
                City.IsEnabled = false;
                myState.IsEnabled = false;
                PostalCode.IsEnabled = false;
                cdNotes.IsEnabled = false;
                eventDatePicker.IsEnabled = false;
            }
            else
            {
                EventName.IsEnabled = true;
                EventID.IsEnabled = true;
                EventAddress.IsEnabled = true;
                EventAddress2.IsEnabled = true;
                City.IsEnabled = true;
                myState.IsEnabled = true;
                PostalCode.IsEnabled = true;
                cdNotes.IsEnabled = true;
                eventDatePicker.IsEnabled = true;
            }

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

        async void getEvents()
        {
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "MemberAccountIDIndex";
            qryAcct.ColName = "MemberAccountID";
            qryAcct.ColValue = myAccount.UserName;

            System.Diagnostics.Debug.WriteLine(" Getting Clubs from login user");

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
                    qryAcct.ColIndex = "IndexName";
                    qryAcct.IndexName = "ClubIDIndex";
                    qryAcct.ColName = "ClubID";
                    qryAcct.ColValue = stacc.ClubID;

                    if (prevclub == stacc.ClubID)
                    {
                        prevclub = stacc.ClubID;
                    }
                    else
                    {
                        try
                        {
                            getEvents myTempEvents = new getEvents();

                            var jsereponse = await mycallAPI.cdcallEventsGET(qryAcct);
                            myTempEvents = JsonConvert.DeserializeObject<getEvents>((string)jsereponse);
                            System.Diagnostics.Debug.WriteLine(" response pay load is " + jsereponse);

                            foreach (var mte in myTempEvents.cdEvents)
                            {
                                System.Diagnostics.Debug.WriteLine(" Event counter is " + eventcounter);
                                mte.Attr1 = stacc.MemberRole;
                                assignedEvents.Add(mte);
                                eventcounter = eventcounter + 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("No events for this club " + stacc.ClubID + " " + ex);
                        }

                        System.Diagnostics.Debug.WriteLine(" Events added ");
                        prevclub = stacc.ClubID;
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Clubs Loop " + ex);
            }



            System.Diagnostics.Debug.WriteLine("Populating global strings ");

            myClubName = assignedEvents[0].ClubName;
            myClubID = assignedEvents[0].ClubID;
            myEventID = assignedEvents[0].EventID;
            myEventName = assignedEvents[0].EventName;
            myEventAddress = assignedEvents[0].AddressLine1 + " " + assignedEvents[0].AddressLine2+" ,"+ assignedEvents[0].City+" ,"+ assignedEvents[0].cdState+" "+ assignedEvents[0].PostalCode;

            System.Diagnostics.Debug.WriteLine("Populating event fields ");

            EventName.Text = "Event Name: " + assignedEvents[0].EventName;
            EventID.Text = "Event ID: " + assignedEvents[0].EventID;
            EventAddress.Text = assignedEvents[0].AddressLine1;
            if (assignedEvents[0].AddressLine2 == "None" || assignedEvents[0].AddressLine2 == "NA" )
            {
                EventAddress2.Text = "";
            }
            else
            {
                EventAddress2.Text = assignedEvents[0].AddressLine2;
            }
            City.Text = assignedEvents[0].City;
            myState.Text = assignedEvents[0].cdState;
            PostalCode.Text = assignedEvents[0].PostalCode;
            ClubName.Text = assignedEvents[0].ClubName;
            cdNotes.Text = assignedEvents[0].Notes;
            eventDatePicker.Date = Convert.ToDateTime(assignedEvents[0].EventDate);

            maxarray = eventcounter-1;
            System.Diagnostics.Debug.WriteLine(" Max Array is " + maxarray);

            if (assignedEvents[0].Attr1.Contains("A") == false)
            {
                EventName.IsEnabled = false;
                EventID.IsEnabled = false;
                EventAddress.IsEnabled = false;
                EventAddress2.IsEnabled = false;
                City.IsEnabled = false;
                myState.IsEnabled = false;
                PostalCode.IsEnabled = false;
                cdNotes.IsEnabled = false;
                eventDatePicker.IsEnabled = false;
            }
            else
            {
                EventName.IsEnabled = true;
                EventID.IsEnabled = true;
                EventAddress.IsEnabled = true;
                EventAddress2.IsEnabled = true;
                City.IsEnabled = true;
                myState.IsEnabled = true;
                PostalCode.IsEnabled = true;
                cdNotes.IsEnabled = true;
                eventDatePicker.IsEnabled = true;
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

        public cdMyEvents(Account loginAccount)
        {
            InitializeComponent();
            myAccount = loginAccount;
            getEvents();
        }
    }
}
