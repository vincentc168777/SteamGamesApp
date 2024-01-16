using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ProgramOptions: Regex
    {
        private static readonly ProgramOptions programOptions = new ProgramOptions();

        private Regex regex = new Regex(@"[^0-9a-zA-Z ]+");

        private ProgramOptions()
        {
  
        }

        public static ProgramOptions ProgramOptionsInstance()
        {
            return programOptions;
        }
        public async Task newsOption()
        {
            Console.WriteLine("What game do you want news for? Enter full name: ");

            string gameNewsInput = Console.ReadLine();

            while (string.IsNullOrEmpty(gameNewsInput))
            {
                Console.WriteLine("Please re-type.");
                Console.WriteLine("What game do you want news for? Enter full name: ");

                gameNewsInput = Console.ReadLine();
            }

            //remove special characters
            gameNewsInput = regex.Replace(gameNewsInput, "").ToLower();

            Console.WriteLine();
            List<App> allApps = GetAllGames();

            foreach (App app in allApps)
            {
                string appName = regex.Replace(app.GameName, "").ToLower();
                //wont call getNews if names can be found in game list
                if (gameNewsInput.Equals(appName))
                {
                    Task getNewsTask = GetGameNews(app.GameId);
                    await getNewsTask;
                    break;
                }
            }
        }

        //gives user any specific game of any games with a title that includes that words they inputted
        public void gamesOption()
        {
            Console.WriteLine("Enter full game title or a word: ");

            string gameinput = Console.ReadLine();

            while (string.IsNullOrEmpty(gameinput))
            {
                Console.WriteLine("Please re-type.");
                Console.WriteLine("Enter full game title or a word: ");

                gameinput = Console.ReadLine();
            }

            Console.WriteLine();
            List<App> result = GetGamesWithWords(gameinput);

            foreach (App app in result)
            {
                Console.WriteLine(app.GameName);
            }
        }

        private async Task GetGameNews(int InputAppId)
        {
            string url = "http://api.steampowered.com/ISteamNews/GetNewsForApp/v2/?appid=" + InputAppId + "&count=6&maxlength=1000&format=json";

            Task<AppNewsRoot> FetchInfoTask = WebConnect.GetWebConnectInstance().getSteamNewsInfoAsync(url);

            AppNewsRoot gameNewsData = await FetchInfoTask;

            foreach (Newsitem n in gameNewsData.Appnews.NewsItems)
            {
                Console.WriteLine("News Title: " + n.Title);
                Console.WriteLine("Sumamry: " + n.Contents);
                Console.WriteLine();
            }
        }


        private List<App> GetGamesWithWords(string gameName)
        {
            List<App> gamesWithName = new List<App>();

            List<App> allGames = GetAllGames();

            //remove special characters
            gameName = regex.Replace(gameName, "").ToLower();

            foreach (App app in allGames)
            {
                string appName = regex.Replace(app.GameName, "").ToLower();

                bool hasName = doesContain(appName, gameName);
                if (hasName)
                {
                    gamesWithName.Add(app);
                }
            }

            return gamesWithName;
        }

        private List<App> GetAllGames()
        {
            return WebConnect.GetWebConnectInstance().allFetchedSteamGames;
        }

        private bool doesContain(string source, string hasThis)
        {
            return source.IndexOf(hasThis, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        
    }
}
