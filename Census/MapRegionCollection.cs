using DaybreakGames.Census;
using DaybreakGames.Census.Exceptions;
using DaybreakGames.Census.Operators;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using watchtower.Code.ExtensionMethods;
using watchtower.Models.Census;

namespace watchtower.Census {

    public class MapRegionCollection : IMapRegionCollection {

        private readonly ILogger<MapRegionCollection> _Logger;

        private readonly ICensusQueryFactory _Census;

        private readonly ConcurrentDictionary<string, PsFacility?> _Cache = new ConcurrentDictionary<string, PsFacility?>();

        public MapRegionCollection(ILogger<MapRegionCollection> logger,
            ICensusQueryFactory query) {

            _Logger = logger;
            _Census = query;
        }

        public async Task<List<PsFacility>> GetAll() {
            lock (_Cache) {
                if (_Cache.Count > 0) {
                    // Force is safe
                    return _Cache.Values.Where(iter => iter != null).ToList()!;
                }
            }

            List<PsFacility> facs = await _GetAllFromCensus(true);

            lock (_Cache) {
                foreach(PsFacility fac in facs) {
                    _Cache.TryAdd(fac.FacilityID, fac);
                }
            }

            return facs;
        }

        private async Task<List<PsFacility>> _GetAllFromCensus(bool retry) {
            CensusQuery query = _Census.Create("map_region");
            query.SetLimit(10000);

            try {
                List<JToken> result = (await query.GetListAsync()).ToList();

                List<PsFacility> facilities = new List<PsFacility>(result.Count);

                foreach (JToken token in result) {
                    PsFacility? fac = _Parse(token);
                    if (fac != null) {
                        facilities.Add(fac);
                    }
                }

                return facilities;
            } catch (CensusConnectionException ex) {
                if (retry == true) {
                    _Logger.LogWarning("Retrying map_region.all from API");
                    return await _GetAllFromCensus(false); 
                } else {
                    _Logger.LogError(ex, "Failed to get map_region.all from API");
                    throw ex;
                }
            }
        }

        public async Task<PsFacility> GetByFacilityID(string facilityID) {
            return (await GetAll()).FirstOrDefault(iter => iter.FacilityID == facilityID);
        }

        public async Task<PsFacility> GetByRegionID(string regionID) {
            return (await GetAll()).FirstOrDefault(iter => iter.RegionID == regionID);
        }

        public async Task<List<PsFacility>> GetByZoneID(string zoneID) {
            return (await GetAll()).Where(iter => iter.ZoneID == zoneID).ToList();
        }

        private PsFacility? _Parse(JToken result) {
            if (result == null) {
                return null;
            }

            PsFacility fac = new PsFacility() {
                RegionID = result.GetString("map_region_id", ""),
                FacilityID = result.GetString("facility_id", ""),
                Name = result.GetString("facility_name", "<No name>"),
                TypeID = result.GetString("facility_type_id", ""),
                TypeName = result.GetString("facility_type", ""),
                ZoneID = result.GetString("zone_id", "")
            };

            return fac;
        }
    }
}
