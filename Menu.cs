using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using HarmonyLib;
using UnityEngine;

namespace MineMogulModMenu
{
    public enum MenuTab
    {
        Player,
        Research,
        Economy,
        World,
        Quests,
        Settings
    }
    public enum SubMenuTab
    {
        Miners,
        PolishingMachine,
        Furnaces,
        DepositBox,
        Detonators
    }
    public class Menu : MonoBehaviour
    {
        public MenuTab CurrentTab { get; private set; }
        public SubMenuTab CurrentSubTab { get; private set; }
        public bool ShowMenu { get; private set; }
        private Rect windowRect = new Rect(100, 100, 950f, 750f);

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

            if (ShowMenu)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        private void OnPlayerTab()
        { 
            MenuUtilities.Toggle(ref Config.Instance.Player.Noclip, "Noclip (V)");
            Config.Instance.Player.NoclipSpeed = MenuUtilities.HorizontalSlider("Noclip Speed", Config.Instance.Player.NoclipSpeed, 0.00f, 50f);
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
            Config.Instance.Player.JumpHeight = MenuUtilities.HorizontalSlider("Jump Height", Config.Instance.Player.JumpHeight, 0.00f, 20f);
            if (MenuUtilities.Button("Set Jump Height"))
            {
                GameUtilities.LocalPlayerController.JumpHeight = Config.Instance.Player.JumpHeight;
            }
        }
        private void OnResearchTab()
        {
            Config.Instance.Research.AddTickets = MenuUtilities.TextField(Config.Instance.Research.AddTickets);
            if (MenuUtilities.Button("Add Tickets"))
            {
                if (int.TryParse(Config.Instance.Economy.AddMoney, out int addTickets))
                    GameUtilities.ResearchManager.AddResearchTickets(addTickets);
            }
        }
        private void OnMinersSubTab()
        {
            string[] autoMinersStringList = [.. GameUtilities.AutoMiners.Select(obj => obj.ToString())];
            Config.Instance.Miners.SelectedIndex = MenuUtilities.SelectionTable(
                "Auto Miners",
                [.. autoMinersStringList, "All Miners"],
                Config.Instance.Miners.SelectedIndex
            );
            MinerModManager.SelectedMinerIndex = Config.Instance.Miners.SelectedIndex;
            MenuUtilities.Separator(2f, 6f);

            MenuUtilities.Toggle(ref Config.Instance.Miners.HighlightSelected, "Highlight Selected");
            Config.Instance.Miners.SpawnRate = MenuUtilities.HorizontalSlider("Spawn Rate", Config.Instance.Miners.SpawnRate, 0.00f, 10f);
            if (MenuUtilities.Button("Set Spawn Rate"))
            {
                MinerModManager.ApplySpawnRate(Config.Instance.Miners.SpawnRate);
            }
        }
        private void OnFurnacesSubTab()
        {
            string[] furnacesStringList = [.. GameUtilities.Furnaces.Select(obj => obj.ToString())];
            Config.Instance.Furnaces.SelectedIndex = MenuUtilities.SelectionTable(
                "Furnaces",
                [.. furnacesStringList, "All Furnaces"],
                Config.Instance.Furnaces.SelectedIndex
            );
            FurnaceModManager.SelectedFurnaceIndex = Config.Instance.Furnaces.SelectedIndex;
            MenuUtilities.Separator(2f, 6f);

            MenuUtilities.Toggle(ref Config.Instance.Furnaces.HighlightSelected, "Highlight Selected");
            Config.Instance.Furnaces.ProcessingTime = MenuUtilities.HorizontalSlider("Processing Time", Config.Instance.Furnaces.ProcessingTime, 0.00f, 10f);
            if (MenuUtilities.Button("Set Processing Rate"))
            {
                FurnaceModManager.ApplyProcessingTime(Config.Instance.Furnaces.ProcessingTime);
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
            if (MenuUtilities.Button("Upgrade To Tier 2"))
            {
                GameUtilities.DepositBox.UpgradeToTier2();
            }
            MenuUtilities.Toggle(ref Config.Instance.DepositBox.InstantSell, "Instant Sell"); 
            // Only sets the animation speed look at coroutine
            //Config.Instance.DepositBox.Speed = MenuUtilities.HorizontalSlider("Speed", Config.Instance.DepositBox.Speed, 0.00f, 10f);
            //if (MenuUtilities.Button("Set Speed"))
            //{
            //    FieldInfo field = typeof(DepositBox).GetField("_speed", BindingFlags.NonPublic | BindingFlags.Instance);
            //    field.SetValue(GameUtilities.DepositBox, Config.Instance.DepositBox.Speed);
            //}
        }
        private void OnDetonatorsSubTab()
        {
            if (MenuUtilities.Button("Detonate All"))
            {
                FindObjectsByType<DetonatorTrigger>(FindObjectsSortMode.None).ToList().ForEach(obj => obj.Interact(null));
            }
        }

        private void OnPolishingMachineSubTab()
        {
            MenuUtilities.Toggle(ref Config.Instance.PolishingMachine.IgnoreDirtyOres, "Ignore Dirty Ores");
        }

        private void OnWorldTab()
        {
            MenuUtilities.BeginHorizontal();
            if (MenuUtilities.Button("Miners", CurrentSubTab == SubMenuTab.Miners))
                CurrentSubTab = SubMenuTab.Miners;
            if (MenuUtilities.Button("Furnaces", CurrentSubTab == SubMenuTab.Furnaces))
                CurrentSubTab = SubMenuTab.Furnaces;
            if (MenuUtilities.Button("Polishing", CurrentSubTab == SubMenuTab.PolishingMachine))
                CurrentSubTab = SubMenuTab.PolishingMachine;
            if (MenuUtilities.Button("Deposit Box", CurrentSubTab == SubMenuTab.DepositBox))
                CurrentSubTab = SubMenuTab.DepositBox;
            if (MenuUtilities.Button("Detonators", CurrentSubTab == SubMenuTab.Detonators))
                CurrentSubTab = SubMenuTab.Detonators;

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
                case SubMenuTab.PolishingMachine:
                    OnPolishingMachineSubTab();
                    break;
                case SubMenuTab.DepositBox:
                    OnDepositBoxSubTab();
                    break;
                case SubMenuTab.Detonators:
                    OnDetonatorsSubTab();
                    break;
            }
        }

