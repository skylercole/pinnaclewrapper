﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PinnacleWrapper.Data;
using PinnacleWrapper.Enums;
using System.Net.Http;
using System.Net.Http.Formatting;

/*
Wrapper Github: https://github.com/harveysburger/pinnaclewrapper
Pinnacle API Doc: https://www.pinnacle.com/en/api/manual#GSettledFixtures
Pinnacle Github: https://pinnacleapi.github.io/linesapi#operation/Odds_Straight_V1_Get
*/
namespace PinnacleWrapper
{
    public class PinnacleClient
    {
        private readonly HttpClient _httpClient;

        public const string DefaultBaseAddress = "https://api.pinnacle.com/";

        public string CurrencyCode { get; }

        public OddsFormat OddsFormat { get; }

        public PinnacleClient(string clientId, string password, string currencyCode, OddsFormat oddsFormat, string baseAddress = DefaultBaseAddress)
        {
            CurrencyCode = currencyCode;
            OddsFormat = oddsFormat;
            _httpClient = HttpClientFactory.GetNewInstance(clientId, password, true, baseAddress);
        }

        public PinnacleClient(string currencyCode, OddsFormat oddsFormat, HttpClient httpClient)
        {
            CurrencyCode = currencyCode;
            OddsFormat = oddsFormat;
            _httpClient = httpClient;
        }

        protected async Task<T> GetXmlAsync<T>(string requestType, params object[] values)
            where T : XmlResponse
        {
            var response = await _httpClient.GetAsync(string.Format(requestType, values)).ConfigureAwait(false);

            //var str = _httpClient.GetStringAsync(string.Format(requestType, values)).Result;

            response.EnsureSuccessStatusCode(); // throw if web request failed

            var xmlFormatter = new XmlMediaTypeFormatter {UseXmlSerializer = true};

            var apiResponse = await response.Content.ReadAsAsync<T>(new[] {xmlFormatter});

            if (apiResponse.IsValid) return apiResponse;

            throw new Exception($"Pinnacle Sports API error: {apiResponse.Error.Message}");
        }

        public async Task<List<Sport>> GetSports()
        {
            return (await GetJsonAsync<SportsResponse>("v2/sports")).Sports;
        }

        public async Task<List<League>> GetLeagues(int sportId)
        {
            return (await GetJsonAsync<LeaguesResponse>($"v2/leagues?sportId={sportId}")).Leagues;
        }

        public async Task<List<Currency>> GetCurrencies()
        {
            return (await GetJsonAsync<CurrenciesResponse>("v2/currencies")).Currencies;
        }

