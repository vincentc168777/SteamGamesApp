using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Newsitem
    {
        [JsonProperty("gid")]
        public string Gid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("is_external_url")]
        public bool IsExternalUrl { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("contents")]
        public string Contents { get; set; }

        [JsonProperty("feedlabel")]
        public string Feedlabel { get; set; }

        [JsonProperty("date")]
        public int Date { get; set; }

        [JsonProperty("feedname")]
        public string Feedname { get; set; }

        [JsonProperty("feed_type")]
        public int FeedType { get; set; }

        [JsonProperty("appid")]
        public int Appid { get; set; }
    }
}
