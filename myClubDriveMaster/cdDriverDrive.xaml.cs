using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdDriverDrive : ContentPage
    {
        Account loginAccount = new Account();
        getDriver mystudArray = new getDriver();
        int maxarray = -1;

        int counter = 0;

        void cdNext(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Next Button");
            counter = counter + 1;
            StudentName.Text = "Student Name: "+mystudArray.DriverAlloc[counter].Attr6 + " "+ mystudArray.DriverAlloc[counter].Attr7;
            String myaddline2 = "";
            if (mystudArray.DriverAlloc[counter].AddressLine2 == "NA")
            {
                myaddline2 = "";
            }
            else
            {
                myaddline2 = mystudArray.DriverAlloc[counter].AddressLine2 + " , ";
            }
            DestinationAddress.Text = "Destination Address : "+ mystudArray.DriverAlloc[counter].AddressLine1 + " , " + myaddline2 ;
            DestinationAddress2.Text = "                                       " + mystudArray.DriverAlloc[counter].City + " , " + mystudArray.DriverAlloc[counter].State + " " + mystudArray.DriverAlloc[counter].PostalCode;

            if (counter >= maxarray)
            {
                NextButton.IsEnabled = false;
                if (counter !=0)
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
            System.Diagnostics.Debug.WriteLine(" Clicked Next Button");
            counter = counter - 1;
            StudentName.Text = "Student Name: " + mystudArray.DriverAlloc[counter].Attr6 + " " + mystudArray.DriverAlloc[counter].Attr7;
            String myaddline2 = "";
            if (mystudArray.DriverAlloc[counter].AddressLine2 == "NA")
            {
                myaddline2 = "";
            }
            else
            {
                myaddline2 = mystudArray.DriverAlloc[counter].AddressLine2 + " , ";
            }
            DestinationAddress.Text = "Destination Address : " + mystudArray.DriverAlloc[counter].AddressLine1 + " , " + myaddline2;
            DestinationAddress2.Text = "                                       " + mystudArray.DriverAlloc[counter].City + " , " + mystudArray.DriverAlloc[counter].State + " " + mystudArray.DriverAlloc[counter].PostalCode;

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

        async void cdDrive(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Drive Button");
            var tpage = new cdTrackDriver(loginAccount);
            await Navigation.PushModalAsync(tpage);

        }


        async void cdHome(object sender, System.EventArgs e)
        {
           System.Diagnostics.Debug.WriteLine(" Clicked Home Button");
            var tpage = new cdHome(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void getStudentData(Account logAccount)
        {
            //Getting Student Information
          try
          {
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "DriverIDAllocationIDindex";
            qryAcct.ColName = "DriverID";
            qryAcct.ColValue = logAccount.UserName;

            getDriver myStudentArray = new getDriver();
            DriverAlloc pubStudentInfo = new DriverAlloc();
            cdCallAPI mycallAPI = new cdCallAPI();

            var jsreponse = await mycallAPI.cdcallDriverAllocGET(qryAcct);
            myStudentArray = JsonConvert.DeserializeObject<getDriver>((string)jsreponse);
            mystudArray = myStudentArray;

 
                foreach (var dalloc in myStudentArray.DriverAlloc)
                {
                    maxarray = maxarray + 1;
                }

            StudentName.Text = "Student Name: " + mystudArray.DriverAlloc[counter].Attr6 + " " + mystudArray.DriverAlloc[counter].Attr7;
            String myaddline2 = "";
            if (mystudArray.DriverAlloc[counter].AddressLine2 == "NA")
            {
                myaddline2 = "";
            }
            else
            {
                myaddline2 = mystudArray.DriverAlloc[counter].AddressLine2 + " , ";
            }
            DestinationAddress.Text = "Destination Address : " + mystudArray.DriverAlloc[counter].AddressLine1 + " , " + myaddline2;
            DestinationAddress2.Text = "                                       " + mystudArray.DriverAlloc[counter].City + " , " + mystudArray.DriverAlloc[counter].State + " " + mystudArray.DriverAlloc[counter].PostalCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
                await DisplayAlert("No Student Allocation Found", "No Student Allocation Found", "ok");
            }
            if (counter >= maxarray)
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

        public cdDriverDrive(Account logAccount)
        {
            InitializeComponent();
            loginAccount = logAccount;

            //Call get student information

            getStudentData(logAccount);

        }
    }
}
