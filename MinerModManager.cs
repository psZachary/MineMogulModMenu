using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MineMogulModMenu
{
    public class MinerModManager : MonoBehaviour
    {
        public static int SelectedMinerIndex = 0;
        public static AutoMiner SelectedMiner => _selectedMiner;
        private static AutoMiner _selectedMiner;
        public static AutoMiner GetAutoMinerByIndex(int index)
        {
            if (index >= 0 && index < GameUtilities.AutoMiners.Count)
                return GameUtilities.AutoMiners[index];
            return null;
        }
        private void Update()
        {
            _selectedMiner = GameUtilities.AutoMiners
                .Where((miner, index) => index == Config.Instance.Miners.SelectedIndex)
                .FirstOrDefault();

            if (!Config.Instance.Miners.HighlightSelected)
                return;

            if (_selectedMiner != null)
            {
                var renderer = _selectedMiner.GetComponent<Renderer>();
                if (renderer != null)
                {
                    DrawingManager.NextDrawEntries.Add(new DrawingEntry
                    {
                        DrawType = DrawType.BoundingBox,
                        Renderers = [renderer],
                        Color = Config.Instance.Settings.MinerHighlightColor
                    });
                }
            }
            else
                {
                    var renderers = GameUtilities.AutoMiners
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
                            Color = Config.Instance.Settings.MinerHighlightColor
                        });
                    }
                }
        }
        public static void ApplySpawnRate(float spawnRate)
        {
            if (_selectedMiner == null)
                GameUtilities.AutoMiners.ForEach(miner =>
                {
                    miner.SpawnRate = spawnRate;
                });
            else if (_selectedMiner != null)
            {
                _selectedMiner.SpawnRate = spawnRate;
            }
        }
    }
}