﻿using System;
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

        public List<App> allFetchedSteamGames { get; private set; }

        private JsonInterface json = new NewtonJson();
        private WebConnect()
        {
            
        }

        public static WebConnect GetWebConnectInstance()
        {
            return connection;
        }

        public async Task<AppNewsRoot> getSteamNewsInfoAsync(string url)
        {
            AppNewsRoot news = null;
            try
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url);

                HttpResponseMessage response = await responseTask; 
                if (response.IsSuccessStatusCode)
                {
                    string newsData = await response.Content.ReadAsStringAsync();
                    /*when deserilazing, make sure to know what data type json has
                     * like array, obj, etc.
                     */
                    news = json.Deserialize<AppNewsRoot>(newsData);

                    //if a game found but has no news at all
                    if (news.Appnews.Count == 0)
                    {
                        Console.WriteLine("No news found for your game!\n");
                    }

                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

            return news;
        }


        public async Task GetSteamGameListAsync()
        {
            AppListRoot aList = null;
            try
            {
                Task<HttpResponseMessage> gameId = client.GetAsync("https://api.steampowered.com/ISteamApps/GetAppList/v2");

                HttpResponseMessage gameIdResponse = await gameId;

                if (gameIdResponse.IsSuccessStatusCode)
                {
                    string gameIdData = await gameIdResponse.Content.ReadAsStringAsync();

                    aList = json.Deserialize<AppListRoot>(gameIdData);

                    if (aList == null)
                    {
                        throw new NullReferenceException("Games could not be fetched!\n");
                    }
                }
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);

            }
            catch(NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
            allFetchedSteamGames = aList.listGetter.Apps;
        }

        

    }
}
