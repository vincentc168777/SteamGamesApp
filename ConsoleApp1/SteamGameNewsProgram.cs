using System;
using ConsoleApp1;
using System.Formats.Asn1;
using System.Reflection.Metadata.Ecma335;

public class SteamGameNewsProgram
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("App startup game fetch start.\n");
        Task startUpGameFetch = WebConnect.GetWebConnectInstance().GetSteamGameListAsync();
        Console.WriteLine("App startup game fetch running. Please wait...");
        
        await startUpGameFetch;

        Console.WriteLine("Fetch completed!\n");

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
                Console.WriteLine("Please re-type name.\n");
                Console.WriteLine("Enter Game Name: ");

                userGame = Console.ReadLine();
            }

            Console.WriteLine("Input accepted, starting search...\n");

            List<App> resultGameList = GetGamesWithName(userGame);

            Console.WriteLine("Finished fetch! Here are your games:");

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

    private List<App> GetGames()
    {
        return WebConnect.GetWebConnectInstance().allFetchedSteamGames;
    }

    private static List<App> GetGamesWithName(string gameName)
    {
        List<App> gamesWithName = new List<App>();
        

        foreach (App app in WebConnect.GetWebConnectInstance().allFetchedSteamGames)
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