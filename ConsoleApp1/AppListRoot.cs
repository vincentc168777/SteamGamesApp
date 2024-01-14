using Newtonsoft.Json;


namespace ConsoleApp1
{
    internal class AppListRoot
    {
        //this class has a getter and setter for the AppListClass
        [JsonProperty("applist")]
        public AppList listGetter { get; set; }
    }
}
