using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace watchtower.Code.Constants {

    public static class Vehicle {

        public static string FLASH = "1";
        public static string SUNDERER = "2";
        public static string LIGHTNING = "3";
        public static string MAGRIDER = "4";
        public static string VANGUARD = "5";
        public static string PROWLER = "6";
        public static string SCYTHE = "7";
        public static string REAVER = "8";
        public static string MOSQUITO = "9";
        public static string LIBERATOR = "10";
        public static string GALAXY = "11";
        public static string HARASSER = "12";
        public static string DROP_POD = "13";
        public static string VALKYRIE = "14";
        public static string ANT = "15";

        public static string CONSTRUCTION_AI = "100";
        public static string MANA_AI = "101";
        public static string MANA_AV = "102";
        public static string SPITFIRE = "103";
        public static string CONSTRUCTION_AA = "150";
        public static string CONSTRUCTION_AV = "151";
        public static string CONSTRUCTION_AV_TOWER = "160";
        public static string CONSTRUCTION_AA_TOWER = "161";
        public static string CONSTRUCTION_AI_TOWER = "162";
        public static string GLAIVE = "163";
        public static string PHOENIX = "1012";
        public static string COLOSSUS = "2007";
        public static string POCKET_FLASH = "2010";
        public static string BASTION = "2019";
        public static string FLAIL = "2021";
        public static string JAVELIN = "2125";
        public static string JAVELIN2 = "2129";
        public static string JEVELIN3 = "2033";
        public static string DERVISH = "2136";
        public static string CHIMERA = "2137";
        public static string BASTION_MOSQUITO = "2122";
        public static string BASTION_REAVER = "2123";
        public static string BASTION_SCYTHE = "2124";

    }

    public class PsVehicle {

        /// <summary>
        ///     How many points will be given when this vehicle is destroyed
        /// </summary>
        public int DeathScore { get; set; }

        /// <summary>
        ///     Multiplier when this vehicle gets a kill
        /// </summary>
        public int KillMultiplier { get; set; } = 1;

        /// <summary>
        ///     ID of the vehicle
        /// </summary>
        public string VehicleID { get; set; } = "";

        /// <summary>
        ///     Name of the vehicle
        /// </summary>
        public string Name { get; set; } = "";

        private static Dictionary<string, PsVehicle> _Map = new Dictionary<string, PsVehicle>();

        protected PsVehicle(string ID, string name, int kill, int death) {
            VehicleID = ID;
            Name = name;
            KillMultiplier = kill;
            DeathScore = death;

            _Map.Add(ID, this);
        }

        public static PsVehicle? Get(string vehicleID) {
            _ = _Map.TryGetValue(vehicleID, out PsVehicle? v);
            return v;
        }

        public static PsVehicle INFANTRY = new PsVehicle("0", "Infantry", 1, 1);
        public static PsVehicle FLASH = new PsVehicle(Vehicle.FLASH, "Flash", 1, 1);
        public static PsVehicle SUNDERER = new PsVehicle(Vehicle.SUNDERER, "Sunderer", 1, 2);
        public static PsVehicle LIGHTNING = new PsVehicle(Vehicle.LIGHTNING, "Lightning", 1, 2);
        public static PsVehicle MAGRIDER = new PsVehicle(Vehicle.MAGRIDER, "Magrider", 1, 3);
        public static PsVehicle VANGUARD = new PsVehicle(Vehicle.VANGUARD, "Vanguard", 1, 3);
        public static PsVehicle PROWLER = new PsVehicle(Vehicle.PROWLER, "Prowler", 1, 3);
        public static PsVehicle SCYTHE = new PsVehicle(Vehicle.SCYTHE, "Scythe", 1, 1);
        public static PsVehicle REAVER = new PsVehicle(Vehicle.REAVER, "Reaver", 1, 1);
        public static PsVehicle MOSQUITO = new PsVehicle(Vehicle.MOSQUITO, "Mosquito", 1, 1);
        public static PsVehicle LIBERATOR = new PsVehicle(Vehicle.LIBERATOR, "Liberator", 1, 3);
        public static PsVehicle GALAXY = new PsVehicle(Vehicle.GALAXY, "Galaxy", 0, 0);
        public static PsVehicle HARASSER = new PsVehicle(Vehicle.HARASSER, "Harasser", 1, 2);
        public static PsVehicle DROP_POD = new PsVehicle(Vehicle.DROP_POD, "Drop pod", 1, 1);
        public static PsVehicle VALKYRIE = new PsVehicle(Vehicle.VALKYRIE, "Valkyrie", 0, 0);
        public static PsVehicle ANT = new PsVehicle(Vehicle.ANT, "ANT", 1, 3);
        public static PsVehicle SPITFIRE = new PsVehicle(Vehicle.SPITFIRE, "Spitfire", 0, 0);
        public static PsVehicle CONSTRUCTION_AI_TURRET = new PsVehicle(Vehicle.CONSTRUCTION_AI, "AA Turret", 2, 1);
        public static PsVehicle MANA_AI = new PsVehicle(Vehicle.MANA_AI, "MANA AI", 1, 1);
        public static PsVehicle MANA_AV = new PsVehicle(Vehicle.MANA_AV, "MANA AV", 1, 1);
        public static PsVehicle CONSTRUCTION_AA_TURRET = new PsVehicle(Vehicle.CONSTRUCTION_AA, "AA Turret", 2, 1);
        public static PsVehicle CONSTRUCTION_AV_TURRET = new PsVehicle(Vehicle.CONSTRUCTION_AV, "AV Turret", 2, 1);
        public static PsVehicle CONSTRUCTION_AV_TOWER = new PsVehicle(Vehicle.CONSTRUCTION_AV_TOWER, "AV Tower", 2, 1);
        public static PsVehicle CONSTRUCTION_AA_TOWER = new PsVehicle(Vehicle.CONSTRUCTION_AA_TOWER, "AA Tower", 2, 1);
        public static PsVehicle CONSTRUCTION_AI_TOWER = new PsVehicle(Vehicle.CONSTRUCTION_AI_TOWER, "AI Tower", 2, 1);
        public static PsVehicle GLAIVE = new PsVehicle(Vehicle.GLAIVE, "Glaive", 3, 1);
        public static PsVehicle PHOENIX = new PsVehicle(Vehicle.PHOENIX, "Phoenix", 0, 0);
        /*
        public static PsVehicle CONSTRUCTION_AV_TURRET2 = new PsVehicle(Vehicle.FLASH, "Flash", 1, 1);
        public static PsVehicle CONSTRUCTION_AI_TURRET2 = new PsVehicle(Vehicle.FLASH, "Flash", 1, 1);
        public static PsVehicle CONSTRUCTION_AA_TURRET2 = new PsVehicle(Vehicle.FLASH, "Flash", 1, 1);
        */
        public static PsVehicle COLOSSUS = new PsVehicle(Vehicle.COLOSSUS, "Colossus", 1, 1);
        public static PsVehicle POCKET_FLASH = new PsVehicle(Vehicle.POCKET_FLASH, "Flash XS-1", 0, 0);
        public static PsVehicle FLAIL = new PsVehicle(Vehicle.FLAIL, "Flail", 3, 1);
        public static PsVehicle JAVELIN = new PsVehicle(Vehicle.JAVELIN, "Javelin", 0, 0);
        public static PsVehicle JAVELIN2 = new PsVehicle(Vehicle.JAVELIN2, "Javelin2", 0, 0);
        public static PsVehicle JAVELIN3 = new PsVehicle(Vehicle.JEVELIN3, "Javelin3", 0, 0);
        public static PsVehicle BASTION = new PsVehicle(Vehicle.BASTION, "Bastion", 0, 0);
        public static PsVehicle BASTION_MOSQUITO = new PsVehicle(Vehicle.BASTION_MOSQUITO, "Bastion Mosquito", 0, 0);
        public static PsVehicle BASTION_REAVER = new PsVehicle(Vehicle.BASTION_REAVER, "Bastion Reaver", 0, 0);
        public static PsVehicle BASTION_SCYTHE = new PsVehicle(Vehicle.BASTION_SCYTHE, "Bastion Scythe", 0, 0);
        public static PsVehicle DERVISH = new PsVehicle(Vehicle.DERVISH, "Dervish", 0, 0);
        public static PsVehicle CHIMERA = new PsVehicle(Vehicle.CHIMERA, "Chimera", 0, 0);

    }

}
