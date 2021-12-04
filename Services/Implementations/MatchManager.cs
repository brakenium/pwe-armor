using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using watchtower.Census;
using watchtower.Code.Constants;
using watchtower.Constants;
using watchtower.Models;
using watchtower.Models.Census;
using watchtower.Models.Events;
using watchtower.Realtime;

namespace watchtower.Services {

    public static class MatchConstants {

        public static int TEAM_VS = 0;
        public static int TEAM_NC = 1;
        public static int TEAM_TR = 2;

    }

    public class MatchManager : IMatchManager {

        private readonly ILogger<MatchManager> _Logger;

        private readonly ICharacterCollection _CharacterColleciton;
        private readonly IItemCollection _ItemCollection;
        private readonly IOutfitCollection _OutfitCollection;
        private readonly IEventBroadcastService _Events;
        private readonly IRealtimeMonitor _Realtime;
        private readonly IMatchMessageBroadcast _MatchLog;
        private readonly IAdminMessageBroadcast _AdminLog;

        private readonly Dictionary<int, TrackedPlayer> _Players = new Dictionary<int, TrackedPlayer>();

        private MatchState _State = MatchState.UNSTARTED;

        private readonly Timer _MatchTimer;
        private DateTime _LastTimerTick = DateTime.UtcNow;

        private DateTime _MatchStart = DateTime.UtcNow;
        private DateTime? _MatchEnd = null;
        private int _MatchLength = 60 * 90;

        private long _MatchTicks = 0;
        const double TICKS_PER_SECOND = 10000000D;

        private MatchSettings _Settings = new MatchSettings();

        public MatchManager(ILogger<MatchManager> logger,
                ICharacterCollection charColl, IItemCollection itemColl,
                IOutfitCollection outfitColl, IMatchMessageBroadcast messageLog,
                IEventBroadcastService events, IRealtimeMonitor realtime,
                IAdminMessageBroadcast adminLog) {

            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _CharacterColleciton = charColl ?? throw new ArgumentNullException(nameof(charColl));
            _ItemCollection = itemColl ?? throw new ArgumentNullException(nameof(itemColl));
            _OutfitCollection = outfitColl ?? throw new ArgumentNullException(nameof(outfitColl));

            _Events = events ?? throw new ArgumentNullException(nameof(events));
            _Realtime = realtime ?? throw new ArgumentNullException(nameof(realtime));
            _MatchLog = messageLog ?? throw new ArgumentNullException(nameof(messageLog));
            _AdminLog = adminLog ?? throw new ArgumentNullException(nameof(adminLog));

            _MatchTimer = new Timer(1000D);

            SetSettings(new MatchSettings());

            AddListeners();
        }

        private void AddListeners() {
            _Events.OnKillEvent += KillHandler;
            _Events.OnExpEvent += ExpHandler;
            _Events.OnVehicleDestroy += VehicleDestroyHandler;

            _MatchTimer.Elapsed += OnTimerTick;

            _AdminLog.Log("Listeners setup");
        }

        public MatchSettings GetSettings() => _Settings;

        public int GetMatchLength() => (int)Math.Round(_MatchTicks / TICKS_PER_SECOND);

        public void SetMatchLength(int len) {
            _AdminLog.Log($"Match length set to {len} seconds");
            _MatchLength = len;
        }

        public TrackedPlayer? GetTeam(int index) {
            _ = _Players.TryGetValue(index, out TrackedPlayer? p);
            return p;
        }

        public void AssignTeamFaction(int index, int factionID) {
            if (_Players.TryGetValue(index, out TrackedPlayer? team) == false) {
                _AdminLog.Log($"Team {index} does not exist, creating");
                team = new TrackedPlayer() {
                    Index = index
                };
            }

            team.FactionID = factionID.ToString();
            _Players[index] = team;
            _Events.EmitPlayerUpdateEvent(team.Index, team);

            _AdminLog.Log($"Set team {index} faction ID to {factionID}");
        }

        public void SetSettings(MatchSettings settings) {
            if (_State == MatchState.RUNNING) {
                _Logger.LogWarning($"Match is currently running, some settings may create funky behavior");
            }

            _Settings = settings;

            _Logger.LogInformation($"Match settings:"
                + $"\n\tKillGoal: {_Settings.KillGoal}"
                + $"\n\tZoneID: {_Settings.ZoneID}"
            );

            _Events.EmitMatchSettingsEvent(_Settings);
        }

        public void SetScore(int index, int score) {
            if (_Players.TryGetValue(index, out TrackedPlayer? player) == true) {
                player.Score = score;
                _Events.EmitPlayerUpdateEvent(player.Index, player);
            } else {
                _Logger.LogWarning($"Cannot set score of runner {index}, _Players does not contain");
            }
        }

        public int GetScore(int index) {
            if (_Players.TryGetValue(index, out TrackedPlayer? player) == true) {
                return player.Score;
            } else {
                _Logger.LogWarning($"Cannot get score of runner {index}, _Players does not contain");
                return -1;
            }
        }

        private void OnTimerTick(object? sender, ElapsedEventArgs args) {
            DateTime time = args.SignalTime.ToUniversalTime();

            long nowTicks = time.Ticks;
            long prevTicks = _LastTimerTick.Ticks;

            _MatchTicks += nowTicks - prevTicks;

            //_Logger.LogDebug($"Total ticks: {_MatchTicks}, seconds {Math.Round(_MatchTicks / TICKS_PER_SECOND)}");

            _Events.EmitTimerEvent((int)Math.Round(_MatchTicks / TICKS_PER_SECOND));

            _LastTimerTick = DateTime.UtcNow;
        }

