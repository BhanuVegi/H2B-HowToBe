using System;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace myClubDriveMaster
{
    public partial class MainPage : ContentPage
    {
        void cdRegistration(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked Registraion Button");
        }

        async void cdLogin(object sender, System.EventArgs e)
        {
        
            String myusername = eusername.Text;
            String mypassword = epassword.Text;
            cdCallAPI mycallAPI = new cdCallAPI();
            loginResponse lresp = new loginResponse();

            //Authenticating user
            System.Diagnostics.Debug.WriteLine(" Authenticating user");

            var jsreponse = await mycallAPI.cdLoginAccount(myusername, mypassword);
            lresp = JsonConvert.DeserializeObject<loginResponse>((string)jsreponse);

            if (lresp.status == "success")
            {
                System.Diagnostics.Debug.WriteLine(" Authentication Successful ");
                loginMessage.Text = "Login is Successful";
                //Getting Account information
                //Set Query Object
                Account myaccount = new Account();
                cdQueryAttr qryAcct = new cdQueryAttr();
                qryAcct.ColIndex = "IndexName";
                qryAcct.IndexName = "UserNameindex";
                qryAcct.ColName = "UserName";
                qryAcct.ColValue = myusername;

                getAccounts myAccountsArray = new getAccounts();

                jsreponse = await mycallAPI.cdcallAccountsGET(qryAcct);
                myAccountsArray = JsonConvert.DeserializeObject<getAccounts>((string)jsreponse);

                System.Diagnostics.Debug.WriteLine(" Before Account array "+ jsreponse);

                myaccount = myAccountsArray.Account[0];

                System.Diagnostics.Debug.WriteLine(" After Account array ");

                System.Diagnostics.Debug.WriteLine("Role for " + myaccount.FirstName + " " + myaccount.LastName + " is " + myaccount.Role);

                if (myaccount.Role.Substring(0,1) == "D" || myaccount.Role.Substring(0, 1) == "P" || myaccount.Role.Substring(0, 1) == "A")
                {
                    var tpage = new cdHome(myaccount);
                    await Navigation.PushModalAsync(tpage);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(" Navigating to Rider Page ");
                    var tpage = new cdRiderDrive(myaccount);
                    await Navigation.PushModalAsync(tpage);
                }

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Authentication Failed ");
                loginMessage.Text = "Invalid Username or Password";
            }

        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