        private void OnQuestsTab()
        {
            if (MenuUtilities.Button("Complete All"))
            {
                GameUtilities.QuestManager.AllQuests.ToList().ForEach(quest =>
                {
                    quest.DebugUnlock();
                });
            }
            if (MenuUtilities.Button("Complete Active"))
            {
                GameUtilities.QuestManager.ActiveQuests.ToList().ForEach(quest =>
                {
                    quest.DebugUnlock();
                });
            }
            if (MenuUtilities.Button("Unlock All"))
            {
                GameUtilities.QuestManager.AllQuests.ToList().ForEach(quest =>
                {
                    quest.PrerequisiteQuests.Clear();
                });
            }
        }

        private void OnSettingsTab()
        {
            MenuUtilities.Toggle(ref Config.Instance.Settings.HighlightThroughWalls, "Highlight Through Walls");
            MenuUtilities.ColorPicker("Miner Highlight Color", ref Config.Instance.Settings.MinerHighlightColor);
            MenuUtilities.ColorPicker("Furnace Highlight Color", ref Config.Instance.Settings.FurnaceHighlightColor);

        }

        private void OnWindow(int id)
        {
            // spacing for title bar
            MenuUtilities.Space(25f);

            MenuUtilities.BeginHorizontal();
            if (MenuUtilities.Button("Player", CurrentTab == MenuTab.Player))
                CurrentTab = MenuTab.Player;
            if (MenuUtilities.Button("Research", CurrentTab == MenuTab.Research))
                CurrentTab = MenuTab.Research;
            if (MenuUtilities.Button("Economy", CurrentTab == MenuTab.Economy))
                CurrentTab = MenuTab.Economy;
            if (MenuUtilities.Button("World", CurrentTab == MenuTab.World))
                CurrentTab = MenuTab.World;
            if (MenuUtilities.Button("Quests", CurrentTab == MenuTab.Quests))
                CurrentTab = MenuTab.Quests;
            if (MenuUtilities.Button("Settings", CurrentTab == MenuTab.Settings))
                CurrentTab = MenuTab.Settings;
            MenuUtilities.EndHorizontal();

            switch (CurrentTab)
            {
                case MenuTab.Player:
                    MenuUtilities.Space(10f);
                    OnPlayerTab();
                    break;
                case MenuTab.Research:
                    MenuUtilities.Space(10f);
                    OnResearchTab();
                    break;
                case MenuTab.Economy:
                    MenuUtilities.Space(10f);
                    OnEconomyTab();
                    break;
                case MenuTab.World:
                    // Has sub tabs so no spacing
                    OnWorldTab();
                    break;
                case MenuTab.Quests:
                    MenuUtilities.Space(10f);
                    OnQuestsTab();
                    break;
                case MenuTab.Settings:
                    MenuUtilities.Space(10f);
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