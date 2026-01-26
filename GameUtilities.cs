using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MineMogulModMenu
{
    public class GameUtilities : MonoBehaviour
    {
        private static PlayerController _localPlayerController;
        private static EconomyManager _economyManager;
        private static QuestManager _questManager;
        private static DepositBox _depositBox;
        private static List<AutoMiner> _autoMiners;
        private static List<BlastFurnace> _furnaces;
        private static float _lastAutoMinerRefreshTime;
        private static float _lastFurnanceRefreshTime;
        private const float RefreshInterval = 0.5f;

        public static List<BlastFurnace> Furnaces
        {
            get
            {
                if (_furnaces == null || Time.time - _lastFurnanceRefreshTime > RefreshInterval)
                {
                    _furnaces = FindObjectsByType<BlastFurnace>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
                    _lastFurnanceRefreshTime = Time.time;
                }
                return _furnaces;
            }
        }
        public static List<AutoMiner> AutoMiners
        {
            get
            {
                if (_autoMiners == null || Time.time - _lastAutoMinerRefreshTime > RefreshInterval)
                {
                    _autoMiners = FindObjectsByType<AutoMiner>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
                    _lastAutoMinerRefreshTime = Time.time;
                }
                return _autoMiners;
            }
        }
        public static PlayerController LocalPlayerController
        {
            get
            {
                if (_localPlayerController == null)
                    _localPlayerController = Object.FindFirstObjectByType<PlayerController>();
                return _localPlayerController;
            }
        }
        public static QuestManager QuestManager
        {
            get
            {
                if (_questManager == null)
                    _questManager = Object.FindFirstObjectByType<QuestManager>();
                return _questManager;
            }
        }
        public static DepositBox DepositBox
        {
            get
            {
                if (_depositBox == null)
                    _depositBox = Object.FindFirstObjectByType<DepositBox>();
                return _depositBox;
            }
        }
        public static EconomyManager EconomyManager
        {
            get
            {
                if (_economyManager == null)
                    _economyManager = Object.FindFirstObjectByType<EconomyManager>();
                return _economyManager;
            }
        }
        
        private void Awake() {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            GameUtilities.Reset();
        }
        public static void Reset()
        {
            _localPlayerController = null;
            _economyManager = null;
            _depositBox = null;
            _questManager = null;
        }
    }
}