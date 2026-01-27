using System.Linq;
using System.Runtime.InteropServices;
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
            selectedFurnace = GameUtilities.Furnaces.Where((furnace, index) => index == Config.Instance.Furnaces.SelectedIndex).FirstOrDefault();

            if (Config.Instance.Furnaces.HighlightSelected)
            {
                if (selectedFurnace)
                {
                    DrawingManager.NextDrawEntries.Add(new DrawingEntry
                    {
                        DrawType = DrawType.BoundingBox,
                        Renderers = [selectedFurnace.GetComponent<Renderer>()],
                        Color = Config.Instance.Settings.FurnaceHighlightColor
                    });
                }
                else
                {
                    DrawingManager.NextDrawEntries.Add(new DrawingEntry
                    {
                        DrawType = DrawType.BoundingBox,
                        Renderers = [.. GameUtilities.Furnaces.Select(f => f.GetComponent<Renderer>())],
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