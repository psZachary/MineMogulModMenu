using HarmonyLib;
using UnityEngine;

namespace MineMogulModMenu {
    [HarmonyPatch(typeof(PolishingMachine), "MakeDirty")]
    public class DirtyOrePatch {
        // skip the original method if we are ignoring dirty ores
        // continue with original method if we want to scan for dirty ores
        static private bool Prefix() {
            return !Config.Instance.PolishingMachine.IgnoreDirtyOres;
        }
    }
}