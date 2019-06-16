using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdMyEvents : ContentPage
    {
        Account myAccount = new Account();
        getEvents myEvents = new getEvents();
        List<cdEvents> assignedEvents = new List<cdEvents>();

        int maxarray = -1;
        int counter = 0;

        async void cdHome(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(myAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void cdRiders(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(myAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void cdSubmit(object sender, System.EventArgs e)
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

        }

        void cdPervious(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Previous Button");
            counter = counter - 1;

            EventName.Text = "Event Name: " + assignedEvents[counter].EventName;
            EventID.Text = "Clud ID: " + assignedEvents[counter].EventID;
            EventAddress.Text = assignedEvents[counter].AddressLine1 + " " + assignedEvents[counter].AddressLine2;
            City.Text = assignedEvents[counter].City;
            myState.Text = assignedEvents[counter].cdState;
            PostalCode.Text = assignedEvents[counter].PostalCode;

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

            EventName.Text = "Event Name: " + assignedEvents[counter].EventName;
            EventID.Text = "Clud ID: " + assignedEvents[counter].EventID;
            EventAddress.Text = assignedEvents[counter].AddressLine1 + " " + assignedEvents[counter].AddressLine2;
            City.Text = assignedEvents[counter].City;
            myState.Text = assignedEvents[counter].cdState;
            PostalCode.Text = assignedEvents[counter].PostalCode;

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

            System.Diagnostics.Debug.WriteLine(" Getting Events from Event members");

            getEventMembers myEventMembers = new getEventMembers();
            cdCallAPI mycallAPI = new cdCallAPI();

            var jsreponse = await mycallAPI.cdcallEventMembersGET(qryAcct);
            myEventMembers = JsonConvert.DeserializeObject<getEventMembers>((string)jsreponse);

            System.Diagnostics.Debug.WriteLine(" Event Member payload is " + jsreponse);

            try
            {
                foreach (var stacc in myEventMembers.EventMember)
                {
                    maxarray = maxarray + 1;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
            }

            EventName.Text = "Event Name: " + assignedEvents[0].EventName;
            EventID.Text = "Event ID: " + assignedEvents[0].EventID;
            EventAddress.Text = assignedEvents[0].AddressLine1 + " " + assignedEvents[0].AddressLine2;
            City.Text = assignedEvents[0].City;
            myState.Text = assignedEvents[0].cdState;
            PostalCode.Text = assignedEvents[0].PostalCode;

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

        public cdMyEvents(Account loginAccount)
        {
            InitializeComponent();
            myAccount = loginAccount;
        }
    }
}
