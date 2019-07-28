using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdCreateEvents : ContentPage
    {
        Account myAccount = new Account();
        int maxarray = -1;
        String myEventDate = "";

        async void cdHome(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(myAccount);
            await Navigation.PushModalAsync(tpage);
        }

        void OnDateSelected(object sender, System.EventArgs e)
        {
            myEventDate = eventDatePicker.Date.ToString();
            System.Diagnostics.Debug.WriteLine(" Event date is "+ myEventDate);

        }

        async void cdSubmit(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Submit Button");
            if (EventName.Text == null || EventAddress.Text == null || City.Text == null || myState.Text == null || PostalCode.Text == null || picker.SelectedItem.ToString() == null)
            {
                await DisplayAlert("Enterable fields cannot be null. ", "Please enter all the fields", "ok");
            }
            else
            {
                cdEvents thisEvent = new cdEvents();
                cdCallAPI mycallAPI = new cdCallAPI();

                thisEvent.EventName = EventName.Text;
                thisEvent.AddressLine1 = EventAddress.Text;
                thisEvent.City = City.Text;
                thisEvent.cdState = myState.Text;
                thisEvent.PostalCode = PostalCode.Text;
                thisEvent.EventID = EventName.Text.Substring(0, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                if (EventAddress2.Text == null)
                {
                    thisEvent.AddressLine2 = "None";
                }
                else 
                {
                    thisEvent.AddressLine2 = EventAddress2.Text;
                }

                thisEvent.AddressLine3 = "NA";
                thisEvent.Notes = cdNotes.Text;
                thisEvent.ClubAdmin = myAccount.UserName;
                String[] mysa = new string[2];
                char[] mysep = "|".ToCharArray();
                mysa = picker.SelectedItem.ToString().Split(mysep);
                System.Diagnostics.Debug.WriteLine(" Club Name "+ mysa[0]+" Club ID "+ mysa[1]);
                thisEvent.ClubName = mysa[0];
                thisEvent.ClubID = mysa[1];
                thisEvent.EventDate = myEventDate;
                thisEvent.PhoneNumber = "0000000000";
                thisEvent.Attr1 = DateTime.Today.Date.ToShortDateString();
                thisEvent.Attr2 = "NA";
                thisEvent.Attr3 = "NA";
                thisEvent.Attr4 = "NA";
                thisEvent.Attr5 = "NA";
                thisEvent.Attr6 = "NA";
                thisEvent.Attr7 = "NA";
                thisEvent.Attr8 = "NA";
                thisEvent.Attr9 = "NA";
                thisEvent.Attr10 = "NA";

                try 
                { 
                    var jsresponse = await mycallAPI.cdcallEventsPUT(thisEvent);

                    System.Diagnostics.Debug.WriteLine(" Response received is " + jsresponse);

                    if (jsresponse.ToString().Contains("ValidationException"))
                    {
                        System.Diagnostics.Debug.WriteLine(" Event creation call failed " + jsresponse);
                        var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                        createStatus.Text = "Event Creation Failed. " + myerror.message;
                    }

                    var eresp = mycallAPI.cdcallEmailPUT(thisEvent.EventID,thisEvent.EventName,thisEvent.ClubID,thisEvent.ClubName,thisEvent.EventDate);
                    await DisplayAlert("Event creation Successful", "Event creation Successful" , "ok");
                    var tpage = new cdHome(myAccount);
                    await Navigation.PushModalAsync(tpage);

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(" Exception is " + ex);
                    await DisplayAlert("Event creation failed", ex.ToString(), "ok");
                }
            }

        }

        async void getPickerClubInfo()
        {
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "MemberAccountIDIndex";
            qryAcct.ColName = "MemberAccountID";
            qryAcct.ColValue = myAccount.UserName;

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
                    if (myClubMembers.ClubMember[maxarray].MemberRole.Contains("A") == true)
                    { 
                        picker.Items.Add(myClubMembers.ClubMember[maxarray].ClubName + "|"+ myClubMembers.ClubMember[maxarray].ClubID);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
            }
        }

        public cdCreateEvents(Account loginAccount)
        {
            InitializeComponent();
            myAccount = loginAccount;
            eventDatePicker.MinimumDate = DateTime.Today.Date;
            getPickerClubInfo();

        }
    }
}
