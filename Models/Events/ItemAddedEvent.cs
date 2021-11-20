using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace watchtower.Models.Events {

    public class ItemAddedEvent {

        public string SourceID { get; set; } = "";

        public string Context { get; set; } = "";

        public int ItemCount { get; set; }

        public string ItemID { get; set; } = "";

        public DateTime Timestamp { get; set; }

        public string WorldID { get; set; } = "";

        public string ZoneID { get; set; } = "";

    }
}
