﻿namespace TwitchLib
{
    using System.Collections.Generic;
    #region using directives
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion

    public class Games
    {
        public Games(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
            helix = new Helix(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }
        public Helix helix { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetTopGames
            public async Task<Models.API.v3.Games.TopGamesResponse> GetTopGamesAsync(int limit = 10, int offset = 0)
            {
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("limit", limit.ToString()), new KeyValuePair<string, string>("offset", offset.ToString()) };
                return await Api.GetGenericAsync<Models.API.v3.Games.TopGamesResponse>($"https://api.twitch.tv/kraken/games/top", getParams, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetTopGames
            public async Task<Models.API.v5.Games.TopGames> GetTopGamesAsync(int? limit = null, int? offset = null)
            {
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset != null)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                
                return await Api.GetGenericAsync<Models.API.v5.Games.TopGames>($"https://api.twitch.tv/kraken/games/top", getParams, null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
        }

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }
            #region GetGames
            public async Task<Models.API.Helix.Games.GetGames.GetGamesResponse> GetGames(List<string> gameIds = null, List<string> gameNames = null)
            {
                if ((gameIds == null && gameNames == null) ||
                    (gameIds != null && gameIds.Count == 0 && gameNames == null) ||
                    (gameNames != null && gameNames.Count == 0 && gameIds == null))
                    throw new Exceptions.API.BadParameterException("Either gameIds or gameNames must have at least one value");
                if (gameIds != null && gameIds.Count > 100)
                    throw new Exceptions.API.BadParameterException("gameIds list cannot exceed 100 items");
                if (gameNames != null && gameNames.Count > 100)
                    throw new Exceptions.API.BadParameterException("gameNames list cannot exceed 100 items");

                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                if (gameIds != null && gameIds.Count > 0)
                    foreach (var gameId in gameIds)
                        getParams.Add(new KeyValuePair<string, string>("id", gameId));
                if (gameNames != null && gameNames.Count > 0)
                    foreach (var gameName in gameNames)
                        getParams.Add(new KeyValuePair<string, string>("name", gameName));
                
                return await Api.GetGenericAsync<Models.API.Helix.Games.GetGames.GetGamesResponse>($"https://api.twitch.tv/helix/games", getParams, null, ApiVersion.Helix).ConfigureAwait(false);
            }
            #endregion
        }

    }
}
