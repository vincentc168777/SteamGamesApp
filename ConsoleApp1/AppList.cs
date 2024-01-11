using Newtonsoft.Json;


namespace ConsoleApp1
{
    internal class AppList
    {
        [JsonProperty("apps")]
        public List<App> Apps { get; set; }
    }
}
