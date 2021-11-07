using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode.CrossPlatform.Models
{
    public class PrefModel
    {
        [JsonProperty("IsLightTheme")]
        public bool IsLightTheme { get; set; }

        [JsonProperty("SortBy")]
        public string SortBy { get; set; }
    }
}
