using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace watchtower.Models.Events {

    public class FacilityControlEvent {

        public string FacilityID { get; set; } = "0";

        public string NewFactionID { get; set; } = "0";

        public string OldFactionID { get; set; } = "0";

        public string OutfitID { get; set; } = "";

        public DateTime Timestamp { get; set; }

        public string WorldID { get; set; } = "";

        public string ZoneID { get; set; } = "";

    }
}
