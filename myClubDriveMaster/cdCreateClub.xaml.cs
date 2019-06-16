using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace myClubDriveMaster
{

    public partial class cdCreateClub : ContentPage
    {
        Account myAccount = new Account();

        async void cdHome(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(myAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void cdSubmit(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Submit Button");
            if (ClubName.Text == null || CubAddress.Text == null || City.Text == null || myState.Text == null || PostalCode.Text == null)
            {
               await DisplayAlert("Enterable fields cannot be null. ","Please enter all the fields", "ok");
            }
            else
            {
                Club thisClub = new Club();
                cdCallAPI mycallAPI = new cdCallAPI();

                thisClub.ClubName = ClubName.Text;
                thisClub.AddressLine1 = CubAddress.Text;
                thisClub.City = City.Text;
                thisClub.cdState = myState.Text;
                thisClub.PostalCode = PostalCode.Text;
                thisClub.ClubID = ClubName.Text.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                if (thisClub.AddressLine2 == null )
                {
                    thisClub.AddressLine2 = "None";
                }
                thisClub.ClubReg = "NA";
                thisClub.AddressLine3 = "NA";
                thisClub.Attr1 = "NA";
                thisClub.Attr2 = "NA";
                thisClub.Attr3 = "NA";
                thisClub.Attr4 = "NA";
                thisClub.Attr5 = "NA";
                thisClub.Attr6 = "NA";
                thisClub.Attr7 = "NA";
                thisClub.Attr8 = "NA";
                thisClub.Attr9 = "NA";
                thisClub.Attr10 = "NA";

                var jsresponse = await mycallAPI.cdcallClubsPUT(thisClub);

                if (jsresponse.ToString().Contains("ValidationException"))
                {
                    System.Diagnostics.Debug.WriteLine(" Club creation call failed " + jsresponse);
                    var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                    createStatus.Text = "Club Creation Failed. " + myerror.message;
                }
            }

        }

        public cdCreateClub(Account loginAccount)
        {
            InitializeComponent();
            myAccount = loginAccount;

        }
    }
}
