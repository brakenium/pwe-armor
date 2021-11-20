using DaybreakGames.Census;
using DaybreakGames.Census.Exceptions;
using DaybreakGames.Census.Operators;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using watchtower.Models;

namespace watchtower.Census {

    public class OutfitCollection : IOutfitCollection {

        private readonly ILogger<OutfitCollection> _Logger;

        private readonly ICensusQueryFactory _Census;

        public OutfitCollection(ILogger<OutfitCollection> logger,
                ICensusQueryFactory census) {

            _Logger = logger;
            _Census = census;
        }

        public Task<TrackedOutfit?> GetByTagAsync(string tag) {
            return _GetFromCensus(tag, true);
        }

        public async Task<TrackedOutfit?> _GetFromCensus(string tag, bool retry = true) {
            CensusQuery query = _Census.Create("outfit");

            query.Where("alias_lower").Equals(tag.ToLower());
            query.AddResolve("leader", "member");

            try {
                JToken result = await query.GetAsync();

                TrackedOutfit? outfit = _Parse(result);

                return outfit;
            } catch (CensusConnectionException ex) {
                if (retry == true) {
                    _Logger.LogWarning("Retrying {Tag} from API", tag);
                    return await _GetFromCensus(tag, false); 
                } else {
                    _Logger.LogError(ex, "Failed to get {0} from API", tag);
                    throw ex;
                }
            }
        }

        private TrackedOutfit? _Parse(JToken result) {
            if (result == null) {
                return null;
            }

            TrackedOutfit outfit = new TrackedOutfit();

            outfit.ID = result.Value<string?>("outfit_id") ?? "0";
            outfit.Name = result.Value<string?>("name") ?? "<Missing name>";
            outfit.Tag = result.Value<string?>("alias") ?? "";

            JToken? leader = result.SelectToken("leader");
            if (leader != null) {
                outfit.FactionID = leader.Value<string?>("faction_id") ?? "-1";
            } else {
                _Logger.LogError($"Missing 'leader' token from {result}");
            }

            JToken? members = result.SelectToken("members");
            if (members != null) {
                JArray? arr = (JArray?)members;
                if (arr != null) {
                    List<OutfitMember> list = new List<OutfitMember>(result.Value<int?>("member_count") ?? 0);
                    foreach (JToken? iter in arr) {
                        if (iter == null) {
                            continue;
                        }

                        list.Add(_ParseCharacter(iter));
                    }

                    outfit.Characters = list;
                } else {
                    _Logger.LogError($"Could not read members, 'members' was not an array");
                }
            } else {
                _Logger.LogError($"Missing 'members' token from {result}");
            }

            return outfit;
        }

        private OutfitMember _ParseCharacter(JToken member) {
            return new OutfitMember() {
                ID = member.Value<string?>("character_id") ?? "0",
                Name = member.Value<string?>("name") ?? ""
            };
        }

    }
}
