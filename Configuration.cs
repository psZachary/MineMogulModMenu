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
        public class EconomyConfig {
            public string AddMoney = "500";
        }
        public class MinerConfig
        {
            public float SpawnRate = 1f;
        }
        
        public class FurnacesConfig
        {
            public float ProcessingTime = 1f;
        }
        public class DepositBoxConfig
        {
            public float Speed = 2f;
        }
        public class PlayerConfig
        {
            public float WalkSpeed = 5f;
            public float SprintSpeed = 5f;
        }
    }
}
