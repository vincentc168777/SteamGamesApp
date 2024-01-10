using System.Net.Http;
using System;
using ConsoleApp1;
using Newtonsoft.Json;
using System.Formats.Asn1;

public class Program
{
    static async Task Main(string[] args)
    {
        Task getAllGames = GetGames();
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
        Task<AppNewsGetter> FetchInfoTask = WebConnect.GetWebConnectInstance().getSteamNewsInfoAsync(url);

        AppNewsGetter gameNewsData = await FetchInfoTask;

        foreach (Newsitem n in gameNewsData.Appnews.NewsItems)
        {
            Console.WriteLine("News Title: " + n.Title);
            Console.WriteLine("Sumamry: " + n.Contents);
            Console.WriteLine();
        }
    }

    public static async Task GetGames()
    {
        string url = "https://api.steampowered.com/ISteamApps/GetAppList/v2";

        Task<AppListRoot> getGameTask = WebConnect.GetWebConnectInstance().GetSteamGameListAsync(url);

        AppListRoot appList = await getGameTask;

        // for future development, add where it will check completely upper or lower cases, as the titles are case sensitive

        foreach (App app in appList.listGetter.Apps)
        {
            if (app.GameName.Contains("ELDEN RING"))
            {
                Console.WriteLine(app.GameName);
            }

        }

    }

    
}