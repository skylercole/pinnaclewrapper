using System.Collections.Generic;
using Newtonsoft.Json;

namespace PinnacleWrapper.Data
{
    public class GetSpecialOddsResponse
    {
        [JsonProperty(PropertyName = "sportId")]
        public int SportId { get; set; }

        [JsonProperty(PropertyName = "last")] 
        public long Last { get; set; }

        [JsonProperty(PropertyName = "leagues")]
        public List<GetSpecialOddsLeague> Leagues { get; set; }
    }
}