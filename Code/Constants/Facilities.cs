using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace watchtower.Code.Constants {

    public static class Facilities {

        public static string Allatum = "4001";
        public static string AllatumResearchLab = "4010";
        public static string AllatumBroadcastHub = "4020";
        public static string AllatumBotanyWing = "4030";
        public static string J908ImpactSite = "5200";
        public static string GalaxySolarPlant = "220";
        public static string BriggsLab = "211";
        public static string SeabedListeningPost = "217";
        public static string CeresHydroponics = "219";
        public static string TheCrown = "6200";
        public static string TiAlloys = "218";
        public static string IndarCommArray = "214";
        public static string SnakeRavineLookout = "229";
        public static string CrossroadsWatchtower = "6100";
        public static string Zurvan = "118000";
        public static string ZurvanPumpStation = "118010";
        public static string ZurvanStorageYard = "118020";
        public static string Dahaka = "3201";
        public static string DahakaSouthernPost = "3230";
        public static string DahakaPumpStation = "3220";
        public static string CrimsonBluffTower = "5900";

        public static List<string> Indar = new List<string>() {
            Allatum, AllatumResearchLab, AllatumBroadcastHub, AllatumBotanyWing,
            J908ImpactSite, GalaxySolarPlant, BriggsLab, SeabedListeningPost,
            CeresHydroponics, TheCrown, TiAlloys,
            IndarCommArray,
            SnakeRavineLookout,
            CrossroadsWatchtower,
            Zurvan, ZurvanPumpStation, ZurvanStorageYard,
            Dahaka, DahakaSouthernPost, DahakaPumpStation,
            CrimsonBluffTower
        };

        public static string Acan = "302000";
        public static string EastAcanStorage = "302010";
        public static string AcanDataHub = "302020";
        public static string AcanSouthernLbs = "302030";
        public static string Ixtab = "301000";
        public static string IxtabPowerReg = "301010";
        public static string IxtabWaterPurification = "301020";
        public static string IxtabSouthernPass = "301030";
        public static string Hurakan = "300000";
        public static string HurakanSecureStorage = "300010";
        public static string HurakanWesternPass = "300020";
        public static string HurakanSouthernDepot = "300030";
        public static string HatcherAirstation = "297000";
        public static string ConstructionSiteEpsilon = "287080";
        public static string WainwrightArmory = "264000";
        public static string BrokenValeGarrison = "275000";
        public static string WoodmanASELabs = "276000";
        public static string NasonsDefiance = "298000";
        public static string MossridgeCommandCenter = "287090";
        public static string BravataPMCCompound = "294000";
        public static string IronQuay = "274000";
        public static string FortDrexler = "290000";
        public static string EasternSubstation = "287060";
        public static string GenesisTerraformingPlant = "293000";
        public static string GourneyDam = "280000";
        public static string OutpostLambda = "287110";
        public static string HuntersBlind = "279000";
        public static string SRPHydro = "281000";
        public static string CairnStation = "291000";
        public static string TakkonStorage = "287040";

        public static List<string> Hossin = new List<string>() {
             Acan, EastAcanStorage, AcanDataHub, AcanSouthernLbs,
             Ixtab, IxtabPowerReg, IxtabWaterPurification, IxtabSouthernPass,
             Hurakan, HurakanSecureStorage, HurakanWesternPass, HurakanSouthernDepot,
             HatcherAirstation,
             ConstructionSiteEpsilon,
             WainwrightArmory,
             BrokenValeGarrison,
             WoodmanASELabs,
             NasonsDefiance,
             MossridgeCommandCenter,
             BravataPMCCompound,
             IronQuay,
             FortDrexler,
             EasternSubstation,
             GenesisTerraformingPlant,
             GourneyDam,
             OutpostLambda,
             HuntersBlind,
             SRPHydro,
             CairnStation,
             TakkonStorage
        };

        public static string Heyoka = "206000";
        public static string HeyokaArmory = "206001";
        public static string HeyokaChemicalLab = "206002";
        public static string Mekala = "208000";
        public static string MekalaCartMining = "208001";
        public static string MekelaAuxCompound = "208002";
        public static string Tumas = "211000";
        public static string TumasSkylanceBattery = "211001";
        public static string TumasCargo = "211002";
        public static string TheBastion = "217000";
        public static string DeepCoreGeolab = "222110";
        public static string AuraxicomNetworkHub = "222060";
        public static string CruxHeadquarters = "216000";
        public static string RockslideOutlook = "222150";
        public static string LithCorpCentral = "222350";
        public static string TheAscent = "222280";
        public static string RavenLanding = "222270";
        public static string ChimneyRockDepot = "222180";
        public static string AuraxisFirearmsCorp = "218000";

        public static List<string> Amerish = new List<string>() {
             Heyoka, HeyokaArmory, HeyokaChemicalLab,
             Mekala, MekalaCartMining, MekelaAuxCompound,
             Tumas, TumasSkylanceBattery, TumasCargo,
             TheBastion,
             DeepCoreGeolab,
             AuraxicomNetworkHub,
             CruxHeadquarters,
             RockslideOutlook,
             LithCorpCentral,
             TheAscent,
             RavenLanding,
             ChimneyRockDepot,
             AuraxisFirearmsCorp
        };

        public static string Freyr = "253000";
        public static string ManiLakeOutpost = "255030";
        public static string ApexGenetics = "234000";
        public static string AuroraMaterialsLab = "233000";
        public static string RimeAnalytics = "244610";
        public static string UntappedReservoir = "400135";
        public static string TheRink = "244620";
        public static string TheTraverse = "237000";
        public static string MatthersonsTriump = "246000";
        public static string SaerroListeningPost = "248000";
        public static string Eisa = "254000";
        public static string EchoValleySubstation = "244100";
        public static string WatersonsRedemption = "249000";
        public static string Baldur = "400314";
        public static string PaleCanyonChemical = "239000";

        public static List<string> Esamir = new List<string>() {
             Freyr,
             ManiLakeOutpost,
             ApexGenetics,
             AuroraMaterialsLab,
             RimeAnalytics,
             UntappedReservoir,
             TheRink,
             TheTraverse,
             MatthersonsTriump,
             SaerroListeningPost,
             Eisa,
             EchoValleySubstation,
             WatersonsRedemption,
             Baldur,
             PaleCanyonChemical
        };

        private static Dictionary<string, string> Bases = new Dictionary<string, string>();

        public static void DoHorribleUglyThing() {

            Type t = typeof(Facilities);
            FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
            
            foreach (FieldInfo fi in fields) {
                if (fi.FieldType != typeof(string)) {
                    continue;
                }
                Console.WriteLine($"{fi.Name} {fi.GetValue(null)}");
                Bases.Add(fi.GetValue(null)!.ToString() ?? "", fi.Name);
            }
        }

        public static string? GetBaseNames(string facilityID) {
            Bases.TryGetValue(facilityID, out string? val);
            return val;
        }

    }
}
