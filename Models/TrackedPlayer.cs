using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using watchtower.Models.Events;

namespace watchtower.Models {

    public class TrackedPlayer {

        public int Index { get; set; } = 0;

        public string RunnerName { get; set; } = "";

        public int Score { get; set; } = 0;

        public string FactionID { get; set; } = "";

        public List<Character> Characters { get; set; } = new List<Character>();

        public List<TrackedOutfit> Outfits { get; set; } = new List<TrackedOutfit>();

        public List<KillEvent> Kills { get; set; } = new List<KillEvent>();

        public List<KillEvent> ValidKills { get; set; } = new List<KillEvent>();

        public List<KillEvent> Deaths { get; set; } = new List<KillEvent>();

        public List<VehicleDestroyEvent> VehicleDestroys { get; set; } = new List<VehicleDestroyEvent>();

        public List<ExpEvent> Exp { get; set; } = new List<ExpEvent>();

        public List<ItemAddedEvent> ItemAddeds { get; set; } = new List<ItemAddedEvent>();

    }
}
