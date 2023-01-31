using System.Collections.Generic;
using Newtonsoft.Json;

namespace PinnacleWrapper.Data
{
    public class GetSpecialOddsLeague
    {
        [JsonProperty(PropertyName = "id")] 
        public int Id { get; set; }

        [JsonProperty(PropertyName = "specials")]
        public List<GetSpecialOddsEvent> Specials { get; set; }
    }
}