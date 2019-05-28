using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using System.Diagnostics;
using System.Threading;
using Plugin.Geolocator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace myClubDriveMaster
{
    public partial class cdTrackDriver : ContentPage
    {
        Account loginAccount = new Account();
        double cPosLat = 0.00;
        double cPosLong = 0.00;
        String csPosLat = "";
        String csPosLang = "";
        int exitloop = 0;

        async void completeDrive(object sender, System.EventArgs e)
        {
            exitloop = 1;
            System.Diagnostics.Debug.WriteLine(" Trip Completed ");
            var tpage = new cdHome(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }

        private async void setCurrentLoc()
        {
            cdCallAPI getPosition = new cdCallAPI();
            if (getPosition != null)
            {
                Plugin.Geolocator.Abstractions.Position mypos = await getPosition.GetCurrentPosition();
                cPosLat = Convert.ToDouble(mypos.Latitude.ToString());
                cPosLong = Convert.ToDouble(mypos.Longitude.ToString());
                csPosLat = mypos.Latitude.ToString();
                csPosLang = mypos.Longitude.ToString();
            }

        }

        private async void moveToCurrentLoc()
        {
        
                var mypos = new Position(cPosLat, cPosLong);

                //var mypos = new Position(33.850417, -118.358729);

                var map = new Map(
                MapSpan.FromCenterAndRadius(
                mypos, Distance.FromMiles(0.3)))
                {
                    IsShowingUser = true,
                    HeightRequest = 675,
                    WidthRequest = 960,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                var pin = new Pin
                {
                    Type = PinType.Generic,
                    Position = mypos,
                    Label = "My Location",
                    Address = ""
                };

                map.Pins.Add(pin);
                mapStack.Children.Add(map);

            while (exitloop == 0)
            {
                System.Diagnostics.Debug.WriteLine("Refreshing map location " + DateTime.Now.ToLocalTime().ToString());
                var npos = new Position(cPosLat, cPosLong);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(npos, Distance.FromMiles(1)));
                await Task.Delay(1000);
            }
        }

        private async void sendCurrentLocation(Account logAccount)
        {
            int counter = 0;

            while (exitloop == 0)
            {

                System.Diagnostics.Debug.WriteLine(" In the send Current location loop "+ DateTime.Now.ToLocalTime().ToString());
                cdCallAPI mycallAPI = new cdCallAPI();
                cdLocation myloc = new cdLocation();
                setCurrentLoc();
                myloc.cdLatitude = csPosLat;
                myloc.cdLongitude = csPosLang;
                myloc.driverID = logAccount.UserName;
                myloc.cddatetime = logAccount.UserName+DateTime.Now.ToShortDateString();
                myloc.TripID = logAccount.UserName + DateTime.Now.ToLocalTime().ToString();
                myloc.seqNumber = counter;
                myloc.Attr1 = "NA";
                myloc.Attr2 = "NA";
                myloc.Attr3 = "NA";
                myloc.Attr4 = "NA";
                myloc.Attr5 = "NA";
                myloc.Attr6 = "NA";
                myloc.Attr7 = "NA";
                myloc.Attr8 = "NA";
                myloc.Attr9 = "NA";
                myloc.Attr10 = "NA";
                var jsreponse = await mycallAPI.cdSendLocation(myloc);
                counter = counter + 1;
                await Task.Delay(5000);

            }

        }

        public cdTrackDriver(Account logAccount)
        {
            InitializeComponent();
            loginAccount = logAccount;
            setCurrentLoc();
            sendCurrentLocation(logAccount);
            moveToCurrentLoc();

        }
    }
}
