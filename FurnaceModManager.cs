using System.Linq;
using UnityEngine;

namespace MineMogulModMenu
{
    public class FurnaceModManager : MonoBehaviour
    {
        public static int SelectedFurnaceIndex = 0;
        private static BlastFurnace selectedFurnace;
        public static BlastFurnace GetFurnaceByIndex(int index)
        {
            if (index >= 0 && index < GameUtilities.Furnaces.Count)
                return GameUtilities.Furnaces[index];
            return null;
        }

        private void Update()
        {
            selectedFurnace = GameUtilities.Furnaces
                .Where((furnace, index) => index == Config.Instance.Furnaces.SelectedIndex)
                .FirstOrDefault();

            if (!Config.Instance.Furnaces.HighlightSelected)
                return;

            if (selectedFurnace != null)
            {
                var renderer = selectedFurnace.GetComponent<Renderer>();
                if (renderer != null)
                {
                    DrawingManager.NextDrawEntries.Add(new DrawingEntry
                    {
                        DrawType = DrawType.BoundingBox,
                        Renderers = [renderer],
                        Color = Config.Instance.Settings.FurnaceHighlightColor
                    });
                }
            }
            else
            {
                var renderers = GameUtilities.Furnaces
                    .Where(f => f != null)
                    .Select(f => f.GetComponent<Renderer>())
                    .Where(r => r != null)
                    .ToArray();

                if (renderers.Length > 0)
                {
                    DrawingManager.NextDrawEntries.Add(new DrawingEntry
                    {
                        DrawType = DrawType.BoundingBox,
                        Renderers = renderers,
                        Color = Config.Instance.Settings.FurnaceHighlightColor
                    });
                }
            }
        }

        public static void ApplyProcessingTime(float processingTIme)
        {
            if (selectedFurnace == null)
                GameUtilities.Furnaces.ForEach(furnace =>
                {
                    furnace.ProcessingTime = processingTIme;
                });
            else if (selectedFurnace != null)
            {
                selectedFurnace.ProcessingTime = processingTIme;
            }
        }
    }
}