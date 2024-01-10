using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AppListRoot
    {
        //this class has a getter and setter for the AppListClass
        [JsonProperty("applist")]
        public AppList listGetter { get; set; }
    }
}
