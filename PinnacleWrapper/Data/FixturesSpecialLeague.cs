using System.Collections.Generic;
using Newtonsoft.Json;

namespace PinnacleWrapper.Data
{
    public class FixturesSpecialLeague
    {
        [JsonProperty(PropertyName = "id")] 
        public int Id;

        [JsonProperty(PropertyName = "specials")]
        public List<Special> Specials;
    }
}