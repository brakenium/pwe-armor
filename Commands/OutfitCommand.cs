using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using watchtower.Census;
using watchtower.Models;

namespace watchtower.Commands {

    [Command]
    public class OutfitCommand {

        private readonly ILogger<OutfitCommand> _Logger;
        private readonly IOutfitCollection _OutfitCollection;

        public OutfitCommand(IServiceProvider services) {
            _Logger = services.GetRequiredService<ILogger<OutfitCommand>>();
            _OutfitCollection = services.GetRequiredService<IOutfitCollection>();
        }

        public async Task Load(string tag) {
            TrackedOutfit? outfit = await _OutfitCollection.GetByTagAsync(tag);

            if (outfit == null) {
                _Logger.LogInformation($"Failed to find outfit {tag}");
                return;
            }

            _Logger.LogInformation($"Outfit loaded by tag: {tag}\n\t[{outfit.Tag}] {outfit.Name}\n\tFaction: {outfit.FactionID}\n\tMembers: {outfit.Characters.Count}");
        }

    }
}
