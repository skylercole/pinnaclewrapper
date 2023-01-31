﻿using System.Collections.Generic;

namespace PinnacleWrapper.Data
{
    public class GetOddsRequest
    {
        public int SportId { get; set; }
        public List<int> LeagueIds { get; set; }
        public List<int> EventIds { get; set; }
        public long Since { get; set; }
        public bool IsLive { get; set; }
        public string ApiVersion { get; set; }

        public GetOddsRequest(int sportId, string apiVersion = "v1")
        {
            SportId = sportId;
            ApiVersion = apiVersion;
        }

        public GetOddsRequest(int sportId, long since, string apiVersion = "v1")
        {
            SportId = sportId;
            Since = since;
            ApiVersion = apiVersion;
        }

        public GetOddsRequest(int sportId, List<int> leagueIds, string apiVersion = "v1")
        {
            SportId = sportId;
            LeagueIds = leagueIds;
            ApiVersion = apiVersion;
        }

        public GetOddsRequest(int sportId, List<int> leagueIds, long since, string apiVersion = "v1")
        {
            SportId = sportId;
            LeagueIds = leagueIds;
            Since = since;
            ApiVersion = apiVersion;
        }

        public GetOddsRequest(int sportId, List<int> leagueIds,List<int> eventIds, long since, string apiVersion = "v1")
        {
            SportId = sportId;
            LeagueIds = leagueIds;
            EventIds = eventIds;
            Since = since;
            //IsLive = isLive;
            ApiVersion = apiVersion;
        }
    }
}