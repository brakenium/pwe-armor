using System;

namespace watchtower.Models.Events {

    public class KillEvent {

        public string AttackerCharacterID { get; set; } = "";

        public DateTime Timestamp { get; set; }

        public string ZoneID { get; set; } = "";

        public string AttackerLoadoutID { get; set; } = "";

        public string KilledCharacterID { get; set; } = "";

        public string KilledLoadoutID { get; set; } = "";

        public string WeaponID { get; set; } = "";

        public bool IsHeadshot { get; set; }

        public string WorldID { get; set; } = "";

    }

}
