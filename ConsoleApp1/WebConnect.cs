using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    internal sealed class WebConnect
    {
        private static readonly HttpClient client = new HttpClient();

        private static WebConnect connection = new WebConnect();
        private WebConnect()
        {

        }

        public static WebConnect GetWebConnectInstance()
        {
            return connection;
        }

        public async Task<AppNewsGetter> getSteamInfoAsync(string url)
        {
            AppNewsGetter news = null;
            try
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url);

                HttpResponseMessage response = await responseTask; 
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    /*when deserilazing, make sure to know what data type json has
                     * like array, obj, etc.
                     */
                    news = JsonConvert.DeserializeObject<AppNewsGetter>(data);
   
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

            return news;
        }


        public async Task GetGameID()
        {
            try
            {
                Task<HttpResponseMessage> gameId = client.GetAsync("https://api.steampowered.com/ISteamApps/GetAppList/v2");

                HttpResponseMessage gameIdResponse = await gameId;

                if (gameIdResponse.IsSuccessStatusCode)
                {
                    string gameIdData = await gameIdResponse.Content.ReadAsStringAsync();
                    //Console.WriteLine(gameIdData);

                    AppListGetter aL = JsonConvert.DeserializeObject<AppListGetter>(gameIdData);

                    foreach (App a in aL.AppGetter.Apps)
                    {
                        if (a.GameName.Contains("Dark Soul"))
                        {
                            Console.WriteLine(a.GameName);
                        }

                    }
                    
                }
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);

            }
        }
       
    }
}
