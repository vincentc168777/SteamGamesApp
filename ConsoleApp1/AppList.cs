using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AppList
    {
        [JsonProperty("apps")]
        public List<App> Apps { get; set; }
    }
}
