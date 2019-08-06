using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Mail;
using Xamarin.Forms;
using Plugin.Geolocator;

namespace myClubDriveMaster
{
    public partial class cdRiderDrive : ContentPage
    {
        public Account plogaccount = new Account();

        public cdRiderDrive(Account logAccount)
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine(" In Rider Drive Page");

            StudentName.Text = "Student Name: "+ logAccount.FirstName + " " + logAccount.LastName;

            plogaccount = logAccount;

            callGetDriver(logAccount);

        }

        async void callGetDriver(Account logAccount)
        {
            try 
            { 
                cdQueryAttr qryAcct = new cdQueryAttr();
                qryAcct.ColIndex = "IndexName";
                qryAcct.IndexName = "StudentIDindex";
                qryAcct.ColName = "StudentID";
                qryAcct.ColValue = logAccount.UserName;

                getDriver myDriverArray = new getDriver();
                DriverAllocation pubDriverInfo = new DriverAllocation();
                cdCallAPI mycallAPI = new cdCallAPI();

                var jsreponse = await mycallAPI.cdcallDriverAllocGET(qryAcct);
                myDriverArray = JsonConvert.DeserializeObject<getDriver>((string)jsreponse);

                pubDriverInfo = myDriverArray.DriverAllocation[0];

                DriverName.Text = "Driver Name: " + pubDriverInfo.DriverName;
                CarType.Text = "Car Type: "+ pubDriverInfo.Attr3;
                LicensePlate.Text = "License Plate: "+ pubDriverInfo.Attr4;
                DestinationAddress.Text = "Address: "+ pubDriverInfo.Attr1;
                DestinationAddress2.Text = "               "+pubDriverInfo.Attr2;
             }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Clubs Loop " + ex);
                await DisplayAlert("Action", "Update Status Failed", "OK");
            }

}

        async void cdDrive(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked send location Button");

            try
            {
                cdCallAPI mycallAPI = new cdCallAPI();
                Plugin.Geolocator.Abstractions.Position mypos = await mycallAPI.GetCurrentPosition();
                string cPosLat = mypos.Latitude.ToString();
                string cPosLong = mypos.Longitude.ToString();

                String mailSubject = "Student "+plogaccount.FirstName+" "+plogaccount.LastName+" location details";
                String mailBody = "Location of student " + plogaccount.FirstName + " " + plogaccount.LastName + " "+ cPosLat+" "+cPosLong;

                var myresult = mycallAPI.cdSendEmail(mailSubject, plogaccount.Attr1, mailBody);

                System.Diagnostics.Debug.WriteLine(" Result is " + myresult.ToString());

                Status.Text = "Sent location " + cPosLat +" "+ cPosLong +" to "+ plogaccount.Attr1 + " Successfully";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(" Result is " + ex);

                await DisplayAlert("Failed to get data. Please try later.", "Failed to get data. Please try later.", "OK");
            }
        }

        async void cdLogout(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Logout Button");
            var tpage = new MainPage();
            await Navigation.PushModalAsync(tpage);
        }


    }
}
