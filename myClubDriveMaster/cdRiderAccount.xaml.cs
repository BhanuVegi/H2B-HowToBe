using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdRiderAccount : ContentPage
    {
        Account myAccount = new Account();
        Account regAccount = new Account();
        int accCreated = 0;

        async void cdHome(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(myAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void cdAClub(object sender, System.EventArgs e)
        {
            if (accCreated == 0)
            {
                var response = await createStudAccount();
                await DisplayAlert("Action", "Account creation " + response, "Ok");

                if ( response.ToString() == "success" )
                {
                    var tpage = new cdAssignClubs(myAccount, regAccount);
                    await Navigation.PushModalAsync(tpage);
                }

            }
            else
            { 
                var tpage = new cdAssignClubs(myAccount, regAccount);
                await Navigation.PushModalAsync(tpage);
            }
        }
        public async Task<JToken> createStudAccount()
        {
            //Populate regiatration data
            System.Diagnostics.Debug.WriteLine(" In create student account "+ regAccount.UserName);
            regAccount.AccountID = cdUserName.Text;
            System.Diagnostics.Debug.WriteLine(" In create student account " + regAccount.AccountID);
            regAccount.UserName = cdUserName.Text;
            regAccount.EmailAddress = cdEmail.Text;
            regAccount.FirstName = cdFirstName.Text;
            regAccount.LastName = cdLastName.Text;
            regAccount.AddressLine1 = cdAddress1.Text;
            regAccount.AddressLine2 = "None";
            regAccount.City = cdCity.Text;
            regAccount.cdState = cdState.Text;
            regAccount.PostalCode = cdPostalCode.Text;
            regAccount.Phone = cdPhone.Text;
            regAccount.MiddleName = "None";
            if (cdPhone.Text == null)
            {
                regAccount.Phone = "None";
            }

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
                return "failed";
            }
            else
            {
                regAccount.AccountStatus = "Approved";
                regAccount.AddressLine3 = "None";
                regAccount.County = "NA";
                regAccount.Destination = "NA";
                regAccount.ParentID = myAccount.UserName;
                regAccount.Role = regAccount.Role + "R";
                regAccount.School = "None";
                regAccount.SchoolID = "None";
                regAccount.Teacher = "None";
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
                    return "failed " + myerror.message;
                }
                else
                {
                    accCreated = 1;
                    signupAccount mysignupAccount = new signupAccount();
                    mysignupAccount.email = cdEmail.Text;
                    mysignupAccount.username = cdUserName.Text;
                    mysignupAccount.password = cdPassword.Text;

                    var signupRespose = await mycallAPI.cdCreateSignup(mysignupAccount);

                    if (signupRespose.ToString().Contains("ValidationException"))
                    {
                        System.Diagnostics.Debug.WriteLine(" Account signup failed " + jsresponse);
                        var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                        await DisplayAlert("Login creation failed","Login creation failed. "+jsresponse, "OK");
                        return "failed";
                    }

                    return "success";
                }
            }

        }

        async void cdSubmit(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Submit button clicked");
            var response = await createStudAccount();
            await DisplayAlert("Action", "Account creation "+response, "Ok");

        }

        public cdRiderAccount(Account loginAccount)
        {
            InitializeComponent();

            myAccount = loginAccount;

        }
    }
}
