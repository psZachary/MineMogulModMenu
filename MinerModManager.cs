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
        private void FixedUpdate() {
            selectedMiner = GameUtilities.AutoMiners.Where((miner, index) => index == Config.Instance.Miners.SelectedIndex).FirstOrDefault();
            
            GameUtilities.AutoMiners.ForEach(miner => {
                if (miner) {
                    Renderer minerRenderer = miner.GetComponent<Renderer>();
                    if (minerRenderer) {
                        BoundingBoxManager.TryAddBoundingBoxEntry(new BoundingBoxEntry {
                            Target = minerRenderer,
                            Object = miner,
                            IdentifierGroup = 428,
                            Enabled = true,
                            DrawColor = Color.blue
                        });
                    }
                }
            });

            // must be all miners selected
            if (!selectedMiner && Config.Instance.Miners.HighlightSelected) {
                BoundingBoxManager.EnableAllBoundingBoxesByIdentifierGroup(428);
            }
            else if (Config.Instance.Miners.HighlightSelected) {
                BoundingBoxManager.DisableAllBoundingBoxesByIdentifierGroup(428);
                BoundingBoxManager.EnableBoundingBoxByObject(selectedMiner);
            }
            else {
                BoundingBoxManager.DisableAllBoundingBoxesByIdentifierGroup(428);
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