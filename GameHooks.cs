using HarmonyLib;

namespace MineMogulModMenu {
    [HarmonyPatch(typeof(UIManager), nameof(UIManager.IsInAnyMenu))]
    public class MenuPatch {
        static private void Postfix(ref bool __result) {
            __result = Plugin.MenuComponent.ShowMenu || __result;
        }
    }
    [HarmonyPatch(typeof(OrePiece), "DelayThenSell")]
    public class OreSellDelayPatch {
        static private bool Prefix(ref float delayBeforeSelling) {
            delayBeforeSelling = Config.Instance.DepositBox.InstantSell ? 0f : delayBeforeSelling;
            return true;
        }
    }
    [HarmonyPatch(typeof(PolishingMachine), "MakeDirty")]
    public class DirtyOrePatch {
        // skip the original method if we are ignoring dirty ores
        // continue with original method if we want to scan for dirty ores
        static private bool Prefix() {
            return !Config.Instance.PolishingMachine.IgnoreDirtyOres;
        }
    }
}