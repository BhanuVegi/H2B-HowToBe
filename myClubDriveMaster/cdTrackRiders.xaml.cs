using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace myClubDriveMaster
{

    public partial class cdTrackRiders : ContentPage
    {
        public int exitloop = 0;
        Account loginAccount = new Account();

        private async void trackToCurrentLoc(String trackKey)
        {
            Double cPosLat = 0.00;
            Double cPosLong = 0.00;

            var mypos = new Position(cPosLat, cPosLong);

            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = mypos,
                Label = "My Location",
                Address = ""
            };

            System.Diagnostics.Debug.WriteLine(" Getting Driver Locations ");
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "cddatetimeindex";
            qryAcct.ColName = "cddatetime";
            qryAcct.ColValue = trackKey;

            getTrips tripArray = new getTrips();
            cdCallAPI mycallAPI = new cdCallAPI();

            while ( exitloop == 0 )
            { 
                try 
                { 

                        var jsreponse = await mycallAPI.cdcallTrackLocGET(qryAcct);
                        tripArray = JsonConvert.DeserializeObject<getTrips>((string)jsreponse);

                        System.Diagnostics.Debug.WriteLine(" Getting Driver Locations "+jsreponse);

                         cPosLat = Convert.ToDouble(tripArray.Trips[0].cdLatitude);
                         cPosLong = Convert.ToDouble(tripArray.Trips[0].cdLongitude);

                        var map = new Map(
                                            MapSpan.FromCenterAndRadius(
                                             new Position(cPosLat, cPosLong), Distance.FromMiles(0.3)))
                        {
                            IsShowingUser = true,
                            HeightRequest = 675,
                            WidthRequest = 960,
                            VerticalOptions = LayoutOptions.FillAndExpand
                        };

                        foreach (var tripLoc in tripArray.Trips)
                        {
                            cPosLat = Convert.ToDouble(tripLoc.cdLatitude);
                            cPosLong = Convert.ToDouble(tripLoc.cdLongitude);
                            mypos = new Position(cPosLat, cPosLong);

                        }

                        pin.Position = mypos;

                        map.Pins.Add(pin);
                        mapStack.Children.Add(map);

                        map.MoveToRegion(MapSpan.FromCenterAndRadius(mypos, Distance.FromMiles(1)));

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(" In exception " + ex);
                    }
                await Task.Delay(5000);
            }
        }

        async void completeTrack(object sender, System.EventArgs e)
        {
            exitloop = 1;
            var tpage = new cdHome(loginAccount);
            await Navigation.PushModalAsync(tpage);
        }

        public cdTrackRiders(String trackKey, Account logAccount)
        {
            InitializeComponent();
            loginAccount = logAccount;
            trackToCurrentLoc(trackKey);
        }
    }

}
