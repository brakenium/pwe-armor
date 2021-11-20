using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using watchtower.Census;
using watchtower.Code.ExtensionMethods;
using watchtower.Constants;
using watchtower.Models;
using watchtower.Models.Events;
using watchtower.Services;

namespace watchtower.Realtime {

    public class EventHandler : IEventHandler {

        private readonly ILogger<EventHandler> _Logger;

        private readonly ICharacterCollection _Characters;
        private readonly IEventBroadcastService _EventBroadcast;

        private readonly List<JToken> _Recent;

        public EventHandler(ILogger<EventHandler> logger,
            ICharacterCollection charCollection, IEventBroadcastService eventBroadcast) {

            _Logger = logger;

            _Recent = new List<JToken>();

            _Characters = charCollection ?? throw new ArgumentNullException(nameof(charCollection));
            _EventBroadcast = eventBroadcast ?? throw new ArgumentNullException(nameof(eventBroadcast));
        }

        public void Process(JToken ev) {
            if (_Recent.Contains(ev)) {
                _Logger.LogInformation($"Skipping duplicate event {ev}");
                return;
            }

            _Recent.Add(ev);
            if (_Recent.Count > 10) {
                _Recent.RemoveAt(0);
            }

            string? type = ev.Value<string?>("type");

            if (type == "serviceMessage") {
                JToken? payloadToken = ev.SelectToken("payload");
                if (payloadToken == null) {
                    _Logger.LogWarning($"Missing 'payload' from {ev}");
                    return;
                }

                string? eventName = payloadToken.Value<string?>("event_name");

                if (eventName == null) {
                    _Logger.LogWarning($"Missing 'event_name' from {ev}");
                } else if (eventName == "PlayerLogin") {
                    _ProcessPlayerLogin(payloadToken);
                } else if (eventName == "PlayerLogout") {
                    _ProcessPlayerLogout(payloadToken);
                } else if (eventName == "GainExperience") {
                    _ProcessExperience(payloadToken);
                } else if (eventName == "Death") {
                    _ProcessDeath(payloadToken);
                } else if (eventName == "ItemAdded") {
                    _ProcessItemAdded(payloadToken);
                } else if (eventName == "FacilityControl") {
                    _ProcessFacilityControl(payloadToken);
                } else if (eventName == "VehicleDestroy") {
                    _ProcessVehicleDestroy(payloadToken);
                } else {
                    _Logger.LogWarning($"Untracked event_name: '{eventName}'");
                }
            } else if (type == "" || type == null) {

            } else if (type == "serviceStateChanged") {

            } else if (type == "heartbeat") {

            } else if (type == "connectionStateChanged") {

            } else {
                _Logger.LogWarning($"Unknown type: {type}");
            }
        }

        private void _ProcessPlayerLogin(JToken payload) {
            string charID = payload.Value<string?>("character_id") ?? "";
            if (charID != "" && charID != "0") {
                _Characters.Cache(charID);
            }
        }

        private void _ProcessPlayerLogout(JToken payload) {
            string charID = payload.Value<string?>("character_id") ?? "";
            if (charID != "" && charID != "0") {
                _Characters.Cache(charID);
            }
        }

        private void _ProcessDeath(JToken payload) {
            DateTime timestamp = DateTimeOffset.FromUnixTimeMilliseconds((payload.Value<long?>("timestamp") ?? 0) * 1000).UtcDateTime;

            string charID = payload.Value<string?>("character_id") ?? "0";
            string attackerID = payload.Value<string?>("attacker_character_id") ?? "0";

            string loadoutID = payload.Value<string?>("character_loadout_id") ?? "-1";
            string attackerLoadoutID = payload.Value<string?>("attacker_loadout_id") ?? "-1";

            _Characters.Cache(attackerID);
            _Characters.Cache(charID);

            KillEvent ev = new KillEvent() {
                AttackerCharacterID = attackerID,
                KilledCharacterID = charID,
                AttackerLoadoutID = attackerLoadoutID,
                KilledLoadoutID = loadoutID,
                Timestamp = timestamp,
                IsHeadshot = (payload.Value<string?>("is_headshot") ?? "0") == "1",
                WeaponID = payload.Value<string?>("attacker_weapon_id") ?? "0",
                WorldID = payload.Value<string?>("world_id") ?? "0",
                ZoneID = payload.Value<string?>("zone_id") ?? "0"
            };

            //_Logger.LogInformation($"{payload}");

            _EventBroadcast.EmitKillEvent(ev);
        }

