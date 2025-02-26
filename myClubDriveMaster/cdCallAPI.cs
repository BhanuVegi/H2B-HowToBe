﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Plugin.Geolocator;
using System.Net.Mail;

namespace myClubDriveMaster
{

    // These set of APIs are used to call the rest end points

    public class cdCallAPI
    {
   
        // Function to set global parameters
        public async Task<JToken> cdSetParameters(String currInstance)
        {
            String callingapiurl = "https://jc7b5uxmj0.execute-api.us-west-2.amazonaws.com/Stage/schooldrive";
            var uri = new Uri(callingapiurl);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("IndexName", "IndexName");
            client.DefaultRequestHeaders.TryAddWithoutValidation("ColIndex", "InstanceIndex");
            client.DefaultRequestHeaders.TryAddWithoutValidation("ColName", "Instance");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Instance", currInstance);
            client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", "H611aknuHz2MW0PmZYyk8akWrmBdqoAz4qY2fD0j");

            var response = await client.GetAsync(uri);

            var responseJSON = await response.Content.ReadAsStringAsync();

            // System.Diagnostics.Debug.WriteLine(" Response received is ");
            // System.Diagnostics.Debug.WriteLine(responseJSON);

            if (response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(" Get Parameters API Call Successful");

                getParameters reqParameters = new getParameters();

                reqParameters = JsonConvert.DeserializeObject<getParameters>((string)responseJSON);

                foreach (var prec in reqParameters.Parameters )
                {
                    if (prec.ParameterName == "cdAccountAPIGet")
                    {
                        App.cdAccountAPIGetGlobal = prec.EndPoint;
                        App.cdAccountKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdLoginHelp")
                    {
                        App.cdGetLoginHelp = prec.EndPoint;
                    }
                    if (prec.ParameterName == "cdGlobalHelpline")
                    {
                        App.cdHelpline = prec.EndPoint;
                    }
                    if (prec.ParameterName == "cdSocialLogin")
                    {
                        App.cdSocial = prec.EndPoint;
                    }
                    if (prec.ParameterName == "cdValToken")
                    {
                        App.cdValidateToken = prec.EndPoint;
                    }
                    if (prec.ParameterName == "cdShopURL")
                    {
                        App.cdShopMyURL = prec.EndPoint;
                    }
                    if (prec.ParameterName == "cdAccountAPIPutPost")
                    {
                        App.cdAccountAPIPutPost = prec.EndPoint;
                        App.cdAccountKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdEmailAPIGet")
                    {
                        App.cdEmailAPIGetGlobal = prec.EndPoint;
                        App.cdEmailRegKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdEmailAPIPutPost")
                    {
                        App.cdEmailAPIPutPost = prec.EndPoint;
                        App.cdEmailRegKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdDriverAllocURLGet")
                    {
                        App.cdDriverAllocURLGet = prec.EndPoint;
                        App.cdDriverAllocationKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdDriverAllocURLPutPost")
                    {
                        App.cdDriverAllocURLPutPost = prec.EndPoint;
                        App.cdDriverAllocationKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdAuthAPIURL")
                    {
                        App.cdAuthAPIURL = prec.EndPoint;
                        App.cdAuthKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdTrackLocGet")
                    {
                        App.cdTrackLocGet = prec.EndPoint;
                        App.cdDriverLocationKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdTrackLocPutPost")
                    {
                        App.cdTrackLocPutPost = prec.EndPoint;
                        App.cdDriverLocationKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdClubAPIGet")
                    {
                        App.cdClubAPIGet = prec.EndPoint;
                        App.cdClubKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdClubAPIPutPost")
                    {
                        App.cdClubAPIPutPost = prec.EndPoint;
                        App.cdClubKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdClubMemberAPIGet")
                    {
                        App.cdClubMemberAPIGet = prec.EndPoint;
                        App.cdClubMemberKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdClubMemberAPIUAGet")
                    {
                        App.cdClubMemberAPIUAGet = prec.EndPoint;
                        App.cdClubMemberKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdClubMemberAPIPutPost")
                    {
                        App.cdClubMemberAPIPutPost = prec.EndPoint;
                        App.cdClubMemberKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdEventAPIGet")
                    {
                        App.cdEventAPIGet = prec.EndPoint;
                        App.cdEventKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdEventAPIPutPost")
                    {
                        App.cdEventAPIPutPost = prec.EndPoint;
                        App.cdEventKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdEventRegAPIGet")
                    {
                        App.cdEventRegAPIGet = prec.EndPoint;
                        App.cdEventRegKey = prec.AccessKey;
                    }
                    if (prec.ParameterName == "cdEventRegAPIPutPost")
                    {
                        App.cdEventRegAPIPutPost = prec.EndPoint;
                        App.cdEventRegKey = prec.AccessKey;
                    }
                }

                System.Diagnostics.Debug.WriteLine("cdAccountAPIGetGlobal = " + App.cdAccountAPIGetGlobal);
                System.Diagnostics.Debug.WriteLine("cdAccountAPIPutPost = " + App.cdAccountAPIPutPost);
                System.Diagnostics.Debug.WriteLine("cdDriverAllocURLGet = " + App.cdDriverAllocURLGet);
                System.Diagnostics.Debug.WriteLine("cdDriverAllocURLPutPost = " + App.cdDriverAllocURLPutPost);
                System.Diagnostics.Debug.WriteLine("cdAuthAPIURL = " + App.cdAuthAPIURL);
                System.Diagnostics.Debug.WriteLine("cdTrackLocPutPost = " + App.cdTrackLocPutPost);
                System.Diagnostics.Debug.WriteLine("cdTrackLocGet = " + App.cdTrackLocGet);
                System.Diagnostics.Debug.WriteLine("cdClubAPIGet = " + App.cdClubAPIGet);
                System.Diagnostics.Debug.WriteLine("cdClubAPIPutPost = " + App.cdClubAPIPutPost);
                System.Diagnostics.Debug.WriteLine("cdClubMemberAPIGet = " + App.cdClubMemberAPIGet);
                System.Diagnostics.Debug.WriteLine("cdClubMemberAPIPutPost = " + App.cdClubMemberAPIPutPost);
                System.Diagnostics.Debug.WriteLine("cdEventAPIGet = " + App.cdEventAPIGet);
                System.Diagnostics.Debug.WriteLine("cdEventAPIPutPost = " + App.cdEventAPIPutPost);
                System.Diagnostics.Debug.WriteLine("cdEventRegAPIGet = " + App.cdEventRegAPIGet);
                System.Diagnostics.Debug.WriteLine("cdEventRegAPIPutPost = " + App.cdEventRegAPIPutPost);
                System.Diagnostics.Debug.WriteLine("cdAccountKey = " + App.cdAccountKey);
                System.Diagnostics.Debug.WriteLine("cdAuthKey = " + App.cdAuthKey);
                System.Diagnostics.Debug.WriteLine("cdClubKey = " + App.cdClubKey);
                System.Diagnostics.Debug.WriteLine("cdClubMemberKey = " + App.cdClubMemberKey);
                System.Diagnostics.Debug.WriteLine("cdDriverAllocationKey = " + App.cdDriverAllocationKey);
                System.Diagnostics.Debug.WriteLine("cdDriverLocationKey = " + App.cdDriverLocationKey);
                System.Diagnostics.Debug.WriteLine("cdEventKey = " + App.cdEventKey);
                System.Diagnostics.Debug.WriteLine("cdEventRegKey = " + App.cdEventRegKey);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Get API Call failed " + response.ReasonPhrase);
            }

            return "Success";

        }

        // Function to get Accounts
        public async Task<JToken> cdcallAccountsGET(cdQueryAttr QueryObject)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var getLocations = await mycallAPI.cdCallGetAPI(App.cdAccountAPIGetGlobal, QueryObject, App.cdAccountKey);
            return getLocations;

        }
        public async Task<JToken> cdvalidateToken()
        {
            String tokenURI = App.cdValidateToken;
            var uri = new Uri(tokenURI);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            var inputJson = JsonConvert.SerializeObject(App.mylresp);
            var inputContent = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync(tokenURI, inputContent);

            var responseJSON = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(" Get API Call Successful");

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Get API Call failed " + response.ReasonPhrase);
            }

            return "Success";

        }

        public async Task<JToken> cdcallEventsGET(cdQueryAttr QueryObject)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallGetAPI(App.cdEventAPIGet, QueryObject, App.cdEventKey);
            return response;

        }
        public async Task<JToken> cdcallClubMembersGET(cdQueryAttr QueryObject)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var getClubMembers = await mycallAPI.cdCallGetAPI(App.cdClubMemberAPIGet, QueryObject,App.cdClubKey);
            return getClubMembers;

        }
        public async Task<JToken> cdcallClubMembersUAGET(cdQueryAttr QueryObject)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var getClubMembers = await mycallAPI.cdCallGetAPI(App.cdClubMemberAPIUAGet, QueryObject, App.cdClubKey);
            return getClubMembers;

        }
        public async Task<JToken> cdcallEventMembersGET(cdQueryAttr QueryObject)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var getClubMembers = await mycallAPI.cdCallGetAPI(App.cdEventRegAPIGet, QueryObject, App.cdEventRegKey);
            return getClubMembers;

        }
        public async Task<JToken> cdcallClubsGET(cdQueryAttr QueryObject)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var getClubMembers = await mycallAPI.cdCallGetAPI(App.cdClubAPIGet, QueryObject, App.cdClubKey);
            return getClubMembers;

        }

        public async Task<JToken> cdcallTrackLocGET(cdQueryAttr QueryObject)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var getAccounts = await mycallAPI.cdCallGetAPI(App.cdTrackLocGet, QueryObject, App.cdDriverLocationKey);
            return getAccounts;

        }

        public async Task<JToken> cdSendLocation(cdLocation dloc)
        {
            System.Diagnostics.Debug.WriteLine("  Location is "+dloc.cdLongitude+" "+dloc.cdLatitude);
            System.Diagnostics.Debug.WriteLine("  Trip ID is " + dloc.TripID);
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPutAPI(App.cdTrackLocPutPost, dloc,App.cdDriverLocationKey);
            return response;
        }

        public async Task<JToken> cdcallDriverAllocGET(cdQueryAttr QueryObject)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var getDriverAlloc = await mycallAPI.cdCallGetAPI(App.cdDriverAllocURLGet, QueryObject, App.cdDriverAllocationKey);
            return getDriverAlloc;

        }
        public async Task<JToken> cdcallDriverAllocPUT(DriverAllocation dalloc)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPutAPI(App.cdDriverAllocURLPutPost, dalloc, App.cdDriverAllocationKey);
            return response;

        }
        public async Task<JToken> cdcallAccountsPUT(Account regacccount)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPutAPI(App.cdAccountAPIPutPost, regacccount, App.cdAccountKey);
            return response;

        }
        public async Task<JToken> cdcallClubsPUT(Club regClub)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPutAPI(App.cdClubAPIPutPost, regClub,App.cdClubKey);
            return response;

        }
        public async Task<JToken> cdcallEventsPUT(cdEvents regEvent)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPutAPI(App.cdEventAPIPutPost, regEvent,App.cdEventKey);
            return response;

        }
        public async Task<JToken> cdcallEventsMemberPUT(cdEventSignups regEvent)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPutAPI(App.cdEventRegAPIPutPost, regEvent, App.cdEventRegKey);
            return response;

        }
        public async Task<JToken> cdcallClubMemberPUT(ClubMembers regacccount)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPutAPI(App.cdClubMemberAPIPutPost, regacccount,App.cdClubMemberKey);
            return response;

        }
        public async Task<JToken> cdcallClubMembersPOST(cdUpdateClubMembers regacccount)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPostAPI(App.cdClubMemberAPIPutPost, regacccount, App.cdClubMemberKey);
            return response;

        }
        public async Task<JToken> cdcallEmailPUT(String EventID, String EventName, String myClubID, String ClubName, String EventDate)
        {
            const String emailcount = "3";
            String consEmails = "";
            cdEmails eventEmail = new cdEmails();
            eventEmail.EmailID = EventID;
            eventEmail.EmailCount = emailcount;
            eventEmail.EventName = EventName;
            eventEmail.ClubName = ClubName;
            eventEmail.Attr1 = EventDate;
            eventEmail.Attr2 = "None";
            eventEmail.Attr3 = "None";
            eventEmail.Attr4 = "None"; 
            eventEmail.Attr5 = "None";
            eventEmail.Attr6 = "None";
            eventEmail.Attr7 = "None";
            eventEmail.Attr8 = "None";
            eventEmail.Attr9 = "None";
            eventEmail.Attr10 = "None";
            eventEmail.EmailSubject = "Signup for new event - " + EventName;
            eventEmail.EmailBody = "Please sign up for a new event "+EventName +" on "+EventDate+" , of your "+ ClubName+" by logging on to clubhives App";
            cdCallAPI mycallAPI = new cdCallAPI();
            cdQueryAttr qryAcct = new cdQueryAttr();
            getClubMembers cdcm = new getClubMembers();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "ClubIDIndex";
            qryAcct.ColName = "ClubID";
            qryAcct.ColValue = myClubID;
            var jsreponse = await mycallAPI.cdcallAccountsGET(qryAcct);
            cdcm = JsonConvert.DeserializeObject<getClubMembers>((string)jsreponse);
            int counter = 0;

            foreach (var stacc in cdcm.ClubMember)
            {
                if (counter == 0)
                {
                    consEmails = stacc.Attr2;
                    counter = counter + 1;
                }
                else
                {
                    consEmails = consEmails +" | "+ stacc.Attr2;
                }
            }

            eventEmail.EmailAddress = consEmails;

            var response = await mycallAPI.cdCallPutAPI(App.cdEmailAPIPutPost, eventEmail, App.cdClubMemberKey);
            return response;
        }

