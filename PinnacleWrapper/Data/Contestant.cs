using Newtonsoft.Json;

namespace PinnacleWrapper.Data
{
    public class Contestant
    {
        [JsonProperty(PropertyName = "id")] 
        public long Id { get; set; }

        [JsonProperty(PropertyName = "lineId")]
        public long LineId { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "handicap")]
        public string Handicap{ get; set; }

        [JsonProperty(PropertyName = "max")]
        public decimal Max { get; set; }
    }
}