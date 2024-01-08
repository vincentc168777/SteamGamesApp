using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Appnews
    {
        [JsonProperty("appid")]
        public int Appid { get; set; }

        [JsonProperty("newsitems")]
        public List<Newsitem> NewsItems { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
