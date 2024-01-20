using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinnacleWrapper.Data
{
    public class Special
    {
        [JsonProperty(PropertyName = "id")]
        public int Id;

        [JsonProperty(PropertyName = "betType")]
        public string BetType;

        [JsonProperty(PropertyName = "name")]
        public string Name;

        [JsonProperty(PropertyName = "date")]
        public string Date;

        [JsonProperty(PropertyName = "cutoff")]
        public string Cutoff;

        [JsonProperty(PropertyName = "category")]
        public string Category;

        [JsonProperty(PropertyName = "units")]
        public string Units;

        [JsonProperty(PropertyName = "status")]
        public string Status;

        [JsonProperty(PropertyName = "event")]
        public EventSpecialType Event;

        [JsonProperty(PropertyName = "contestants")]
        public List<ContestantSpecial> Contestants;
    }
}
