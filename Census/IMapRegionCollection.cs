using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using watchtower.Models.Census;

namespace watchtower.Census {

    public interface IMapRegionCollection {

        /// <summary>
        /// Get the <see cref="PsFacility"/> by its <see cref="PsFacility.RegionID"/>
        /// </summary>
        /// <param name="regionID"></param>
        /// <returns></returns>
        Task<PsFacility> GetByRegionID(string regionID);

        Task<PsFacility> GetByFacilityID(string facilityID);

        Task<List<PsFacility>> GetByZoneID(string facilityID);

        Task<List<PsFacility>> GetAll();

    }
}
