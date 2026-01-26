using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MineMogulModMenu
{
    public enum MenuTab
    {
        Player,
        Economy,
        World,
        Quests,
        Settings
    }
    public enum SubMenuTab
    {
        Miners,
        Furnaces,
        DepositBox
    }
    public class Menu : MonoBehaviour
    {
        public MenuTab CurrentTab { get; private set; }
        public SubMenuTab CurrentSubTab { get; private set; }
        public bool ShowMenu { get; private set; }
        private Rect windowRect = new Rect(100, 100, 750f, 450f);

        private void Start()
        {
            DontDestroyOnLoad(this);
            CurrentTab = MenuTab.Player;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                ShowMenu = !ShowMenu;

            }

            if (!Cursor.visible && ShowMenu)
                Cursor.visible = true;
            if (Cursor.lockState != CursorLockMode.None && ShowMenu)
                Cursor.lockState = CursorLockMode.Locked;

        }
        private void OnPlayerTab()
        {
            MenuUtilities.Space(10f);

            Config.Instance.Player.WalkSpeed = MenuUtilities.HorizontalSlider("Walk Speed", Config.Instance.Player.WalkSpeed, 0.00f, 20f);
            if (MenuUtilities.Button("Set Walk Speed"))
            {
                GameUtilities.LocalPlayerController.WalkSpeed = Config.Instance.Player.WalkSpeed;
            }
            Config.Instance.Player.SprintSpeed = MenuUtilities.HorizontalSlider("Sprint Speed", Config.Instance.Player.SprintSpeed, 0.00f, 20f);
            if (MenuUtilities.Button("Set Sprint Speed"))
            {
                GameUtilities.LocalPlayerController.SprintSpeed = Config.Instance.Player.SprintSpeed;
            }
        }
        private void OnMinersSubTab()
        {
            Config.Instance.Miners.SpawnRate = MenuUtilities.HorizontalSlider("Spawn Rate", Config.Instance.Miners.SpawnRate, 0.00f, 10f);
            if (MenuUtilities.Button("Set Spawn Rate"))
            {
                FindObjectsByType(typeof(AutoMiner), FindObjectsSortMode.None).ToList().ForEach(obj =>
                {
                    AutoMiner miner = obj as AutoMiner;
                    miner.SpawnRate = Config.Instance.Miners.SpawnRate;
                });
            }
        }
        private void OnFurnacesSubTab()
        {
            Config.Instance.Furnaces.ProcessingTime = MenuUtilities.HorizontalSlider("Processing Time", Config.Instance.Furnaces.ProcessingTime, 0.00f, 10f);
            if (MenuUtilities.Button("Set Processing Rate"))
            {
                FindObjectsByType(typeof(BlastFurnace), FindObjectsSortMode.None).ToList().ForEach(obj =>
                {
                    BlastFurnace furnace = obj as BlastFurnace;
                    furnace.ProcessingTime = Config.Instance.Furnaces.ProcessingTime;
                });
            }
        }
        private void OnEconomyTab()
        {
            Config.Instance.Economy.AddMoney = MenuUtilities.TextField(Config.Instance.Economy.AddMoney);
            if (MenuUtilities.Button("Add Money"))
            {
                if (float.TryParse(Config.Instance.Economy.AddMoney, out float addMoney))
                    GameUtilities.EconomyManager.AddMoney(addMoney);
            }
            if (MenuUtilities.Button("Unlock All Shop Items"))
            {
                GameUtilities.EconomyManager.UnlockAllShopItems();
            }
        }
        private void OnDepositBoxSubTab()
        {
            if (MenuUtilities.Button("Upgrade To Tier 2")) {
                GameUtilities.DepositBox.UpgradeToTier2();
            }
            // Only sets the animation speed look at coroutine
            //Config.Instance.DepositBox.Speed = MenuUtilities.HorizontalSlider("Speed", Config.Instance.DepositBox.Speed, 0.00f, 10f);
            //if (MenuUtilities.Button("Set Speed"))
            //{
            //    FieldInfo field = typeof(DepositBox).GetField("_speed", BindingFlags.NonPublic | BindingFlags.Instance);
            //    field.SetValue(GameUtilities.DepositBox, Config.Instance.DepositBox.Speed);
            //}
        }
        private void OnWorldTab()
        {
            MenuUtilities.BeginHorizontal();
            if (MenuUtilities.Button("Miners"))
                CurrentSubTab = SubMenuTab.Miners;
            if (MenuUtilities.Button("Furnaces"))
                CurrentSubTab = SubMenuTab.Furnaces;
            if (MenuUtilities.Button("Deposit Box"))
                CurrentSubTab = SubMenuTab.DepositBox;
            MenuUtilities.EndHorizontal();
            MenuUtilities.Space(10f);

            switch (CurrentSubTab)
            {
                case SubMenuTab.Furnaces:
                    OnFurnacesSubTab();
                    break;
                case SubMenuTab.Miners:
                    OnMinersSubTab();
                    break;
                case SubMenuTab.DepositBox:
                    OnDepositBoxSubTab();
                    break;
            }
        }

        private void OnQuestsTab()
        {
            if (MenuUtilities.Button("Complete All"))
            {
                GameUtilities.QuestManager.AllQuests.ToList().ForEach(quest => {
                    quest.DebugUnlock();
                });
            }
            if (MenuUtilities.Button("Complete Active"))
            {
                GameUtilities.QuestManager.ActiveQuests.ToList().ForEach(quest => {
                    quest.DebugUnlock();
                });
            }
            if (MenuUtilities.Button("Unlock All"))
            {
                GameUtilities.QuestManager.AllQuests.ToList().ForEach(quest => {
                    quest.PrerequisiteQuests.Clear();
                });
            }
        }

        private void OnSettingsTab()
        {

        }

        private void OnWindow(int id)
        {
            // spacing for title bar
            MenuUtilities.Space(25f);

            MenuUtilities.BeginHorizontal();
            if (MenuUtilities.Button("Player"))
                CurrentTab = MenuTab.Player;
            if (MenuUtilities.Button("Economy"))
                CurrentTab = MenuTab.Economy;
            if (MenuUtilities.Button("World"))
                CurrentTab = MenuTab.World;
            if (MenuUtilities.Button("Quests"))
                CurrentTab = MenuTab.Quests;
            if (MenuUtilities.Button("Settings"))
                CurrentTab = MenuTab.Settings;
            MenuUtilities.EndHorizontal();

            switch (CurrentTab)
            {
                case MenuTab.Player:
                    OnPlayerTab();
                    break;
                case MenuTab.Economy:
                    OnEconomyTab();
                    break;
                case MenuTab.World:
                    OnWorldTab();
                    break;
                case MenuTab.Quests:
                    OnQuestsTab();
                    break;
                case MenuTab.Settings:
                    OnSettingsTab();
                    break;
            }

            MenuUtilities.DragWindow(ref windowRect);
        }
        private void OnGUI()
        {
            MenuUtilities.InitStyles();

            if (!ShowMenu) return;

            GUI.Window(55223, windowRect, OnWindow, "MineMogul Mod Menu", MenuUtilities.WindowStyle);
        }
    }
}