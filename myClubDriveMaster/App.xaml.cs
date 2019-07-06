using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace myClubDriveMaster
{
    public partial class App : Application
    {
        public static String getAPIURLs = "";
        public static String cdAccountAPIGetGlobal = "";
        public static String cdAccountAPIPutPost = "";
        public static String cdDriverAllocURLGet = "";
        public static String cdDriverAllocURLPutPost = "";
        public static String cdAuthAPIURL = "";
        public static String cdTrackLocPutPost = "";
        public static String cdTrackLocGet = "";
        public static String cdClubAPIGet = "";
        public static String cdClubAPIPutPost = "";
        public static String cdClubMemberAPIGet = "";
        public static String cdClubMemberAPIPutPost = "";
        public static String cdEventAPIGet = "";
        public static String cdEventAPIPutPost = "";
        public static String cdEventRegAPIGet = "";
        public static String cdEventRegAPIPutPost = "";
        public static String cdAccountKey = "";
        public static String cdAuthKey = "";
        public static String cdClubKey = "";
        public static String cdClubMemberKey = "";
        public static String cdDriverAllocationKey = "";
        public static String cdDriverLocationKey = "";
        public static String cdEventKey = "";
        public static String cdEventRegKey = "";

        public App()
        {
            InitializeComponent();

            getAPIURLs = "TestValue";

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
