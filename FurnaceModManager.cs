using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MineMogulModMenu {
    public class FurnaceModManager : MonoBehaviour {
        public static int SelectedFurnaceIndex = 0;
        private static BlastFurnace selectedFurnace;
        public static BlastFurnace GetFurnaceByIndex(int index) {
            if (index >= 0 && index < GameUtilities.Furnaces.Count)
                return GameUtilities.Furnaces[index];
            return null;
        }
        private void FixedUpdate() {
            selectedFurnace = GameUtilities.Furnaces.Where((furnace, index) => index == Config.Instance.Furnaces.SelectedIndex).FirstOrDefault();
            
            GameUtilities.Furnaces.ForEach(furnace => {
                if (furnace) {
                    Renderer renderer = furnace.GetComponent<Renderer>();
                    if (renderer) {
                        BoundingBoxManager.TryAddBoundingBoxEntry(new BoundingBoxEntry {
                            Target = renderer,
                            Object = furnace,
                            IdentifierGroup = 429,
                            Enabled = true,
                            DrawColor = Color.orangeRed
                        });
                    }
                }
            });

            // must be all furnaces selected
            if (!selectedFurnace && Config.Instance.Furnaces.HighlightSelected) {
                BoundingBoxManager.EnableAllBoundingBoxesByIdentifierGroup(429);
            }
            else if (Config.Instance.Furnaces.HighlightSelected) {
                BoundingBoxManager.DisableAllBoundingBoxesByIdentifierGroup(429);
                BoundingBoxManager.EnableBoundingBoxByObject(selectedFurnace);
            }
            else {
                BoundingBoxManager.DisableAllBoundingBoxesByIdentifierGroup(429);
            }
        }
        public static void ApplyProcessingTime(float processingTIme) {
            if (selectedFurnace == null)
                GameUtilities.Furnaces.ForEach(furnace =>
                {
                    furnace.ProcessingTime = processingTIme;
                });
            else if (selectedFurnace != null) {
                selectedFurnace.ProcessingTime = processingTIme;
            }
        }
    }
}