        protected async Task<T> GetJsonAsync<T>(string requestType, params object[] values)
        {
            var response = await _httpClient.GetAsync(string.Format(requestType, values)).ConfigureAwait(false);
           
            var json = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode(); // throw if web request failed
           
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json));
        }

        // TODO: replace requestData object type with "IJsonSerialisable"
        protected async Task<T> PostJsonAsync<T>(string requestType, object requestData)
        {
            var requestPostData = JsonConvert.SerializeObject(requestData);

            var response = await _httpClient.PostAsync(requestType,
                new StringContent(requestPostData, Encoding.UTF8, "application/json")).ConfigureAwait(false);

            response.EnsureSuccessStatusCode(); // throw if web request failed

            var json = await response.Content.ReadAsStringAsync();

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json));
        }

        public Task<ClientBalance> GetClientBalance()
        {
            const string uri = "v1/client/balance";
            return GetJsonAsync<ClientBalance>(uri);
        }

        public Task<GetBetsResponse> GetBets(BetListType type, DateTime startDate, DateTime endDate)
        {
            // get request uri
            var sb = new StringBuilder();
            sb.Append($"v1/bets?betlist={type.ToString().ToLower()}");
            sb.Append($"&fromDate={startDate:yyyy-MM-dd}");
            sb.Append($"&toDate={endDate:yyyy-MM-dd}");

            var uri = sb.ToString();

            return GetJsonAsync<GetBetsResponse>(uri);
        }

        public Task<GetBetsResponse> GetBets(List<int> betIds)
        {
            // get request uri
            var sb = new StringBuilder();

            sb.Append($"v1/bets?betids={string.Join(",", betIds)}");

            var uri = sb.ToString();

            return GetJsonAsync<GetBetsResponse>(uri);
        }

        public Task<PlaceBetResponse> PlaceBet(PlaceBetRequest placeBetRequest)
        {
            return PostJsonAsync<PlaceBetResponse>("v1/bets/place", placeBetRequest);
        }

        public Task<PlaceBetResponse> PlaceSpecialBet(PlaceSpecialBets placeBetRequest)
        {
            return PostJsonAsync<PlaceBetResponse>("v1/bets/special", placeBetRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sportId"></param>
        /// <param name="leagueId"></param>
        /// <param name="eventId"></param>
        /// <param name="periodNumber">0 for sports other than soccer, soccer: 0 = Game, 1 = 1st Half, 2 = 2nd Half</param>
        /// <param name="betType"></param>
        /// <param name="oddsFormat"></param>
        /// <param name="team"></param>
        /// <param name="side"></param>
        /// <param name="handicap"></param>
        /// <returns></returns>
        public Task<GetLineResponse> GetLine(int sportId, int leagueId, long eventId, int periodNumber, BetType betType,
            OddsFormat oddsFormat,
            TeamType? team = null, SideType? side = null, decimal? handicap = null)
        {
            if (team == null)
                if (betType == BetType.Moneyline || betType == BetType.Spread || betType == BetType.Team_Total_Points)
                    throw new Exception($"TeamType is required for {betType} Bets!");

            if (side == null)
                if (betType == BetType.Total_Points || betType == BetType.Team_Total_Points)
                    throw new Exception($"SideType is required for {betType} Bets!");

            if (handicap == null)
                if (betType == BetType.Spread || betType == BetType.Total_Points || betType == BetType.Team_Total_Points)
                    throw new Exception($"Handicap is required for {betType} Bets!");

            // get request uri
            var sb = new StringBuilder();
            sb.Append($"v1/line?sportId={sportId}");
            sb.Append($"&leagueId={leagueId}");
            sb.Append($"&eventId={eventId}");
            sb.Append($"&betType={betType.ToString().ToUpper()}");
            sb.Append($"&oddsFormat={oddsFormat.ToString().ToUpper()}");
            sb.Append($"&periodNumber={periodNumber}"); // i.e. for soccer: 0 = Game, 1 = 1st Half, 2 = 2nd Half

            if (team != null)
                sb.Append($"&team={team.ToString().ToUpper()}");

            if (side != null)
                sb.Append($"&side={side.ToString().ToUpper()}");

            if (handicap != null)
                sb.Append($"&handicap={handicap.ToString().ToUpper()}");

            var uri = sb.ToString();

            return GetJsonAsync<GetLineResponse>(uri);
        }

        public Task<GetInRunningResponse> GetInRunning()
        {
            const string uri = "v1/inrunning";
            return GetJsonAsync<GetInRunningResponse>(uri);
        }

        public Task<GetFixturesResponse> GetFixtures(GetFixturesRequest request)
        {
            var sb = new StringBuilder();

            sb.Append($"{request.ApiVersion}/fixtures?sportId={request.SportId}");

            if (request.LeagueIds != null && request.LeagueIds.Any())
                sb.Append($"&leagueIds={string.Join(",", request.LeagueIds)}");

            if (request.EventIds != null && request.EventIds.Any())
                sb.Append($"&eventIds={string.Join(",", request.EventIds)}");

            if (request.Since > 0)
                sb.Append($"&since={request.Since}");

            if (request.IsLive)
                sb.Append($"&IsLive={request.IsLive}");
            
            return GetJsonAsync<GetFixturesResponse>(sb.ToString());
        }

        public Task<GetFixturesSpecialResponse> GetFixturesSpecial(GetFixturesSpecialRequest request)
        {
            var sb = new StringBuilder();

            sb.Append($"{request.ApiVersion}/fixtures/special?sportId={request.SportId}");

            sb.Append($"&eventId={request.EventId}");

            if (request.Since > 0)
                sb.Append($"&since={request.Since}");

            if (request.IsLive)
                sb.Append($"&IsLive={request.IsLive}");

            return GetJsonAsync<GetFixturesSpecialResponse>(sb.ToString());
        }

        public Task<GetOddsResponse> GetOdds(GetOddsRequest request)
        {
            var sb = new StringBuilder();

            sb.Append($"{request.ApiVersion}/odds?sportId={request.SportId}");

            if (request.LeagueIds != null && request.LeagueIds.Any())
                sb.Append($"&leagueIds={string.Join(",", request.LeagueIds)}");

            if (request.EventIds != null && request.EventIds.Any())
                sb.Append($"&eventIds={string.Join(",", request.EventIds)}");

            if (request.Since > 0)
                sb.Append($"&since={request.Since}");

            if (request.IsLive)
                sb.Append($"&IsLive={request.IsLive}");

            sb.Append($"&oddsFormat={OddsFormat}");
            sb.Append($"&toCurrencyCode={CurrencyCode}");

            return GetJsonAsync<GetOddsResponse>(sb.ToString());
        }

        public Task<GetSpecialOddsResponse> GetSpecialOdds(GetSpecialOddsRequest request)
        {
            var sb = new StringBuilder();

            sb.Append($"{request.ApiVersion}/odds/special?sportId={request.SportId}");

            if (request.LeagueIds != null && request.LeagueIds.Any())
                sb.Append($"&leagueIds={string.Join(",", request.LeagueIds)}");

            sb.Append($"&specialId={request.SpecialId}");

            sb.Append($"&oddsFormat={OddsFormat}");

            return GetJsonAsync<GetSpecialOddsResponse>(sb.ToString());
        }
    }
}