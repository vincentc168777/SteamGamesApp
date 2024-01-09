using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class App
    {
        [JsonProperty("appid")]
        public int GameId { get; set; }

        [JsonProperty("name")]

        public string GameName { get; set; }
    }
}
