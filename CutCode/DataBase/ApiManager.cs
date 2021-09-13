using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public class ApiManager : IApiManager
    {
        public HttpClient client;
        private string sharingApiAddr = "Something";
        private string sharingApiPassw = "Something";
        public ApiManager()
        {
            client = new HttpClient();
        }

        public async Task<ShareAddResponseModel> ShareAddRequest(ShareAddRequestModel request)
        {
            ShareAddResponseModel returnResponse;
            request.Passw = sharingApiPassw;

            var requestJson = JsonConvert.SerializeObject(request);
            var requestData = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(sharingApiAddr, requestData);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                returnResponse = JsonConvert.DeserializeObject<ShareAddResponseModel>(responseString);
            }
            else
            {
                returnResponse = new ShareAddResponseModel() { Done=false, Message="Error occurred from the server!"};
            }
            return returnResponse;
        }

        public async Task<ShareGetResponseModel> ShareGetRequest(ShareGetRequestModel request)
        {
            ShareGetResponseModel returnResponse;
            request.Passw = sharingApiPassw;

            var requestJson = JsonConvert.SerializeObject(request);
            var requestData = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(sharingApiAddr, requestData);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                returnResponse = JsonConvert.DeserializeObject<ShareGetResponseModel>(responseString);
            }
            else
            {
                returnResponse = new ShareGetResponseModel() { Done = false, Message = "Error occurred from the server!" };
            }
            return returnResponse;
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);
        public bool IsInternetAvailable()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }
    }
}