        private void _ProcessExperience(JToken payload) {
            string? charID = payload.Value<string?>("character_id");
            if (charID == null) {
                return;
            }

            string expId = payload.Value<string?>("experience_id") ?? "-1";
            int amount = payload.Value<int?>("amount") ?? 0;

            ExpEvent ev = new ExpEvent {
                SourceID = charID,
                TargetID = payload.GetString("other_id", ""),
                Timestamp = payload.CensusTimestamp("timestamp"),
                LoadoutID = payload.GetString("loadout_id", "-1"),
                WorldID = payload.GetString("world_id", "-1"),
                ZoneID = payload.GetString("zone_id", "-1"),
                Amount = amount,
                ExpID = expId
            };

            //_Logger.LogInformation($"Processing exp: {payload}");

            _EventBroadcast.EmitExpEvent(ev);
        }

        private void _ProcessFacilityControl(JToken payload) {
            //_Logger.LogInformation($"Processing FacilityControl {payload}");

            FacilityControlEvent ev = new FacilityControlEvent() {
                FacilityID = payload.GetString("facility_id", ""),
                OldFactionID = payload.GetString("old_faction_id", ""),
                NewFactionID = payload.GetString("new_faction_id", ""),
                OutfitID = payload.GetString("outfit_id", ""),
                WorldID = payload.GetString("world_id", ""),
                ZoneID = payload.GetString("zone_id", "")
            };

            _EventBroadcast.EmitFacilityControlEvent(ev);
        }

        private void _ProcessVehicleDestroy(JToken token) {
            VehicleDestroyEvent ev = new VehicleDestroyEvent() {
                AttackerCharacterID = token.GetString("attacker_character_id", ""),
                AttackerLoadoutID = token.GetString("attacker_loadout_id", ""),
                AttackerVehicleID = token.GetString("attacker_vehicle_id", ""),
                AttackerWeaponID = token.GetString("attacker_weapon_id", ""),
                SourceCharacterID = token.GetString("character_id", ""),
                FactionID = token.GetString("faction_id", ""),
                Timestamp = token.CensusTimestamp("timestamp"),
                SourceVehicleID = token.GetString("vehicle_id", ""),
                WorldID = token.GetString("world_id", ""),
                ZoneID = token.GetString("zone_id", "")
            };

            //_Logger.LogInformation($"Processing VehicleDestroy: {token}\n{JToken.FromObject(ev)}");

            _EventBroadcast.EmitVehicleDestroyEvent(ev);
        }

        private void _ProcessItemAdded(JToken payload) {
            //_Logger.LogInformation($"Processing ItemAdded: {payload}");

            string? charID = payload.NullableString("character_id");
            if (charID == null) {
                return;
            }

            ItemAddedEvent ev = new ItemAddedEvent() {
                SourceID = payload.GetString("character_id", ""),
                Context = payload.GetString("context", "<No context>"),
                ItemCount = payload.Value<int?>("item_count") ?? 0,
                ItemID = payload.GetString("item_id", "0"),
                Timestamp = payload.CensusTimestamp("timestamp"),
                WorldID = payload.GetString("world_id", "0"),
                ZoneID = payload.GetString("zone_id", "0")
            };

            _EventBroadcast.EmitItemAddedEvent(ev);
        }

    }
}
