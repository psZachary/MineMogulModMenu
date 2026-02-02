using UnityEngine;

namespace MineMogulModMenu
{
    public class Config
    {
        public static Config Instance { get; } = new Config();

        public MinerConfig Miners { get; } = new MinerConfig();
        public PlayerConfig Player { get; } = new PlayerConfig();
        public FurnacesConfig Furnaces { get; } = new FurnacesConfig();
        public DepositBoxConfig DepositBox { get; } = new DepositBoxConfig();
        public EconomyConfig Economy { get; } = new EconomyConfig();
        public ResearchConfig Research { get; } = new ResearchConfig();
        public PolishingMachineConfig PolishingMachine { get; } = new PolishingMachineConfig();
        public SettingsConfig Settings { get; } = new SettingsConfig();
        public class PolishingMachineConfig
        {
            public bool IgnoreDirtyOres = false;
        }
        public class EconomyConfig
        {
            public string AddMoney = "500";
        }
        public class ResearchConfig
        {
            public string AddTickets = "10";
        }
        public class MinerConfig
        {
            public bool HighlightSelected = true;
            public int SelectedIndex = 0;
            public float SpawnRate = 1f;
        }

        public class FurnacesConfig
        {
            public bool HighlightSelected = true;
            public float ProcessingTime = 1f;
            public int SelectedIndex = 0;
        }

        public class DepositBoxConfig
        {
            public bool InstantSell = false;
        }
        public class PlayerConfig
        {
            public bool Noclip = false;
            public float NoclipSpeed = 20f;
            public float WalkSpeed = 4f;
            public float SprintSpeed = 6f;
            public float JumpHeight = 2f;
        }
        public class SettingsConfig
        {
            public Color MinerHighlightColor = Color.blue;
            public Color FurnaceHighlightColor = Color.orangeRed;
            public bool HighlightThroughWalls = true;
        }
    }
}
