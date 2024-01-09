using System.Net.Http;
using System;
using ConsoleApp1;
using Newtonsoft.Json;

public class Program
{
    static async Task Main(string[] args)
    {
        Task getAllGames = WebConnect.GetWebConnectInstance().GetGameID();
        await getAllGames;
        /*
            Console.WriteLine("Get News For Your Game: ");
            Console.WriteLine("Enter Game ID: ");
            int userGameId = Convert.ToInt32(Console.ReadLine());

            Task startNewsFetch = GetGameNews(userGameId);

            Console.WriteLine("Fetching....");

            await startNewsFetch;
        */
        

    }

    public static async Task GetGameNews(int InputAppId)
    {
        string url = "http://api.steampowered.com/ISteamNews/GetNewsForApp/v2/?appid=" + InputAppId + "&count=6&maxlength=500&format=json";
        Task<AppNewsGetter> FetchInfoTask = WebConnect.GetWebConnectInstance().getSteamInfoAsync(url);

        AppNewsGetter gameNewsData = await FetchInfoTask;

        foreach (Newsitem n in gameNewsData.Appnews.NewsItems)
        {
            Console.WriteLine("News Title: " + n.Title);
            Console.WriteLine("Sumamry: " + n.Contents);
            Console.WriteLine();
        }
    }

    
}