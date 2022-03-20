using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Services;
using Newtonsoft.Json;

namespace CutCode.CrossPlatform.Helpers
{
    public class UpdateReqModel
    {
        public bool NewUpdate { get; set; }
        public string UpdateVersion { get; set; }
    }
    public static class UpdateChecker
    {
        private static string _updateUrl = "https://cutcodeupdater.herokuapp.com";
        private static HttpClient client = new HttpClient();
        private static string _currentVersion = "v3.0.0";
        public static async void Run()
        {
            await Task.Delay(TimeSpan.FromSeconds(4));
            var internetAvailable = IsInternetAvailable();
            if (!internetAvailable) return;
            try
            {
                var response = await client.GetAsync($"{_updateUrl}/check_update/{_currentVersion}"); if (!response.IsSuccessStatusCode) return;
                string responseString = await response.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<UpdateReqModel>(responseString);
                if (responseJson is { NewUpdate: true })
                {
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        NotificationService.Current.CreateNotification(
                            "Update",
                            $"New version({responseJson.UpdateVersion}) is available. Download it from the release page on the Github repository", 
                            10);
                    });
                }
            }
            catch
            {
                return;
            }
        }
        
        public static bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204")) 
                    return true; 
            }
            catch
            {
                return false;
            }
        }
    }
}