        public void StartRound() {
            if (_State == MatchState.RUNNING) {
                _Logger.LogWarning($"Not starting match, already started");
                return;
            }

            if (_State == MatchState.UNSTARTED) {
                _LastTimerTick = DateTime.UtcNow;
            }

            _MatchTimer.AutoReset = true;
            _MatchTimer.Start();

            _AdminLog.Log($"Match started at {_MatchStart}");

            SetState(MatchState.RUNNING);
        }

        public void ClearMatch() {
            foreach (KeyValuePair<int, TrackedPlayer> entry in _Players) {
                SetScore(entry.Value.Index, 0);
            }

            _AdminLog.Log($"Match explicitly cleared");

            _MatchTimer.Stop();
            _MatchTicks = 0;

            SetState(MatchState.UNSTARTED);
            _Events.EmitTimerEvent(0);

            _MatchStart = DateTime.UtcNow;
            _MatchEnd = null;
        }

        public void StopRound() {
            _MatchTimer.Stop();
            _MatchEnd = DateTime.UtcNow;

            _Logger.LogInformation($"Match finished at {_MatchEnd}");
            _AdminLog.Log($"Match finished at {_MatchEnd}");

            SetState(MatchState.FINISHED);
        }

        private void SetState(MatchState state) {
            if (_State == state) {
                _Logger.LogDebug($"Not setting match state to {state}, is the current one");
                return;
            }

            _State = state;
            _Events.EmitMatchStateEvent(_State);
            _AdminLog.Log($"Match state set to {_State}");
        }

        public MatchState GetState() => _State;

        public DateTime GetMatchStart() => _MatchStart;

        public DateTime? GetMatchEnd() => _MatchEnd;

        public List<TrackedPlayer> GetPlayers() => _Players.Values.ToList();

        public int GetTimeLeft() {
            return (int)Math.Round(_MatchTicks / TICKS_PER_SECOND);
        }

        private TrackedPlayer? _GetTeamOfLoadout(string loadoutID) {
            string faction = Loadout.GetFaction(loadoutID);

            foreach (KeyValuePair<int, TrackedPlayer> entry in _Players) {
                if (entry.Value.FactionID == faction) {
                    return entry.Value;
                }
            }

            return null;
        }

        private void ExpHandler(object? sender, Ps2EventArgs<ExpEvent> args) {
            if (_State != MatchState.RUNNING) {
                return;
            }

            ExpEvent ev = args.Payload;

            if (_Settings.ZoneID != null && _Settings.ZoneID != ev.ZoneID) {
                _Logger.LogTrace($"Exp event in {ev.ZoneID}, filtered to {_Settings.ZoneID}");
                return;
            }

            TrackedPlayer? team = _GetTeamOfLoadout(ev.LoadoutID);
            if (team == null) {
                return;
            }

            int points = 0;
            string size = "";

            if (ev.ExpID == "616") { // Small
                points = 2;
                size = "small";
            } else if (ev.ExpID == "604") { // Medium
                points = 4;
                size = "medium";
            } else if (ev.ExpID == "628") { // Large
                points = 6;
                size = "large";
            } else if (ev.ExpID == "57") { // Vehicle ammo thingy
                points = 0;
                size = "vehicle ammo/mana AI";
            }

            if (points == 0) {
                return;
            }

            _MatchLog.Log($"Team {team.Index + 1}:BUILDING>> A {size} building was destroyed, granting {points} points");
            SetScore(team.Index, GetScore(team.Index) + points);
        }

        private void VehicleDestroyHandler(object? sender, Ps2EventArgs<VehicleDestroyEvent> args) {
            if (_State != MatchState.RUNNING) {
                return;
            }

            TrackedPlayer? team = _GetTeamOfLoadout(args.Payload.AttackerLoadoutID);
            if (team == null) {
                return;
            }

            if (args.Payload.FactionID == team.FactionID) {
                return;
            }

            PsVehicle? killed = PsVehicle.Get(args.Payload.SourceVehicleID);
            PsVehicle? attacker = PsVehicle.Get(args.Payload.AttackerVehicleID);

            if (killed == null || attacker == null) {
                _AdminLog.Log($"SYSTEM:NOTICE>> Extra info in console");
                _Logger.LogInformation($"{JToken.FromObject(args.Payload)}");
            } else {
                int points = killed.DeathScore * attacker.KillMultiplier;
                _MatchLog.Log($"Team {team.Index + 1}:VEHICLE>> A {killed.Name} was killed by {attacker.Name}. A {killed.Name} is worth {killed.DeathScore} points, {attacker.Name} gives {attacker.KillMultiplier}x points, for a total of {points}");
                SetScore(team.Index, GetScore(team.Index) + points);
            }
        }

        private void KillHandler(object? sender, Ps2EventArgs<KillEvent> args) {
            if (_State != MatchState.RUNNING) {
                return;
            }

            KillEvent ev = args.Payload;

            if (ev.KilledCharacterID == "0") {
                return;
            }

            TrackedPlayer? team = _GetTeamOfLoadout(ev.KilledLoadoutID);

            if (team == null) {
                return;
            }

            if (team.Index == 0) {
                SetScore(1, GetScore(1) + 1);
            } else if (team.Index == 1) {
                SetScore(0, GetScore(0) + 1);
            } else {
                throw new ArgumentException($"Expected Index 0|1, got {team.Index}");
            }

            _MatchLog.Log($"Team {team.Index + 1}:DEATH>> {ev.KilledCharacterID} was killed by {ev.AttackerCharacterID}. This gives the other team 1 point");
        }


    }
}
