using BepInEx;
using UnityEngine;

namespace MineMogulModMenu
{
    [BepInPlugin("com.you.minemogumodmenu", "MineMogulModMenu", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static Menu MenuComponent;
        internal static GameUtilities GameUtilitiesComponent;
        internal static BoundingBoxManager BoundingBoxManagerComponent;
        internal static MinerModManager MinerModManagerComponent;
        internal static FurnaceModManager FurnaceModManagerComponent;
        private void Awake()
        {
            Logging.Log = Logger;
            MenuComponent = gameObject.AddComponent<Menu>();
            Logging.Log.LogInfo("Added Menu component");
            GameUtilitiesComponent = gameObject.AddComponent<GameUtilities>();
            Logging.Log.LogInfo("Added GameUtilities component");
            BoundingBoxManagerComponent = gameObject.AddComponent<BoundingBoxManager>();
            Logging.Log.LogInfo("Added BoundingBoxManager component");
            MinerModManagerComponent = gameObject.AddComponent<MinerModManager>();
            Logging.Log.LogInfo("Added MinerModManager component");
            FurnaceModManagerComponent = gameObject.AddComponent<FurnaceModManager>();
            Logging.Log.LogInfo("Added FurnaceModManager component");
        }
    }
}