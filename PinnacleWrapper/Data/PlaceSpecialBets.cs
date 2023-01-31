using System.Collections.Generic;
using Newtonsoft.Json;

namespace PinnacleWrapper.Data
{
    public class PlaceSpecialBets
    {
        [JsonProperty(PropertyName = "bets")]
        public List<PlaceSpecialBetRequest> Bets;
    }
}