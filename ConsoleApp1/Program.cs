using System.Net.Http;
using System;
using ConsoleApp1;
using Newtonsoft.Json;

public class Program
{
    static async Task Main(string[] args)
    {
        string url = "http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid=440&count=3&maxlength=300&format=json";

        //waits for this to finish
        Task<AppNewsGetter> FetchInfoTask = WebConnect.GetWebConnectInstance().getSteamInfoAsync(url);

        AppNewsGetter gameData = await FetchInfoTask;
        foreach(Newsitem n in gameData.Appnews.NewsItems)
        {
            Console.WriteLine(n.Title);
            Console.WriteLine(n.Contents);
        }
        







    }

    
}