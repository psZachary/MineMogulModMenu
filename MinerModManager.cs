using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MineMogulModMenu {
    public class MinerModManager : MonoBehaviour {
        public static int SelectedMinerIndex = 0;
        private static AutoMiner selectedMiner;
        public static AutoMiner GetAutoMinerByIndex(int index) {
            if (index >= 0 && index < GameUtilities.AutoMiners.Count)
                return GameUtilities.AutoMiners[index];
            return null;
        }
        private void Update() {
            selectedMiner = GameUtilities.AutoMiners.Where((miner, index) => index == Config.Instance.Miners.SelectedIndex).FirstOrDefault();

            if (Config.Instance.Miners.HighlightSelected) {
                if (selectedMiner) {
                    DrawingManager.NextDrawEntries.Add(new DrawingEntry{
                        DrawType = DrawType.BoundingBox,
                        Renderers = [selectedMiner.GetComponent<Renderer>()],
                        Color = Color.orangeRed
                    });
                }
                else {
                    DrawingManager.NextDrawEntries.Add(new DrawingEntry{
                        DrawType = DrawType.BoundingBox,
                        Renderers = [.. GameUtilities.AutoMiners.Select(f => f.GetComponent<Renderer>())],
                        Color = Color.blue
                    });
                }
            }
        }
        public static void ApplySpawnRate(float spawnRate) {
            if (selectedMiner == null)
                GameUtilities.AutoMiners.ForEach(miner =>
                {
                    miner.SpawnRate = spawnRate;
                });
            else if (selectedMiner != null) {
                selectedMiner.SpawnRate = spawnRate;
            }
        }
    }
}