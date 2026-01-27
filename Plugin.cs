using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace MineMogulModMenu
{
    [BepInPlugin("com.pszachary.minemogumodmenu", "MineMogulModMenu", "1.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static Menu MenuComponent;
        internal static GameUtilities GameUtilitiesComponent;
        internal static DrawingManager BoundingBoxManagerComponent;
        internal static MinerModManager MinerModManagerComponent;
        internal static FurnaceModManager FurnaceModManagerComponent;
        internal static NoclipController NoclipControllerComponent;
        internal static Harmony Harmony;
        internal static ManualLogSource Log;
        private void Awake()
        {
            Log = Logger;
            try
            {
                Harmony = new Harmony("com.pszachary.minemogumodmenu");
                Harmony.PatchAll();
                Logger.LogInfo("Patching complete");
            }
            catch (System.Exception e)
            {
                Logger.LogError($"Harmony patch failed: {e}");
            }

            MenuComponent = gameObject.AddComponent<Menu>();
            Logger.LogInfo("Added Menu component");
            GameUtilitiesComponent = gameObject.AddComponent<GameUtilities>();
            Logger.LogInfo("Added GameUtilities component");
            BoundingBoxManagerComponent = gameObject.AddComponent<DrawingManager>();
            Logger.LogInfo("Added BoundingBoxManager component");
            MinerModManagerComponent = gameObject.AddComponent<MinerModManager>();
            Logger.LogInfo("Added MinerModManager component");
            FurnaceModManagerComponent = gameObject.AddComponent<FurnaceModManager>();
            Logger.LogInfo("Added FurnaceModManager component");
            NoclipControllerComponent = gameObject.AddComponent<NoclipController>();
            Logger.LogInfo("Added NoclipController component");
        }
    }
}