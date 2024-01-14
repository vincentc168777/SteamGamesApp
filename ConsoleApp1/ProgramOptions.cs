using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ProgramOptions
    {
        private static readonly ProgramOptions programOptions = new ProgramOptions();
        private ProgramOptions() { }

        public static ProgramOptions ProgramOptionsInstance()
        {
            return programOptions;
        }
        public async Task newsOption()
        {
            Console.WriteLine("What game do you want news for?: ");

            string gameNewsinput = Console.ReadLine();

            while (string.IsNullOrEmpty(gameNewsinput))
            {
                Console.WriteLine("Please re-type.");
                Console.WriteLine("What game do you want news for?: ");

                gameNewsinput = Console.ReadLine();
            }

            Console.WriteLine();
            List<App> allApps = GetAllGames();

            foreach (App app in allApps)
            {
                if (gameNewsinput.ToLower().Equals(app.GameName.ToLower()))
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


        private List<App> GetGamesWithWords(string gameName)
        {
            List<App> gamesWithName = new List<App>();

            List<App> allGames = GetAllGames();

            foreach (App app in allGames)
            {
                bool hasName = doesContain(app.GameName, gameName);
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
