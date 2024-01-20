using System.Collections.Generic;
using Newtonsoft.Json;

namespace PinnacleWrapper.Data
{
    public class GetFixturesSpecialResponse
    {
        [JsonProperty(PropertyName = "sportId")]
        public int SportId;

        [JsonProperty(PropertyName = "last")] 
        public long Last;

        [JsonProperty(PropertyName = "leagues")]
        public List<FixturesSpecialLeague> Leagues;
    }
}