        public async Task<JToken> cdcallAccountsPOST(cdUpdateAccount regacccount)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPostAPI(App.cdAccountAPIPutPost, regacccount, App.cdAccountKey);
            return response;

        }
        public async Task<JToken> cdcallEventsPOST(cdUpdateEvent regEvent)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPostAPI(App.cdEventAPIPutPost, regEvent,App.cdEventKey);
            return response;

        }
        public async Task<JToken> cdEventMembersPOST(cdUpdateEventMembers regEventMembers)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPostAPI(App.cdEventRegAPIPutPost, regEventMembers, App.cdEventRegKey);
            return response;

        }
        public async Task<JToken> cdcallClubsPOST(cdUpdateClub regacccount)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPostAPI(App.cdClubAPIPutPost, regacccount,App.cdClubKey);
            return response;

        }
        public async Task<JToken> cdLoginAccount(String username, String password)
        {
            loginObject myLoginObject = new loginObject();
            myLoginObject.username = username;
            myLoginObject.password = password;
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPostAPI(App.cdAuthAPIURL, myLoginObject, App.cdAuthKey);
            return response;
        }

        public async Task<JToken> cdCreateSignup(signupAccount mysignupAccount)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPutAPI(App.cdAuthAPIURL, mysignupAccount, App.cdAuthKey);
            return response;
        }

        // This is the generic Get API call
        public async Task<JToken> cdCallGetAPI(string callingapiurl, cdQueryAttr qobj, String accessKey)
        {
            var uri = new Uri(callingapiurl);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type","application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("IndexName", qobj.ColIndex);
            client.DefaultRequestHeaders.TryAddWithoutValidation("ColIndex", qobj.IndexName);
            client.DefaultRequestHeaders.TryAddWithoutValidation("ColName",qobj.ColName);
            client.DefaultRequestHeaders.TryAddWithoutValidation(qobj.ColName,qobj.ColValue);
            client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", accessKey);

            System.Diagnostics.Debug.WriteLine(" Request passed is " + qobj.IndexName +qobj.ColName + " " + qobj.ColValue);

            var response = await client.GetAsync(uri);

            var responseJSON = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(" Get API Call Successful");

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Get API Call failed "+response.ReasonPhrase);
            }

            return responseJSON;

        }

        // This is the generic Put API call
        public async Task<JToken> cdCallPutAPI(string callingapiurl, Object callingapiobject, String accessKey)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", accessKey);

            var inputJson = JsonConvert.SerializeObject(callingapiobject);
            var inputContent = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");
            System.Diagnostics.Debug.WriteLine(" Input JSON " + inputJson);
            var response = await client.PutAsync(callingapiurl, inputContent);
            var responseJSON = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(" Put API Call Successful");

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Put API Call failed " + response.ReasonPhrase);
            }

            return responseJSON;
        }

        //This is a generic POST API function
        public async Task<JToken> cdCallPostAPI(string callingapiurl, Object callingapiobject, String accessKey)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", accessKey);

            var inputJson = JsonConvert.SerializeObject(callingapiobject);
            var inputContent = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");

            System.Diagnostics.Debug.WriteLine(" Input JSON is "+ inputJson);

            var response = await client.PostAsync(callingapiurl, inputContent);
            var responseJSON = await response.Content.ReadAsStringAsync();

           if (responseJSON.Contains("ValidationException"))
            {
                System.Diagnostics.Debug.WriteLine(" Post API Call failed " + responseJSON);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Post API Call Successful");
            }

            return responseJSON;
        }

    // This API returns the current location allways

        public async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentPosition()
        {

            Plugin.Geolocator.Abstractions.Position mypos = null;

            try
            {
                var locator = Plugin.Geolocator.CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                var position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    //got a cahched position, so let's use it.
                    return position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

                if (position == null)
                    return null;

                var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                        position.Timestamp, position.Latitude, position.Longitude,
                        position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

                Debug.WriteLine(output);

                mypos = position;

                return position;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location: " + ex);
            }
             return mypos;
        }

        // This API Sends email

        public String cdSendEmail(String mailSubject, String toAddress, String mailBody)
        {
            try
            {

                Debug.WriteLine("In Send mail procedure");
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("myclubdrive@gmail.com");
                mail.To.Add(toAddress);
                mail.Subject = mailSubject;
                mail.Body = mailBody; //"Location of student " + plogaccount.FirstName + " " + plogaccount.LastName + " " + cPosLat + " " + cPosLong;
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("myclubdrive@gmail.com", "MyfirstBusiness4us$");

                SmtpServer.Send(mail);

                Debug.WriteLine("Sent email successfully");

                return "sucess";
            }
            catch (Exception ex)
            {
                 return "Failed "+ ex;
            }
        }

    }
}
