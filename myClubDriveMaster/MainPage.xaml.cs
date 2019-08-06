using System;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace myClubDriveMaster
{
    public partial class MainPage : ContentPage
    {
        async void cdloginHelp(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked on Login Help "+ App.cdGetLoginHelp);
            bool x = await DisplayAlert("Login Help", "Please click Ok button which will open the browser window with login screen. Click on forgot password link and follow the steps to reset your password.", "ok", "cancel");
            if (x==true)
            { 
            Device.OpenUri(new Uri(App.cdGetLoginHelp));
            }
            else 
            {
                System.Diagnostics.Debug.WriteLine(" Clicked on cancel button ");
            }
        }
        void cdHelp(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Clicked on Helpline");
            Device.OpenUri(new Uri(App.cdHelpline));
        }
        async void cdRegistration(object sender, System.EventArgs e)
        {
            Account pAccount = new Account();
            Club pClub = new Club();
            String pPassword = "";
            System.Diagnostics.Debug.WriteLine(" Clicked Registraion Button");
            var tpage = new cdAccounts(pAccount, pClub, pPassword);
            await Navigation.PushModalAsync(tpage);
        }

        async void cdLogin(object sender, System.EventArgs e)
        {
        
            String myusername = eusername.Text;
            String mypassword = epassword.Text;
            cdCallAPI mycallAPI = new cdCallAPI();
            loginResponse lresp = new loginResponse();

            //Authenticating user
            System.Diagnostics.Debug.WriteLine(" Authenticating user");
            System.Diagnostics.Debug.WriteLine(" Long string " + Math.Abs(DateTime.Now.ToBinary()).ToString());
            System.Diagnostics.Debug.WriteLine(" Hash Code " + Math.Abs(DateTime.Now.GetHashCode()));

            var jsreponse = await mycallAPI.cdLoginAccount(myusername, mypassword);
            lresp = JsonConvert.DeserializeObject<loginResponse>((string)jsreponse);
            App.mylresp = lresp;

            if (lresp.status == "success")
            {
                try
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

                    var tpage = new cdHome(myaccount);
                    await Navigation.PushModalAsync(tpage);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(" Invalid login "+ ex);
                    await DisplayAlert("Unable to Login", "Unable to Login", "OK");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Authentication Failed ");
                await DisplayAlert("Invalid Username or Password", "Invalid Username or Password", "OK");
            }

        }

        async void getMyParam()
        {
            App.getAPIURLs = "NewTestValue";
            cdCallAPI mycallAPI = new cdCallAPI();
            var myParamResp = await mycallAPI.cdSetParameters("Stage");
            System.Diagnostics.Debug.WriteLine("socialText Login is " + App.cdSocial);
        }

        public MainPage()
        {
            InitializeComponent();
            getMyParam();
            System.Diagnostics.Debug.WriteLine("Date time is " + DateTime.Today.Date.ToShortDateString());
        }
    }
}
