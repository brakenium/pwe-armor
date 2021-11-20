using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using watchtower.Constants;
using watchtower.Models;
using watchtower.Models.Events;

namespace watchtower.Services {

    public class EventBroadcastService : IEventBroadcastService {

        private ILogger<EventBroadcastService> _Logger;

        public EventBroadcastService(ILogger<EventBroadcastService> logger) {
            _Logger = logger;
        }

        public event EventHandler<Ps2EventArgs<string>>? OnTestEvent;
        public delegate void TestEventHandler(object sender, Ps2EventArgs<string> args);

        public event EventHandler<Ps2EventArgs<KillEvent>>? OnKillEvent;
        public delegate void KillEventHandler(object sender, Ps2EventArgs<KillEvent> args);

        public event EventHandler<Ps2EventArgs<ExpEvent>>? OnExpEvent;
        public delegate void ExpEventHandler(object sender, Ps2EventArgs<ExpEvent> args);

        public event EventHandler<Ps2EventArgs<FacilityControlEvent>>? OnFacilityControlEvent;
        public delegate void FacilityControlEventHandler(object sender, Ps2EventArgs<FacilityControlEvent> args);

        public event EventHandler<Ps2EventArgs<ItemAddedEvent>>? OnItemAddedEvent;
        public delegate void ItemAddedEventHandler(object sender, Ps2EventArgs<ItemAddedEvent> args);

        public event EventHandler<Ps2EventArgs<TrackedPlayer?>>? OnPlayerUpdateEvent;
        public delegate void PlayerUpdateEventHandler(object sender, Ps2EventArgs<TrackedPlayer?> args);

        public event EventHandler<Ps2EventArgs<int>>? OnTimerEvent;
        public delegate void TimerEventHandler(object sender, Ps2EventArgs<int> args);

        public event EventHandler<Ps2EventArgs<BastionTickArgs>>? OnBastionTimerEvent;
        public delegate void BastionTimerEventHandler(object sender, Ps2EventArgs<BastionTickArgs> args);

        public event EventHandler<Ps2EventArgs<MatchState>>? OnMatchStateEvent;
        public delegate void MatchStateEvent(object sender, Ps2EventArgs<MatchState> args);

        public event EventHandler<Ps2EventArgs<MatchSettings>>? OnMatchSettingsEvent;
        public delegate void MatchSettingsEvent(object sender, Ps2EventArgs<MatchSettings> args);

        public event EventHandler<Ps2EventArgs<VehicleDestroyEvent>>? OnVehicleDestroy;
        public delegate void VehicleDestroyEventHandler(object sender, Ps2EventArgs<VehicleDestroyEvent> args);
        
        public void EmitKillEvent(KillEvent ev) {
            OnKillEvent?.Invoke(this, new Ps2EventArgs<KillEvent>(ev));
        }

        public void EmitExpEvent(ExpEvent ev) {
            OnExpEvent?.Invoke(this, new Ps2EventArgs<ExpEvent>(ev));
        }

        public void EmitVehicleDestroyEvent(VehicleDestroyEvent ev) {
            OnVehicleDestroy?.Invoke(this, new Ps2EventArgs<VehicleDestroyEvent>(ev));
        }

        public void EmitFacilityControlEvent(FacilityControlEvent ev) {
            OnFacilityControlEvent?.Invoke(this, new Ps2EventArgs<FacilityControlEvent>(ev));
        }

        public void EmitItemAddedEvent(ItemAddedEvent ev) {
            OnItemAddedEvent?.Invoke(this, new Ps2EventArgs<ItemAddedEvent>(ev));
        }

        public void EmitTestEvent(string msg) {
            OnTestEvent?.Invoke(this, new Ps2EventArgs<string>(msg));
        }

        public void EmitPlayerUpdateEvent(int index, TrackedPlayer? player) {
            OnPlayerUpdateEvent?.Invoke(this, new Ps2EventArgs<TrackedPlayer?>(player));
        }

        public void EmitTimerEvent(int time) {
            OnTimerEvent?.Invoke(this, new Ps2EventArgs<int>(time));
        }

        public void EmitBastionTimerEvent(int index, int lifespan) {
            OnBastionTimerEvent?.Invoke(this, new Ps2EventArgs<BastionTickArgs>(new BastionTickArgs() {
                Index = index,
                Lifespan = lifespan
            }));
        }

        public void EmitMatchStateEvent(MatchState state) {
            OnMatchStateEvent?.Invoke(this, new Ps2EventArgs<MatchState>(state));
        }

        public void EmitMatchSettingsEvent(MatchSettings settings) {
            OnMatchSettingsEvent?.Invoke(this, new Ps2EventArgs<MatchSettings>(settings));
        }

    }
}
