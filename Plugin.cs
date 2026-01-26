using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace MineMogulModMenu
{
    [BepInPlugin("com.pszachary.minemogumodmenu", "MineMogulModMenu", "1.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static Menu MenuComponent;
        internal static GameUtilities GameUtilitiesComponent;
        internal static BoundingBoxManager BoundingBoxManagerComponent;
        internal static MinerModManager MinerModManagerComponent;
        internal static FurnaceModManager FurnaceModManagerComponent;
        internal static NoclipController NoclipControllerComponent;
        internal static Harmony Harmony;

        private void Awake()
        {
            Logging.Log = Logger;

            try
            {
                Harmony = new Harmony("com.pszachary.minemogumodmenu");
                Harmony.PatchAll();
                Logging.Log.LogInfo("Patching complete");
            }
            catch (System.Exception e)
            {
                Logging.Log.LogError($"Harmony patch failed: {e}");
            }

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
            NoclipControllerComponent = gameObject.AddComponent<NoclipController>();
            Logging.Log.LogInfo("Added NoclipController component");
        }
    }
}