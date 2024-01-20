using System.Collections.Generic;

namespace PinnacleWrapper.Data
{
    public class GetFixturesSpecialRequest
    {
        public int SportId { get; set; }
        public List<int> LeagueIds { get; set; }
        public int EventId { get; set; }
        public long Since { get; set; }
        public bool IsLive { get; set; }
        public string ApiVersion { get; set; }

        public GetFixturesSpecialRequest(int sportId, List<int> leagueIds, int eventId, long since, string apiVersion = "v1")
        {
            SportId = sportId;
            LeagueIds = leagueIds;
            EventId = eventId;
            Since = since;
            ApiVersion = apiVersion;
        }
    }
}