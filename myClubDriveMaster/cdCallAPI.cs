using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace myClubDriveMaster
{
    public class cdCallAPI
    {
        public String sdAccountAPIURL = "https://ctf6eu57xh.execute-api.us-west-2.amazonaws.com/Stage/schooldrive/{AccountID}";
        public String sdAccountAPIURLPOST = "https://ctf6eu57xh.execute-api.us-west-2.amazonaws.com/Stage/schooldrive";
        public String sdDriverAllocURL = "https://fjziwkczek.execute-api.us-west-2.amazonaws.com/Stage/schooldrive/:AllocationID";
        public String sdAuthAPIURL = "https://olx0fy0k5k.execute-api.us-west-2.amazonaws.com/Stage/apigateway";

        public async Task<JToken> cdcallAccountsGET(cdQueryAttr QueryObject)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var getAccounts = await mycallAPI.cdCallGetAPI(sdAccountAPIURL, QueryObject);
            return getAccounts;

        }
        public async Task<JToken> cdcallAccountsPUT(Account regacccount)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPutAPI(sdAccountAPIURLPOST, regacccount);
            return response;

        }

        public async Task<JToken> cdcallAccountsPOST(Account regacccount)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            var response = await mycallAPI.cdCallPutAPI(sdAccountAPIURLPOST, regacccount);
            return response;

        }


        public async Task<JToken> cdCallGetAPI(string callingapiurl, cdQueryAttr qobj)
        {
            var uri = new Uri(callingapiurl);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("IndexName", qobj.IndexName);
            client.DefaultRequestHeaders.Add("ColIndex", qobj.ColIndex);
            client.DefaultRequestHeaders.Add("ColName", qobj.ColName);
            client.DefaultRequestHeaders.Add(qobj.ColName, qobj.ColValue);

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
        public async Task<JToken> cdCallPutAPI(string callingapiurl, Object callingapiobject)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var inputJson = JsonConvert.SerializeObject(callingapiobject);
            var inputContent = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");

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
        public async Task<JToken> cdCallPostAPI(string callingapiurl, Object callingapiobject)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var inputJson = JsonConvert.SerializeObject(callingapiobject);
            var inputContent = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(callingapiurl, inputContent);
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
    }
}
