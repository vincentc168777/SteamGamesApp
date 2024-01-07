using System.Net.Http;
using System;
using ConsoleApp1;

public class Program
{
    static async Task Main(string[] args)
    {
        string url = "http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid=440&count=3&maxlength=300&format=json";

        await WebConnect.GetWebConnectInstance().getSteamInfo(url);
        

    }

    
}