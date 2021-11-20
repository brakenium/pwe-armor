using DaybreakGames.Census.Stream;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using watchtower.Models.Census;
using watchtower.Services;
using Websocket.Client;

namespace watchtower.Realtime {

    public class RealtimeMonitor : IDisposable, IRealtimeMonitor {

        private readonly ILogger<RealtimeMonitor> _Logger;
        private readonly ICensusStreamClient _Stream;
        private readonly IBackgroundTaskQueue _Queue;

        private List<Subscription> _Subscriptions = new List<Subscription>();

        public RealtimeMonitor(ILogger<RealtimeMonitor> logger,
            ICensusStreamClient stream,
            IBackgroundTaskQueue queue) {

            _Logger = logger;

            _Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _Queue = queue ?? throw new ArgumentNullException(nameof(queue));

            _Stream.OnConnect(_OnConnectAsync)
                .OnMessage(_OnMessageAsync)
                .OnDisconnect(_OnDisconnectAsync);

            Subscription sub = new Subscription() {
                Events = { "FacilityControl" },
                Worlds = { "all" }
            };

            //Subscribe(sub);
        }

        public void Subscribe(Subscription sub) {
            CensusStreamSubscription subscription = new CensusStreamSubscription() {
                Characters = sub.Characters,
                EventNames = sub.Events,
                Worlds = sub.Worlds
            };

            _Logger.LogInformation($"New subscription: {subscription}");

            _Stream.Subscribe(subscription);
        }

        private Task _OnMessageAsync(string msg) {
            if (msg == null) {
                return Task.CompletedTask;
            }

            try {
                JToken token = JToken.Parse(msg);
                _Queue.Queue(token);
            } catch (Exception ex) {
                _Logger.LogError(ex, "Failed to parse message: {json}", msg);
            }

            return Task.CompletedTask;
        }

        public async Task OnStartAsync(CancellationToken cancel) {
            await _Stream.ConnectAsync();
        }

        public Task OnShutdownAsync(CancellationToken cancel) {
            return _Stream.DisconnectAsync();
        }

        private Task _OnConnectAsync(ReconnectionType type) {
            if (type == ReconnectionType.Initial) {
                _Logger.LogInformation($"Stream connected");

                Subscription sub = new Subscription() {
                    Characters = { "all" },
                    Events = { "VehicleDestroy", "Death", "GainExperience", "PlayerLogin", "PlayerLogout" },
                    Worlds = { "17", "19" }
                };

                Subscribe(sub);
            } else {
                _Logger.LogInformation($"{type}, reconnecting");
                _Resubscribe();
            }

            return Task.CompletedTask;
        }

        private Task _OnDisconnectAsync(DisconnectionInfo info) {
            _Logger.LogInformation($"Stream disconnected: {info.Type}");
            return Task.CompletedTask;
        }

        public void Dispose() {
            _Stream?.Dispose();
        }

        private void _Resubscribe() {
            foreach (Subscription sub in _Subscriptions) {
                Subscribe(sub);
            }
        }

    }
}
