using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace watchtower.Models.Events {

    public class VehicleDestroyEvent {

        public string AttackerCharacterID { get; set; } = "";

        public string AttackerLoadoutID { get; set; } = "";

        public string AttackerVehicleID { get; set; } = "";

        public string AttackerWeaponID { get; set; } = "";

        public string SourceCharacterID { get; set; } = "";

        public string SourceVehicleID { get; set; } = "";

        public string WorldID { get; set; } = "";

        public string ZoneID { get; set; } = "";

        public string FactionID { get; set; } = "";

        public DateTime Timestamp { get; set; }

    }
}
