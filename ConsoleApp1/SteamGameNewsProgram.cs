using System.Net.Http;
using System;
using ConsoleApp1;
using Newtonsoft.Json;
using System.Formats.Asn1;
using System.Reflection.Metadata.Ecma335;

public class SteamGameNewsProgram
{
    public static async Task Main(string[] args)
    {

        await askUser();

    }

    public static async Task askUser()
    {
        bool keepRunning = true;

        Console.WriteLine("Get news for your favorite game!");
        while (keepRunning)
        {
            Console.WriteLine("Enter game name: ");

            string userGame = Console.ReadLine();

            while (string.IsNullOrEmpty(userGame))
            {
                Console.WriteLine("Please re-type name.");
                Console.WriteLine("Enter Game Name: ");

                userGame = Console.ReadLine();
            }

            Console.WriteLine("Input accepted, starting fetch...");
            Task<List<App>> startFetchTask = GetGamesWithName(userGame);

            List<App> resultGameList = await startFetchTask;

            Console.WriteLine("Finished fetch! Here are your games:");
            Console.WriteLine();

            foreach (App app in resultGameList)
            {
                Console.WriteLine(app.GameName);
            }

            keepRunning = continueRunning();
        }
        

    }

    private static async Task GetGameNews(int InputAppId)
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

    private static async Task<List<App>> GetGames()
    {
        string url = "https://api.steampowered.com/ISteamApps/GetAppList/v2";

        Task<AppListRoot> getGameTask = WebConnect.GetWebConnectInstance().GetSteamGameListAsync(url);

        AppListRoot appListRoot = await getGameTask;

        return appListRoot.listGetter.Apps;

    }

    private static async Task<List<App>> GetGamesWithName(string gameName)
    {
        List<App> gamesWithName = new List<App>();
        //gets all games first
        Task<List<App>> getAllGames = GetGames();

        Console.WriteLine("Fetching steam games...");

        List<App> gamelist = await getAllGames;

        foreach (App app in gamelist)
        {
            bool hasName = doesContain(app.GameName, gameName);
            if (hasName)
            {
                gamesWithName.Add(app);
            }
        }

        return gamesWithName;
    }

    private static bool doesContain(string source, string hasThis)
    {
        return source.IndexOf(hasThis, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static bool continueRunning()
    {
        bool keepRunning = false;
        Console.WriteLine("");
        Console.WriteLine("Do you want to search more games? Type yes or no: ");

        string yN = Console.ReadLine();

        while (string.IsNullOrEmpty(yN))
        {
            Console.WriteLine("Do you want to search more games? Type yes or no: ");
            yN = Console.ReadLine();
        }

        if (yN.ToLower().Equals("yes"))
        {
            keepRunning = true;
        }
        else
        {
            keepRunning = false;
        }
 
        return keepRunning;
    }
    
    

    
}