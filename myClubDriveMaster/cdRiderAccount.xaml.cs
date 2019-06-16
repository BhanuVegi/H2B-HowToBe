using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdRiderAccount : ContentPage
    {
        Account myAccount = new Account();

        async void cdHome(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(myAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void cdSubmit(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Submit button clicked");

            Account regAccount = new Account();

            //Populate regiatration data
            if (regAccount.UserName == null ||
                regAccount.FirstName == null ||
                regAccount.AddressLine1 == null ||
                regAccount.cdState == null ||
                regAccount.City == null ||
                regAccount.PostalCode == null ||
                regAccount.EmailAddress == null ||
                regAccount.LastName == null)
            {
                await DisplayAlert("Action", "Key attributes cannot be null. Please go to personal information and enter the same", "Ok");
            }
            else
            {
                regAccount.AccountID = regAccount.UserName;
                regAccount.UserName = cdUserName.Text;
                regAccount.EmailAddress = cdEmail.Text;
                regAccount.FirstName = cdFirstName.Text;
                regAccount.MiddleName = cdMiddleName.Text;
                regAccount.LastName = cdLastName.Text;
                regAccount.AddressLine1 = cdAddress1.Text;
                regAccount.AddressLine2 = cdAddress2.Text;
                regAccount.City = cdCity.Text;
                regAccount.cdState = cdState.Text;
                regAccount.PostalCode = cdPostalCode.Text;
                regAccount.Phone = cdPhone.Text;

                if (regAccount.MiddleName == null)
                {
                    regAccount.MiddleName = "None";
                }
                regAccount.AccountStatus = "Approved";
                regAccount.AddressLine3 = "None";
                regAccount.County = "NA";
                regAccount.Destination = "NA";
                regAccount.ParentID = myAccount.UserName;
                regAccount.Role = regAccount.Role + "R";

                if (regAccount.Phone == null)
                {
                    regAccount.Phone = "None";
                }
                if (regAccount.School == null)
                {
                    regAccount.School = "None";
                }
                if (regAccount.SchoolID == null)
                {
                    regAccount.SchoolID = "None";
                }
                if (regAccount.Teacher == null)
                {
                    regAccount.Teacher = "None";
                }
                regAccount.Attr1 = "NA";
                regAccount.Attr2 = "NA";
                regAccount.Attr3 = "NA";
                regAccount.Attr4 = "NA";
                regAccount.Attr5 = "NA";
                regAccount.Attr6 = "NA";
                regAccount.Attr7 = "NA";
                regAccount.Attr8 = "NA";
                regAccount.Attr9 = "NA";
                regAccount.Attr10 = "NA";

                cdCallAPI mycallAPI = new cdCallAPI();
                var jsresponse = await mycallAPI.cdcallAccountsPUT(regAccount);

                if (jsresponse.ToString().Contains("ValidationException"))
                {
                    System.Diagnostics.Debug.WriteLine(" Account creation call failed " + jsresponse);
                    var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                    createStatus.Text = "Account Creation Failed. " + myerror.message;
                }
            }
        }

        public cdRiderAccount(Account loginAccount)
        {
            InitializeComponent();

            myAccount = loginAccount;

        }
    }
}
