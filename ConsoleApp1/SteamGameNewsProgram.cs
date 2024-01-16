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

        Console.WriteLine("Welcome to steam game app!");
        while (keepRunning)
        {
            Console.WriteLine();
            Console.WriteLine("Do you want news for a game or just a game list? Type 'news' or 'games': ");

            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input) || (!input.ToLower().Equals("games") && !input.ToLower().Equals("news")))
            {
                Console.WriteLine("Please re-type request.\n");
                Console.WriteLine("Type 'news' or 'games': ");

                input = Console.ReadLine();
            }

            Console.WriteLine("Input accepted!\n");

            Task gameOrNewsTask = gamesOrNews(input);
            await gameOrNewsTask;


            keepRunning = continueRunning();
        }
        

    }

    private static async Task gamesOrNews(string userIn)
    {
        if (userIn.ToLower().Equals("games"))
        {
            ProgramOptions.ProgramOptionsInstance().gamesOption();

        }
        else if (userIn.ToLower().Equals("news"))
        {
            Task newsTask = ProgramOptions.ProgramOptionsInstance().newsOption();
            await newsTask;
        }
    }

    private static bool continueRunning()
    {
        bool keepRunning = false;
        Console.WriteLine("");
        Console.WriteLine("Do you want to search more games? Type yes or no: ");

        string yN = Console.ReadLine();

        while (string.IsNullOrEmpty(yN)|| ( !yN.ToLower().Equals("yes") && !yN.ToLower().Equals("no") ) )
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