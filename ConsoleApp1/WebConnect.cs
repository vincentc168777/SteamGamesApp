using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task getSteamInfo(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(data);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
