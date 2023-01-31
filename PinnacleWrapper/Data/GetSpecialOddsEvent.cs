using System.Collections.Generic;
using Newtonsoft.Json;

namespace PinnacleWrapper.Data
{
    public class GetSpecialOddsEvent
    {
        [JsonProperty(PropertyName = "id")] 
        public long Id { get; set; }

        [JsonProperty(PropertyName = "maxBet")]
        public decimal MaxBet { get; set; }

        [JsonProperty(PropertyName = "contestantLines")]
        public List<Contestant> ContestantLines { get; set; }
    }
}