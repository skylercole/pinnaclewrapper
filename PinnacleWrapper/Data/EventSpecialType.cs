using Newtonsoft.Json;

namespace PinnacleWrapper.Data
{
    public class EventSpecialType
    {
        [JsonProperty(PropertyName = "id")]
        public int Id;

        [JsonProperty(PropertyName = "periodNumber")]
        public int PeriodNumber;
    }
}
