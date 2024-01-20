using Newtonsoft.Json;

namespace PinnacleWrapper.Data
{
    public class ContestantSpecial
    {
        [JsonProperty(PropertyName = "id")]
        public int Id;

        [JsonProperty(PropertyName = "name")]
        public string Name;

        [JsonProperty(PropertyName = "rotNum")]
        public int RotNum;
    }
}
