using System;
using Newtonsoft.Json;
using PinnacleWrapper.Enums;

namespace PinnacleWrapper.Data
{
    public class PlaceSpecialBetRequest
    {
        [JsonProperty(PropertyName = "uniqueRequestId")]
        public Guid UniqueRequestId;

        [JsonProperty(PropertyName = "acceptBetterLine")]
        public bool AcceptBetterLine;

        [JsonProperty(PropertyName = "oddsFormat")]
        public OddsFormat OddsFormat;

        [JsonProperty(PropertyName = "stake")] 
        public decimal Stake;

        [JsonProperty(PropertyName = "winRiskStake")]
        public WinRiskType WinRiskType;

        [JsonProperty(PropertyName = "lineId")]
        public long LineId;

        [JsonProperty(PropertyName = "specialId")]
        public long SpecialId;

        [JsonProperty(PropertyName = "contestantId")]
        public long ContestantId;
    }
}