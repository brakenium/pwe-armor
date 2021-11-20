using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using watchtower.Models;

namespace watchtower.Census {

    public interface IOutfitCollection {

        public Task<TrackedOutfit?> GetByTagAsync(string tag);

    }
}
