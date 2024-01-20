using System.Collections.Generic;

namespace PinnacleWrapper.Data
{
    public class GetSpecialOddsRequest
    {
        public int SportId { get; set; }
        public List<int> LeagueIds { get; set; }
        public int SpecialId { get; set; }
        public long Since { get; set; }
        public bool IsLive { get; set; }
        public string ApiVersion { get; set; }

        public GetSpecialOddsRequest(int sportId, string apiVersion = "v1")
        {
            SportId = sportId;
            ApiVersion = apiVersion;
        }

        public GetSpecialOddsRequest(int sportId, long since, string apiVersion = "v1")
        {
            SportId = sportId;
            Since = since;
            ApiVersion = apiVersion;
        }

        public GetSpecialOddsRequest(int sportId, List<int> leagueIds, string apiVersion = "v1")
        {
            SportId = sportId;
            LeagueIds = leagueIds;
            ApiVersion = apiVersion;
        }

        public GetSpecialOddsRequest(int sportId, List<int> leagueIds, long since, string apiVersion = "v1")
        {
            SportId = sportId;
            LeagueIds = leagueIds;
            Since = since;
            ApiVersion = apiVersion;
        }

        public GetSpecialOddsRequest(int sportId, List<int> leagueIds, int specialId, long since, string apiVersion = "v1")
        {
            SportId = sportId;
            LeagueIds = leagueIds;
            SpecialId = specialId;
            Since = since;
            ApiVersion = apiVersion;
        }
    }
}