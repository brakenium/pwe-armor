using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace watchtower.Models {

    public class OutfitMember {

        public string ID { get; set; } = "";
        public string Name { get; set; } = "";

    }

    public class TrackedOutfit {

        public string ID { get; set; } = "";

        public string? Tag { get; set; }

        public string Name { get; set; } = "";

        public string FactionID { get; set; } = "";

        public List<OutfitMember> Characters { get; set; } = new List<OutfitMember>();

    }
}
