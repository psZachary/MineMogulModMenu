using BepInEx;
using UnityEngine;

namespace MineMogulModMenu
{
    [BepInPlugin("com.you.minemogumodmenu", "MineMogulModMenu", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static Menu MenuComponent;
        internal static GameUtilities GameUtilitiesComponent;
        private void Awake()
        {
            Logging.Log = Logger;
            MenuComponent = gameObject.AddComponent<Menu>();
            Logging.Log.LogInfo("Added Menu component");
            GameUtilitiesComponent = gameObject.AddComponent<GameUtilities>();
            Logging.Log.LogInfo("Added GameUtilities component");
        }
    }
}