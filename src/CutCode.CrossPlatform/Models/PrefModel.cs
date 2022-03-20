using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CutCode.CrossPlatform.Services;

namespace CutCode.CrossPlatform.Models
{
    public class PrefModel
    {
        [JsonProperty("ThemeType")]
        public ThemeType Theme { get; set; }

        [JsonProperty("SortBy")]
        public string SortBy { get; set; }
    }
}
