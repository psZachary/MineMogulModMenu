using BepInEx;
using BepInEx.Logging;
using UnityEngine;

namespace MineMogulModMenu
{
    [BepInPlugin("com.you.minemogumodmenu", "MineMogulModMenu", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        private bool showMenu = false;
        
        private void Awake()
        {
            Log = Logger;
            Log.LogInfo("Plugin loaded!");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                showMenu = !showMenu;
        }

        private void OnGUI()
        {
            if (!showMenu) return;

            GUI.Box(new Rect(10, 10, 200, 300), "Mod Menu");

            if (GUI.Button(new Rect(20, 40, 180, 30), "Button 1"))
            {
                Log.LogInfo("Button 1 clicked");
            }

            if (GUI.Button(new Rect(20, 80, 180, 30), "Button 2"))
            {
                Log.LogInfo("Button 2 clicked");
            }

            GUI.Label(new Rect(20, 120, 180, 20), "Some label text");
        }
    }
}