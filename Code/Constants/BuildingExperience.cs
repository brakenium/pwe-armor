using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace watchtower.Code.Constants {

    public class BuildingExperience {

        public string Name { get; set; } = "";

        public int Points { get; set; } = 0;

        public string ExperienceID { get; set; } = "";

        protected BuildingExperience(string name, int points, string expID) {
            Name = name;
            Points = points;
            ExperienceID = expID;
        }

        private const int POINTS_SMALL = 6;
        private const int POINTS_MEDIUM = 6;
        private const int POINTS_LARGE = 6;
        private const int POINTS_OTHER = 6;

        public static BuildingExperience ALARM_MODULE = new BuildingExperience("Alarm Module", POINTS_SMALL, "616");
        public static BuildingExperience BLAST_WALL = new BuildingExperience("Blast Wall", POINTS_LARGE, "616");
        public static BuildingExperience BUNKER = new BuildingExperience("Bunker", POINTS_LARGE, "616");
        public static BuildingExperience CORTIUM_SILO = new BuildingExperience("Cortium Silo", POINTS_LARGE, "616");
        public static BuildingExperience ELYSIUM_SPAWN_TUBE = new BuildingExperience("Elysium Spawn Tube", POINTS_MEDIUM, "616");
        public static BuildingExperience GLAIVE = new BuildingExperience("Glaive", POINTS_MEDIUM, "616");
        public static BuildingExperience AA_TOWER = new BuildingExperience("AA Tower", POINTS_MEDIUM, "616");
        public static BuildingExperience INFANTRY_TOWER = new BuildingExperience("Infantry Tower", POINTS_LARGE, "616");
        public static BuildingExperience AIR_TERMINAL = new BuildingExperience("Air Terminal", POINTS_MEDIUM, "616");
        public static BuildingExperience VEHICLE_TERMINAL = new BuildingExperience("Vehicle Terminal", POINTS_MEDIUM, "616");
        public static BuildingExperience ORBITAL_STRIKE_UPLINK = new BuildingExperience("Orbital Strike Uplink", POINTS_LARGE, "616");
        public static BuildingExperience PAIN_SPIRE = new BuildingExperience("Pain Spire", POINTS_SMALL, "616");
        public static BuildingExperience PILLBOX = new BuildingExperience("Pillbox", POINTS_MEDIUM, "616");
        public static BuildingExperience RAMPART_WALL = new BuildingExperience("Rampart Wall", POINTS_LARGE, "616");
        public static BuildingExperience REENFORCEMENTS_MODULE = new BuildingExperience("Reinforcements Module", POINTS_SMALL, "616");
        public static BuildingExperience REPAIR_MODULE = new BuildingExperience("Repair Module", POINTS_SMALL, "616");
        public static BuildingExperience ROUTING_SPIRE = new BuildingExperience("Routing Spire", POINTS_SMALL, "616");
        public static BuildingExperience SKYWALL_SHIELD_EMITTER = new BuildingExperience("Skywall Shield Emitter", POINTS_SMALL, "616");
        public static BuildingExperience AV_TOWER = new BuildingExperience("AV Tower", POINTS_MEDIUM, "616");
        public static BuildingExperience AV_TURRET = new BuildingExperience("AV Turret", POINTS_MEDIUM, "616");
        public static BuildingExperience SHIELD_MODULE = new BuildingExperience("Shield Module", POINTS_SMALL, "616");
        public static BuildingExperience SUNDERER_GARAGE = new BuildingExperience("Sunderer Garage", POINTS_MEDIUM, "616");
        public static BuildingExperience FLAIL = new BuildingExperience("Flail", POINTS_MEDIUM, "616");
        public static BuildingExperience AI_MODULE = new BuildingExperience("AI Module", POINTS_SMALL, "616");
        public static BuildingExperience VEHICLE_AMMO_DISPENSER = new BuildingExperience("Vehicle Ammo Dispenser", POINTS_OTHER, "616");
        public static BuildingExperience VEHICLE_GATE = new BuildingExperience("Vehicle Gate", POINTS_LARGE, "616");
        public static BuildingExperience VEHICLE_RAMP = new BuildingExperience("Vehicle Ramp", POINTS_MEDIUM, "616");
        public static BuildingExperience AI_TOWER = new BuildingExperience("AI Tower", POINTS_MEDIUM, "616");


    }
}
